using PersonalBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PersonalBlog.Views.Home;

public class ViewBlogModel : PageModel {
    [BindProperty]
    public Blog Blog {get; set;}

    public void OnGet() {
        
    }
}