namespace CEC.Domain.Common
{
    public abstract class AuditEntity
    {
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        #region methods

        public virtual string TryGetName()
        {
            return (string)this.GetType().GetProperty("Name").GetValue(this, null);
        }

        #endregion methods
    }
}