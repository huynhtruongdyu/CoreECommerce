namespace CEC.Domain.Common
{
    public interface IRootEntity : ISoftDeleteEntity
    {
        string TryGetName();
    }
}