using PersonalBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PersonalBlog.Views.Admin;

public class CreateBlogModel : PageModel {
    [BindProperty]
    public Blog BlogPost {get; set;}

    public IActionResult OnPost() {
        if (!ModelState.IsValid) {
            return Page();
        }

        return RedirectToPage("Viewing");
    }
}