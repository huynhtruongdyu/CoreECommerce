using CEC.Domain.Common;
using CEC.Shared.Helper;

using System.ComponentModel;

namespace CEC.Shared.Models
{
    public class UserAcionLog
    {
        public UserAcionLog(string name, EnumUserAcion action, IRootEntity entity)
        {
            Name = name;
            Action = action;
            Entity = entity;
            CreatedAt = DateTime.Now;
        }

        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public EnumUserAcion Action { get; set; }
        public IRootEntity Entity { get; set; }

        public override string ToString()
        {
            return $"[{String.Format("{0:hh:mm:ss dd:MM:yyyy} | {1} | {2}", CreatedAt, Name, Action.GetDescription())}] {Entity.TryGetName()}";
        }
    }

    public enum EnumUserAcion
    {
        [Description("Add")]
        Add,

        [Description("Remove")]
        Remove,

        [Description("Update")]
        Update
    }
}