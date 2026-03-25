using PersonalBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PersonalBlog.Views.Admin;

public class ViewOneBlogModel : PageModel {
    [BindProperty]
    public Blog Blog {get; set;}
    
    public void OnGet() {
        
    }
}