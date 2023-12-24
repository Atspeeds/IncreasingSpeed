using IncreasingSpeed.Infrastrure;
using IncreasingSpeed.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace IncreasingSpeed.Controllers
{
    [ResponseCache(CacheProfileName ="default")]
    public class HomeController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        private readonly IDistributedCache _distributedCache;

        private readonly IncreasingSpeedContext _context;

        public HomeController(IncreasingSpeedContext context, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            _context = context;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }

        public IActionResult Index()
        {
            List<User> Users;

            if (!_memoryCache.TryGetValue("UsersApi", out Users))
            {
                Users = _context.users;
                _memoryCache.Set("UsersApi", Users, TimeSpan.FromSeconds(5));
            }


            return View(Users);
        }

        public IActionResult Privacy()
        {
            List<User> Users;
            var user = _distributedCache.Get("UserData");

            if (user == null)
            {

                Users = _context.users;

                var userJson = JsonConvert.SerializeObject(_context.users);
                var usereByte = Encoding.UTF8.GetBytes(userJson);

                var optionsDistribut = new DistributedCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(5),
                };
                _distributedCache.Set("UserData", usereByte, optionsDistribut);
                ViewData["WhatData"] = "Database";
            }
            else
            {
                var data = Encoding.UTF8.GetString(_distributedCache.Get("UserData"));
                Users = JsonConvert.DeserializeObject<List<User>>(data);
                ViewData["WhatData"] = "Cach";
            }
           

            return View(Users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
