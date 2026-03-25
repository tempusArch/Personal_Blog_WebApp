using PersonalBlog.Interfaces;
using PersonalBlog.Models;
using PersonalBlog.Services;
using PersonalBlog.Views.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PersonalBlog.Controllers;

public class AdminController : Controller {
    private readonly IBlogService _blogService;
    public AdminController(IBlogService blogService) {
        _blogService = blogService;
    }

    public IActionResult CreateBlog() {
        return View();
    }

    public IActionResult EditBlog(int id) {
        var blog = _blogService.GetOneBlogById(id);

        return View(new EditBlogModel {
            BlogPost = blog
        });
    }

    public IActionResult GetBlog() {
        return View();
    }

    public IActionResult Viewing() {
        ViewData["Blog"] = _blogService.GetAllBlogs();
        return View();
    }

    public IActionResult ViewOneBlog(int id) {
        var blog = _blogService.GetOneBlogById(id);
        var viewModel = new ViewOneBlogModel {
            Blog = blog
        };

        return View(viewModel);
    }

    public IActionResult DeleteBlog(int id) {
        var b = _blogService.DeleteBlog(id);
        return RedirectToAction("Viewing", "Admin");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Blog blogPost) {
        if (ModelState.IsValid) {
            _blogService.CreateBlog(blogPost);
            return RedirectToAction("Viewing", "Admin");
        }

        return View(blogPost);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Blog blogPost) {
        if (ModelState.IsValid) {
            _blogService.UpdateBlog(blogPost);
            return RedirectToAction("Viewing", "Admin");
        }

        return View(blogPost);
    }
}