using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisCachingDemo;
using System.Text.Json;
using System;
using Caching_DynamicUI.Services;
using Caching_DynamicUI.Models;
using StackExchange.Redis;

namespace Caching_DynamicUI.Controllers
{

    [Route("todos")]
    public class TodosRedisController : Controller
    {
        private readonly ToDoService _service;
        private readonly IDatabase _redisDb;

        public TodosRedisController(ToDoService service)
        {
            _service = service;
            _redisDb = RedisConnectorHelper.Connection.GetDatabase();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string cacheKey = "todos";
            var cachedTodos = await _redisDb.StringGetAsync(cacheKey);

            List<ToDo>? todos;

            if (cachedTodos.HasValue)
            {
                todos = JsonSerializer.Deserialize<List<ToDo>>(cachedTodos!);
                ViewBag.Source = "From Redis";
            }
            else
            {
                Thread.Sleep(5000);
                todos = await _service.GetTodosAsync();
                if (todos == null) return NotFound("No todos found");

                var serialized = JsonSerializer.Serialize(todos);
                await _redisDb.StringSetAsync(cacheKey, serialized, TimeSpan.FromMinutes(10));

                ViewBag.Source = "From API";
            }

            return View("RedisChach", todos);
        }
    }

}
