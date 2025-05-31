
namespace Core.Interfaces;

/// <summary>
/// Interface creataed for Generic Repository to abstract the Data Access Layer.
/// </summary>
public interface IGenericRepository<T> where T : class
{
    /// <summary>
    /// To get all data in db for "T" as queryable, later filtered using LINQ
    /// </summary>
    /// <returns>IQueryable<T></returns>
    IQueryable<T> GetAsQueryable();

    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve. Must be a positive integer.</param>
    /// <returns>The entity to be retrieved.</returns>
    Task<T?> GetByIdAsync(int id);
    /// <summary>
    /// Adds a record to the target Table
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task AddAsync(T entity);
    /// <summary>
    /// Deletes/Removes a record from the target table.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task Delete(T entity);
    /// <summary>
    /// Updates the entity record in the target table.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task UpdateAsync(T entity);
    /// <summary>
    /// Saves whatever changes we have done to the target table.
    /// </summary>
    /// <returns></returns>
    void SaveChanges();
}
