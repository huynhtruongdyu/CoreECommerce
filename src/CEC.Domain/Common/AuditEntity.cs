﻿namespace CEC.Domain.Common
{
    public class AuditEntity<TIdentity>
    {
        public TIdentity Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}