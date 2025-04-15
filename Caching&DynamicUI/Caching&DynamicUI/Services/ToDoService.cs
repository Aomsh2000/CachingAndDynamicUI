using Caching_DynamicUI.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Caching_DynamicUI.Services
{
    public class ToDoService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        public ToDoService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<List<ToDo>> GetTodosAsync()
        {

            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos");
            response.EnsureSuccessStatusCode();

            var todos = await response.Content.ReadFromJsonAsync<List<ToDo>>();

        
            return todos!;
        }
    }
}
