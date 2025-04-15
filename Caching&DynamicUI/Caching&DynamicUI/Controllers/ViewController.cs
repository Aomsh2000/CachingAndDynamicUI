using Caching_DynamicUI.Models;
using Caching_DynamicUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RedisCachingDemo;
using StackExchange.Redis;
using System.Text.Json;

namespace Caching_DynamicUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewController : ControllerBase
    {
        private readonly ToDoService _service;
        private readonly IMemoryCache _memoryCache;
        private readonly IDatabase _redisDb;

        public ViewController(ToDoService service, IMemoryCache memoryCache)
        {
            _service = service;
            _memoryCache = memoryCache;
            _redisDb = RedisConnectorHelper.Connection.GetDatabase(); // Assuming you have RedisConnectorHelper setup
        }

        private async Task<List<ToDo>> GetTodosWithCachingAsync()
        {
            const string cacheKey = "todos";

            // 1. Check Memory Cache
            if (_memoryCache.TryGetValue(cacheKey, out List<ToDo> memoryTodos))
            {
                return memoryTodos;
            }

            // 2. Check Redis Cache
            var redisValue = await _redisDb.StringGetAsync(cacheKey);
            if (redisValue.HasValue)
            {
                var redisTodos = JsonSerializer.Deserialize<List<ToDo>>(redisValue!);
                // Store in memory cache too
                _memoryCache.Set(cacheKey, redisTodos, TimeSpan.FromMinutes(5));
                return redisTodos!;
            }

            // 3. Fetch from API via service
            var todos = await _service.GetTodosAsync();
            if (todos != null)
            {
                var serialized = JsonSerializer.Serialize(todos);
                await _redisDb.StringSetAsync(cacheKey, serialized, TimeSpan.FromMinutes(10));
                _memoryCache.Set(cacheKey, todos, TimeSpan.FromMinutes(5));
            }

            return todos ?? new List<ToDo>();
        }

        [HttpGet("view1")]
        public async Task<IActionResult> GetView1Data()
        {
            var todos = await GetTodosWithCachingAsync();
            return Ok(todos);
        }

        [HttpGet("view2")]
        public async Task<IActionResult> GetView2Data()
        {
            var todos = await GetTodosWithCachingAsync();
            var completed = todos.Where(t => t.Completed).ToList();
            return Ok(completed);
        }

        [HttpGet("view3")]
        public async Task<IActionResult> GetView3Data()
        {
            var todos = await GetTodosWithCachingAsync();
            var notCompleted = todos.Where(t => !t.Completed).ToList();
            return Ok(notCompleted);
        }
    }
}
