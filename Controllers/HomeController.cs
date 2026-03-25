using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Models;
using PersonalBlog.Services;
using PersonalBlog.Interfaces;
using PersonalBlog.Views.Home;

namespace PersonalBlog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBlogService _blogService;

    public HomeController(ILogger<HomeController> logger, IBlogService blogService)
    {
        _logger = logger;
        _blogService = blogService;
    }

    public IActionResult Index()
    {
        ViewData["Blogs"] = _blogService.GetAllBlogs();
        return View();
    }

    public IActionResult ViewBlog(int id)
    {
        var blog = _blogService.GetOneBlogById(id);

        var viewModel = new ViewBlogModel {
            Blog = blog,
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
