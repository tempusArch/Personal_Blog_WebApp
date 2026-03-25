using PersonalBlog.Models;

namespace PersonalBlog.Interfaces;

public interface IBlogService {
    bool CreateBlog(Blog b);
    bool UpdateBlog(Blog b);
    bool DeleteBlog(int i);
    Blog GetOneBlogById(int i);
    List<Blog> GetAllBlogs();
}