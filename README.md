# 📦 Class Task: Caching and Dynamic UI Updates in .NET Using an Online API

## 🎯 Objective
Build a complete .NET web application that fetches data from an **online API**, implements **caching** using **Redis** and **IMemoryCache**, dynamically updates the **UI using JavaScript**, and optimizes **image loading** using **lazy loading**.  
By the end of the task, you'll have a fully functional and performance-optimized app.

---

## 🧩 Task Breakdown

### ✅ Part 1: Use an Online API and Implement Redis Caching

**Goal:** Fetch data from an online API and cache it using Redis.

#### Steps:
- Set up Redis (locally).
- Use a public API like [JSONPlaceholder](https://jsonplaceholder.typicode.com/) to fetch data. I used (todos).
- Create a .NET Web API to fetch and cache the data in Redis.

---

### ✅ Part 2: In-Memory Caching with IMemoryCache (.NET)

**Goal:** Add in-memory caching using IMemoryCache.

#### Steps:
- Register and configure `IMemoryCache` in my Web API.
- Implement caching for the same API data (todos).

---

### ✅ Part 3: Dynamic UI Updates with JavaScript

**Goal:** Update UI dynamically with JavaScript and Fetch API.

#### Steps:
- Create a CSHTML UI to show todos, etc.
- Use `fetch()` to get data from my backend Web API.
- Render data dynamically without full page reloads.

---

### ✅ Part 4: Image Optimization with Lazy Loading 

**Goal:** Optimize image loading using lazy loading.

#### Steps:
- Use free image services like [Lorem Picsum](https://picsum.photos/).
- Display a gallery of images.
- Use `loading="lazy"` **and** implement lazy loading with the **Intersection Observer API**.

---

### ✅ Final Task: Integration and Testing (30 mins)

**Goal:** Connect all parts into one full app.

#### Steps:
- Ensure backend caching (Redis + MemoryCache) is functioning.
- Ensure frontend fetches and displays cached data dynamically.
- Confirm lazy loading works and performance is optimized.
---


## 🚀 Technologies Used

- **ASP.NET Core Web API**
- **Redis**
- **IMemoryCache**
- **JavaScript (Fetch API)**
- **HTML + CSS**
- **CSHTML + Razor**
- **Intersection Observer API / loading="lazy"**
- **JSONPlaceholder / Lorem Picsum APIs**

---
## 💡 Tip
To test Redis:
- Ensure Redis is running (check with `redis-cli ping` => should return `PONG`).
- Use breakpoints or logs to verify if data is being served from cache vs API.

---
