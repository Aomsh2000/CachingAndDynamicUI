using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using Caching_DynamicUI.Services;
using Caching_DynamicUI.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Caching_DynamicUI.Controllers
{

    [Route("TodosMemory")]
    public class TodosImemoryController : Controller
    {
        private readonly ToDoService _service;
        private readonly IMemoryCache _cache;

        public TodosImemoryController(ToDoService service, IMemoryCache cache)
        {
            _service = service;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string cacheKey = "todos";
            List<ToDo>? todos;

            // Try to get from memory cache
            if (!_cache.TryGetValue(cacheKey, out todos))
            {
                // If not in cache, fetch from service
                todos = await _service.GetTodosAsync();

                if (todos == null)
                    return NotFound("No todos found");

                // Simulate delay
                Thread.Sleep(5000);

                // Set cache options
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                // Save in cache
                _cache.Set(cacheKey, todos, cacheEntryOptions);

                ViewBag.Source = "From API";
            }
            else
            {
                ViewBag.Source = "From Memory Cache";
            }

            return View("MemoryCache", todos);
        }
    }

}
