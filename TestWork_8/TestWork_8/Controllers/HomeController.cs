﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWork_8.Data;
using TestWork_8.Models;

namespace TestWork_8.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var them = _context.Themses
                .Include(t => t.User)
                .OrderByDescending(r => r.Id);

            return View(them.ToList());
        }

        public IActionResult CreateThem(string userId, string nameThem, string contentThem, Thems thems)
        {
//            var them = _context.Themses.FirstOrDefault(c => c.UserId == userId);
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(User);
                var thm = new Thems
                {
                    UserId = userId,
                    NameThem = nameThem,
                    ContentThems = contentThem,
                    DateCreateThem = DateTime.Now
                };

                thm.UserId = user.Result.Id;
                _context.Add(thm);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        #region Create

        // GET: Comments/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,NameThem,ContentThems,DateCreateThem")]
            Thems thems)
        {
            var them = _context.Themses.FirstOrDefault(c => c.Id == thems.Id);
            if (them != null)
            {
                thems.UserId = them.UserId;
                thems.NameThem = them.NameThem;
                thems.ContentThems = them.ContentThems;

            }


            if (ModelState.IsValid)
            {
                _context.Add(thems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(thems);
        }

        #endregion

        
    }
}