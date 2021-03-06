﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
                .OrderByDescending(r => r.DateCreateThem.Date);

            return View(them.ToList());
        }


        #region Details

        public async Task<IActionResult> Details(string id)
        {
            ViewBag.CommentList = _context.Comments
                .Include(t => t.User)
                .OrderByDescending(c => c.Content);
            if (id == null)
            {
                return NotFound();
            }

            var thems = await _context.Themses
                .SingleOrDefaultAsync(m => m.Id == id);
            if (thems == null)
            {
                return NotFound();
            }

            return View(thems);
        }

        #endregion

        #region Create

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,NameThem,ContentThems,DateCreateThem")]
            Thems thems)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var createThems = CreateThems(thems);
                createThems.UserId = user.Id;

                _context.Add(createThems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(thems);
        }

        #endregion

        #region CreateThems

        private Thems CreateThems(Thems thems)
        {
            var tms = new Thems()
            {
                Id = thems.UserId,
                NameThem = thems.NameThem,
                ContentThems = thems.ContentThems,
                DateCreateThem = DateTime.Now
            };

            return tms;
        }

        #endregion

        #region Commmmmment

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Comment(string themsId, string userId, string content, Comment comment)
        {
            var themS = _context.Themses.FirstOrDefault(c => c.Id == comment.ThemsId);
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var comm = new Comment
                {
                    UserId = userId,
                    ThemsId = themsId,
                    Content = content,
                    NameThemsComment = themS.NameThem,
                    CommentDate = DateTime.Now
                };

                comm.UserId = user.Id;
                _context.Add(comm);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        #endregion

        #region ShowComment

        public ActionResult ShowComment(int id = 0)
        {
            if (id > 0)
            {
                Thems thems = _context.Themses.Find(id);

                _context.Add(thems);
                _context.SaveChanges();
                return View(_context.Themses.OrderBy(t => t.DateCreateThem));
            }

            return Redirect("Details");
        }

        #endregion

        #region Delete

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theme = await _context.Themses
                .SingleOrDefaultAsync(m => m.Id == id);
            if (theme == null)
            {
                return NotFound();
            }

            return View(theme);
        }

        // POST: Publications/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Thems theme = await _context.Themses
                .SingleOrDefaultAsync(m => m.Id == id);
            var comm = await _context.Comments.SingleOrDefaultAsync(c => c.ThemsId == id);

            if (theme != null)
            {
                _context.Comments.Remove(comm);
                _context.Themses.Remove(theme);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Themses.Remove(theme);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}