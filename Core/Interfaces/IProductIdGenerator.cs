using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductIdGenerator
    {
        /// <summary>
        /// Generates a unique 6-digit product identifier that is safe for distributed systems.
        /// </summary>
        /// <returns>A Task containing a string representing a 6-digit unique identifier</returns>
        Task<string> GenerateProductIdAsync();
    }
} 