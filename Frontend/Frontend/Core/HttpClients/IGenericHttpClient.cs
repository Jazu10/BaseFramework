using Frontend.DTO.Response.Common;

namespace Frontend.Core.HttpClients
{
    public interface IGenericHttpClient
    {
        Task<Result<List<TResponse>>> GetAllAsync<TResponse>(string address);
        Task<Result<TResponse>> GetAsync<TResponse>(string address);
        Task<Result<TResponse>> PostAsync<TResponse>(string address, dynamic dynamicObject);
        Task<Result<TResponse>> PutAsync<TResponse>(string address, dynamic dynamicObject);
        Task DeleteAsync(string address);
    }
}
