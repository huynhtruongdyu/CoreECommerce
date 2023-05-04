using System.Reflection;

namespace CEC.Infrastructure
{
    public class InfrastructureAssemblyReference
    {
        public static Assembly Assembly { get => Assembly.GetExecutingAssembly(); }
    }
}