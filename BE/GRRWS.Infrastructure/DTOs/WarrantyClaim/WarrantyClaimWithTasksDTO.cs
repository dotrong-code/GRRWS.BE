using System;
using System.Collections.Generic;

namespace GRRWS.Infrastructure.DTOs.WarrantyClaim
{
    public class WarrantyClaimWithTasksDTO
    {
        public Guid Id { get; set; }
        public string ClaimNumber { get; set; }
        public string ClaimStatus { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public string Resolution { get; set; }
        public string IssueDescription { get; set; }
        public string WarrantyNotes { get; set; }
        public string ContractNumber { get; set; }
        public decimal? ClaimAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        
        // Device warranty information
        public DeviceWarrantySummaryDTO DeviceWarranty { get; set; }
        
        // Related tasks
        public TaskSummaryDTO SubmissionTask { get; set; }
        public TaskSummaryDTO ReturnTask { get; set; }
        
        // Documents
        public List<WarrantyDocumentDTO> Documents { get; set; } = new List<WarrantyDocumentDTO>();
    }

    public class TaskSummaryDTO
    {
        public Guid Id { get; set; }
        public string TaskName { get; set; }
        public string TaskType { get; set; }
        public string Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? ExpectedTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string AssigneeName { get; set; }
        public Guid? AssigneeId { get; set; }
    }

    public class DeviceWarrantySummaryDTO
    {
        public Guid Id { get; set; }
        public string Provider { get; set; }
        public string WarrantyCode { get; set; }
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string DeviceName { get; set; }
        public string DeviceCode { get; set; }
    }

    public class WarrantyDocumentDTO
    {
        public Guid Id { get; set; }
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public string DocumentUrl { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
    }
}