﻿using Microsoft.AspNetCore.Mvc;

namespace WebStore_GB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        
        public IActionResult Blog() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult Cart() => View();

        public IActionResult CheckOut() => View();

        public IActionResult ContactUs() => View();

        public IActionResult Login() => View();

        public IActionResult ProductDetails() => View();

        public IActionResult Shop() => View();
    }
}