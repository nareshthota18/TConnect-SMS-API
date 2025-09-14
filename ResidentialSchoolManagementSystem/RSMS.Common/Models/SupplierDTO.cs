﻿namespace RSMS.Common.Models
{
    public class SupplierDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? GSTNumber { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
    }
}
