using ClosedXML.Excel;
using ExcelDataReader;
using FluentValidation;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GRRWS.Application.Implement.Service
{
    public class ImportService : IImportService
    {
        public async Task<Result> ImportAsync<TEntity>(Stream excelStream, IGenericRepository<TEntity> repository) where TEntity : class, new()
        {
            var errors = new List<Infrastructure.DTOs.Common.Error>();
            var entities = new List<TEntity>();

            // Ensure the stream is readable
            if (!excelStream.CanRead)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("Cannot read the provided Excel file stream.", "Can not read"));
            }

            // Register encoding provider for ExcelDataReader
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var reader = ExcelReaderFactory.CreateReader(excelStream))
            {
                // Read header row to get column names
                if (!reader.Read())
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("Excel file is empty or invalid.", "empty"));
                }

                // Get column names from header row
                var columnNames = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnNames.Add(reader.GetString(i)?.Trim() ?? string.Empty);
                }

                // Check if Id column exists in the Excel file
                if (!columnNames.Contains("Id"))
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("Excel file must contain an 'Id' column.", "missing_id"));
                }

                int rowNumber = 1; // Start from row 2 (after header)
                while (reader.Read())
                {
                    rowNumber++;
                    var entity = new TEntity();
                    bool hasError = false;

                    // Map Excel columns to entity properties
                    foreach (PropertyInfo property in typeof(TEntity).GetProperties().Where(p => p.CanWrite))
                    {
                        try
                        {
                            // Find column index by property name
                            int columnIndex = columnNames.IndexOf(property.Name);
                            if (columnIndex >= 0 && reader.GetValue(columnIndex) != null && reader.GetValue(columnIndex) != DBNull.Value)
                            {
                                var value = reader.GetValue(columnIndex);
                                var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                                object convertedValue = null;
                                if (propertyType == typeof(string))
                                {
                                    convertedValue = reader.GetString(columnIndex); // Use GetString for strings
                                }
                                else if (propertyType.IsEnum)
                                {
                                    convertedValue = Enum.Parse(propertyType, value.ToString(), true);
                                }
                                else if (propertyType == typeof(DateTime))
                                {
                                    if (DateTime.TryParse(value.ToString(), out var date))
                                        convertedValue = date;
                                    else
                                        throw new FormatException("Invalid date format.");
                                }
                                else if (propertyType == typeof(Guid))
                                {
                                    if (Guid.TryParse(value.ToString(), out var guid))
                                        convertedValue = guid;
                                    else
                                        throw new FormatException("Invalid GUID format.");
                                }
                                else if (propertyType == typeof(bool))
                                {
                                    if (bool.TryParse(value.ToString(), out var boolValue))
                                        convertedValue = boolValue;
                                    else if (value.ToString().Equals("TRUE", StringComparison.OrdinalIgnoreCase))
                                        convertedValue = true;
                                    else if (value.ToString().Equals("FALSE", StringComparison.OrdinalIgnoreCase))
                                        convertedValue = false;
                                    else
                                        throw new FormatException("Invalid boolean format.");
                                }
                                else if (propertyType == typeof(decimal))
                                {
                                    if (decimal.TryParse(value.ToString(), out var decimalValue))
                                        convertedValue = decimalValue;
                                    else
                                        throw new FormatException("Invalid decimal format.");
                                }
                                else
                                {
                                    convertedValue = Convert.ChangeType(value, propertyType);
                                }

                                property.SetValue(entity, convertedValue);
                            }
                            else if (property.Name == "Id")
                            {
                                // Id is required and must be provided in the Excel file
                                throw new FormatException("Id column cannot be empty.");
                            }
                        }
                        catch (Exception ex)
                        {
                            errors.Add(Infrastructure.DTOs.Common.Error.Validation($"Row {rowNumber}: Error mapping column '{property.Name}': {ex.Message}", "Empty"));
                            hasError = true;
                            break;
                        }
                    }

                    if (!hasError)
                    {
                        // Set default values for BaseEntity (excluding Id)
                        if (entity is BaseEntity baseEntity)
                        {
                            baseEntity.CreatedDate = DateTime.UtcNow;
                            baseEntity.IsDeleted = false;
                        }
                        entities.Add(entity);
                    }
                }
            }

            if (errors.Any())
            {
                return Result.Failures(errors);
            }

            if (!entities.Any())
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("No valid entities to import.", "Valid entities"));
            }

            // Save entities to database
            try
            {
                await repository.CreateRangeAsync(entities);
                return Result.SuccessWithObject(new { ImportedCount = entities.Count });
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation($"Failed to save entities to database: {ex.Message}", "Cannot save entities"));
            }
        }
    }
}