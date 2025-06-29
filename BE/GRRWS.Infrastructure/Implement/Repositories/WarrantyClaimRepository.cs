using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.WarrantyClaim;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class WarrantyClaimRepository : GenericRepository<WarrantyClaim>, IWarrantyClaimRepository
    {
        public WarrantyClaimRepository(GRRWSContext context) : base(context) { }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.WarrantyClaims
                .Where(wc => !wc.IsDeleted)
                .CountAsync();
        }

        public async Task<WarrantyClaimWithTasksDTO> GetWarrantyClaimWithTasksAsync(Guid warrantyClaimId)
        {
            return await _context.WarrantyClaims
                .Include(wc => wc.SubmittedByTask)
                    .ThenInclude(t => t.Assignee)
                .Include(wc => wc.ReturnTask)
                    .ThenInclude(t => t.Assignee)
                .Include(wc => wc.DeviceWarranty)
                    .ThenInclude(dw => dw.Device)
                .Include(wc => wc.Documents)
                .Where(wc => wc.Id == warrantyClaimId && !wc.IsDeleted)
                .Select(wc => new WarrantyClaimWithTasksDTO
                {
                    Id = wc.Id,
                    ClaimNumber = wc.ClaimNumber,
                    ClaimStatus = wc.ClaimStatus.ToString(),
                    ExpectedReturnDate = wc.ExpectedReturnDate,
                    ActualReturnDate = wc.ActualReturnDate,
                    Resolution = wc.Resolution,
                    IssueDescription = wc.IssueDescription,
                    WarrantyNotes = wc.WarrantyNotes,
                    ContractNumber = wc.ContractNumber,
                    ClaimAmount = wc.ClaimAmount,
                    CreatedDate = wc.CreatedDate,
                    
                    SubmissionTask = wc.SubmittedByTask == null ? null : new TaskSummaryDTO
                    {
                        Id = wc.SubmittedByTask.Id,
                        TaskName = wc.SubmittedByTask.TaskName,
                        TaskType = wc.SubmittedByTask.TaskType.ToString(),
                        Status = wc.SubmittedByTask.Status.ToString(),
                        StartTime = wc.SubmittedByTask.StartTime,
                        ExpectedTime = wc.SubmittedByTask.ExpectedTime,
                        EndTime = wc.SubmittedByTask.EndTime,
                        AssigneeName = wc.SubmittedByTask.Assignee.FullName,
                        AssigneeId = wc.SubmittedByTask.AssigneeId
                    },
                    
                    ReturnTask = wc.ReturnTask == null ? null : new TaskSummaryDTO
                    {
                        Id = wc.ReturnTask.Id,
                        TaskName = wc.ReturnTask.TaskName,
                        TaskType = wc.ReturnTask.TaskType.ToString(),
                        Status = wc.ReturnTask.Status.ToString(),
                        StartTime = wc.ReturnTask.StartTime,
                        ExpectedTime = wc.ReturnTask.ExpectedTime,
                        EndTime = wc.ReturnTask.EndTime,
                        AssigneeName = wc.ReturnTask.Assignee.FullName,
                        AssigneeId = wc.ReturnTask.AssigneeId
                    },
                    
                    DeviceWarranty = new DeviceWarrantySummaryDTO
                    {
                        Id = wc.DeviceWarranty.Id,
                        Provider = wc.DeviceWarranty.Provider,
                        WarrantyCode = wc.DeviceWarranty.WarrantyCode,
                        WarrantyStartDate = wc.DeviceWarranty.WarrantyStartDate,
                        WarrantyEndDate = wc.DeviceWarranty.WarrantyEndDate,
                        DeviceName = wc.DeviceWarranty.Device.DeviceName,
                        DeviceCode = wc.DeviceWarranty.Device.DeviceCode
                    },
                    
                    Documents = wc.Documents.Select(doc => new WarrantyDocumentDTO
                    {
                        Id = doc.Id,
                        DocumentType = doc.DocumentType,
                        DocumentName = doc.DocumentName,
                        DocumentUrl = doc.DocumentUrl,
                        Description = doc.Description,
                        UploadDate = doc.UploadDate ?? TimeHelper.GetHoChiMinhTime()
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<WarrantyClaimWithTasksDTO>> GetWarrantyClaimsWithTasksAsync(int pageNumber, int pageSize)
        {
            return await _context.WarrantyClaims
                .Include(wc => wc.SubmittedByTask)
                    .ThenInclude(t => t.Assignee)
                .Include(wc => wc.ReturnTask)
                    .ThenInclude(t => t.Assignee)
                .Include(wc => wc.DeviceWarranty)
                    .ThenInclude(dw => dw.Device)
                .Include(wc => wc.Documents)
                .Where(wc => !wc.IsDeleted)
                .OrderByDescending(wc => wc.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(wc => new WarrantyClaimWithTasksDTO
                {
                    Id = wc.Id,
                    ClaimNumber = wc.ClaimNumber,
                    ClaimStatus = wc.ClaimStatus.ToString(),
                    ExpectedReturnDate = wc.ExpectedReturnDate,
                    ActualReturnDate = wc.ActualReturnDate,
                    Resolution = wc.Resolution,
                    IssueDescription = wc.IssueDescription,
                    WarrantyNotes = wc.WarrantyNotes,
                    ContractNumber = wc.ContractNumber,
                    ClaimAmount = wc.ClaimAmount,
                    CreatedDate = wc.CreatedDate,
                    
                    SubmissionTask = wc.SubmittedByTask == null ? null : new TaskSummaryDTO
                    {
                        Id = wc.SubmittedByTask.Id,
                        TaskName = wc.SubmittedByTask.TaskName,
                        TaskType = wc.SubmittedByTask.TaskType.ToString(),
                        Status = wc.SubmittedByTask.Status.ToString(),
                        StartTime = wc.SubmittedByTask.StartTime,
                        ExpectedTime = wc.SubmittedByTask.ExpectedTime,
                        EndTime = wc.SubmittedByTask.EndTime,
                        AssigneeName = wc.SubmittedByTask.Assignee.FullName,
                        AssigneeId = wc.SubmittedByTask.AssigneeId
                    },
                    
                    ReturnTask = wc.ReturnTask == null ? null : new TaskSummaryDTO
                    {
                        Id = wc.ReturnTask.Id,
                        TaskName = wc.ReturnTask.TaskName,
                        TaskType = wc.ReturnTask.TaskType.ToString(),
                        Status = wc.ReturnTask.Status.ToString(),
                        StartTime = wc.ReturnTask.StartTime,
                        ExpectedTime = wc.ReturnTask.ExpectedTime,
                        EndTime = wc.ReturnTask.EndTime,
                        AssigneeName = wc.ReturnTask.Assignee.FullName,
                        AssigneeId = wc.ReturnTask.AssigneeId
                    },
                    
                    DeviceWarranty = new DeviceWarrantySummaryDTO
                    {
                        Id = wc.DeviceWarranty.Id,
                        Provider = wc.DeviceWarranty.Provider,
                        WarrantyCode = wc.DeviceWarranty.WarrantyCode,
                        WarrantyStartDate = wc.DeviceWarranty.WarrantyStartDate,
                        WarrantyEndDate = wc.DeviceWarranty.WarrantyEndDate,
                        DeviceName = wc.DeviceWarranty.Device.DeviceName,
                        DeviceCode = wc.DeviceWarranty.Device.DeviceCode
                    },
                    
                    Documents = wc.Documents.Select(doc => new WarrantyDocumentDTO
                    {
                        Id = doc.Id,
                        DocumentType = doc.DocumentType,
                        DocumentName = doc.DocumentName,
                        DocumentUrl = doc.DocumentUrl,
                        Description = doc.Description,
                        UploadDate = doc.UploadDate ?? TimeHelper.GetHoChiMinhTime()
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<WarrantyClaimWithTasksDTO> GetWarrantyClaimByTaskIdAsync(Guid taskId, bool isSubmissionTask)
        {
            // Create a query based on the task type
            var warrantyClaim = isSubmissionTask
                ? await _context.WarrantyClaims
                    .Include(wc => wc.SubmittedByTask)
                        .ThenInclude(t => t.Assignee)
                    .Include(wc => wc.ReturnTask)
                        .ThenInclude(t => t.Assignee)
                    .Include(wc => wc.DeviceWarranty)
                        .ThenInclude(dw => dw.Device)
                    .Include(wc => wc.Documents)
                    .Where(wc => wc.SubmittedByTaskId == taskId && !wc.IsDeleted)
                    .FirstOrDefaultAsync()
                : await _context.WarrantyClaims
                    .Include(wc => wc.SubmittedByTask)
                        .ThenInclude(t => t.Assignee)
                    .Include(wc => wc.ReturnTask)
                        .ThenInclude(t => t.Assignee)
                    .Include(wc => wc.DeviceWarranty)
                        .ThenInclude(dw => dw.Device)
                    .Include(wc => wc.Documents)
                    .Where(wc => wc.ReturnTaskId == taskId && !wc.IsDeleted)
                    .FirstOrDefaultAsync();
            
            if (warrantyClaim == null)
                return null;
                
            return new WarrantyClaimWithTasksDTO
            {
                Id = warrantyClaim.Id,
                ClaimNumber = warrantyClaim.ClaimNumber,
                ClaimStatus = warrantyClaim.ClaimStatus.ToString(),
                ExpectedReturnDate = warrantyClaim.ExpectedReturnDate,
                ActualReturnDate = warrantyClaim.ActualReturnDate,
                Resolution = warrantyClaim.Resolution,
                IssueDescription = warrantyClaim.IssueDescription,
                WarrantyNotes = warrantyClaim.WarrantyNotes,
                ContractNumber = warrantyClaim.ContractNumber,
                ClaimAmount = warrantyClaim.ClaimAmount,
                CreatedDate = warrantyClaim.CreatedDate,
                
                SubmissionTask = warrantyClaim.SubmittedByTask == null ? null : new TaskSummaryDTO
                {
                    Id = warrantyClaim.SubmittedByTask.Id,
                    TaskName = warrantyClaim.SubmittedByTask.TaskName,
                    TaskType = warrantyClaim.SubmittedByTask.TaskType.ToString(),
                    Status = warrantyClaim.SubmittedByTask.Status.ToString(),
                    StartTime = warrantyClaim.SubmittedByTask.StartTime,
                    ExpectedTime = warrantyClaim.SubmittedByTask.ExpectedTime,
                    EndTime = warrantyClaim.SubmittedByTask.EndTime,
                    AssigneeName = warrantyClaim.SubmittedByTask.Assignee?.FullName,
                    AssigneeId = warrantyClaim.SubmittedByTask.AssigneeId
                },
                
                ReturnTask = warrantyClaim.ReturnTask == null ? null : new TaskSummaryDTO
                {
                    Id = warrantyClaim.ReturnTask.Id,
                    TaskName = warrantyClaim.ReturnTask.TaskName,
                    TaskType = warrantyClaim.ReturnTask.TaskType.ToString(),
                    Status = warrantyClaim.ReturnTask.Status.ToString(),
                    StartTime = warrantyClaim.ReturnTask.StartTime,
                    ExpectedTime = warrantyClaim.ReturnTask.ExpectedTime,
                    EndTime = warrantyClaim.ReturnTask.EndTime,
                    AssigneeName = warrantyClaim.ReturnTask.Assignee?.FullName,
                    AssigneeId = warrantyClaim.ReturnTask.AssigneeId
                },
                
                DeviceWarranty = new DeviceWarrantySummaryDTO
                {
                    Id = warrantyClaim.DeviceWarranty.Id,
                    Provider = warrantyClaim.DeviceWarranty.Provider,
                    WarrantyCode = warrantyClaim.DeviceWarranty.WarrantyCode,
                    WarrantyStartDate = warrantyClaim.DeviceWarranty.WarrantyStartDate,
                    WarrantyEndDate = warrantyClaim.DeviceWarranty.WarrantyEndDate,
                    DeviceName = warrantyClaim.DeviceWarranty.Device?.DeviceName,
                    DeviceCode = warrantyClaim.DeviceWarranty.Device?.DeviceCode
                },
                
                Documents = warrantyClaim.Documents?.Select(doc => new WarrantyDocumentDTO
                {
                    Id = doc.Id,
                    DocumentType = doc.DocumentType,
                    DocumentName = doc.DocumentName,
                    DocumentUrl = doc.DocumentUrl, // This URL will be resolved in the service
                    Description = doc.Description,
                    UploadDate = doc.UploadDate ?? TimeHelper.GetHoChiMinhTime()
                }).ToList() ?? new List<WarrantyDocumentDTO>()
            };
        }
    }
}
