﻿using Microsoft.AspNetCore.Mvc;
using System;

namespace WebStore_GB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Throw(string id) => throw new ApplicationException(id ?? "Error!");
        
        public IActionResult Blog() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult ContactUs() => View();        

        public IActionResult Error404() => View();
    }
}