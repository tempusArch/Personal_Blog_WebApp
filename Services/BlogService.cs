using System.Text.Json;
using PersonalBlog.Interfaces;
using PersonalBlog.Models;

namespace PersonalBlog.Services;

public class BlogService : IBlogService {
    private static string basePath = Directory.GetCurrentDirectory() + "//Data";
  
    public bool CreateBlog(Blog b) {
        try {
            string counterFilePath = Path.Combine(basePath, "counter.txt"); 
            int number = File.Exists(counterFilePath) ? int.Parse(File.ReadAllText(counterFilePath)) : 1;

            string fileName = $"{number}.json";
            string filePath = Path.Combine(basePath, fileName);

            if (createFile(fileName, filePath)) {
                var newBlog = new Blog {
                    Id = number,
                    Title = b.Title,
                    Content = b.Content,
                    Author = b.Author,
                    PublishedDate = DateTime.UtcNow,
                    LastUpdatedDate = DateTime.UtcNow,
                };

                var newJson = JsonSerializer.Serialize<Blog>(newBlog);
                File.WriteAllText(filePath, newJson);
                
                number++;
                File.WriteAllText(counterFilePath, number.ToString());

                return true;
            } else
                return false;

        } catch (Exception e) {
            Console.WriteLine($"Failed at creating. Erro - " + e.Message);
            return false;
        }
    }

    public bool UpdateBlog(Blog b) {
        try {
            string fileName = $"{b.Id}.json";
            string filePath = Path.Combine(basePath, fileName);

            if (File.Exists(filePath)) {
                var updatedBlog = new Blog {
                    Id = b.Id,
                    Title = b.Title,
                    Content = b.Content,
                    Author = b.Author,
                    PublishedDate = b.PublishedDate,
                    LastUpdatedDate = DateTime.UtcNow,
                };

                var updatedJson = JsonSerializer.Serialize<Blog>(updatedBlog);
                File.WriteAllText(filePath, updatedJson);
                Console.WriteLine($"{fileName} updated successfully.");
                return true;
            } else 
                return false;

        } catch (Exception e) {
            Console.WriteLine("Failed at updating. Error - " + e.Message);
            return false;
        }
    }

    public bool DeleteBlog(int i) {
        try {
            string fileName = $"{i}.json";
            string filePath = Path.Combine(basePath, fileName);

            if (File.Exists(filePath)) {
                File.Delete(filePath);
                Console.WriteLine($"{fileName} deleted successfully.");
                return true;
            } else {
                Console.WriteLine($"{fileName} does not exist.");
                return false;
            }
        } catch (Exception e) {
            Console.WriteLine("Failed at deleting. Error - " + e.Message);
            return false;
        } 
    }

    public Blog GetOneBlogById(int i) {
        try {
            string fileName = $"{i}.json";
            string filePath = Path.Combine(basePath, fileName);

            if (File.Exists(filePath)) {
                var jsonData = File.ReadAllText(filePath);
                var result = JsonSerializer.Deserialize<Blog>(jsonData);
                return result ?? new();
            } else {
                Console.WriteLine($"{fileName} does not exist.");
                return new();
            }

        } catch (Exception e) {
            Console.WriteLine("Failed at fetching. Error - " + e.Message);
            return new();
        }
    }

    public List<Blog> GetAllBlogs() {
        try {
            List<Blog> result = new List<Blog>();

            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            string[] filePathRisuto = Directory.GetFiles(basePath, "*.json");

            foreach (var s in filePathRisuto) {
                string jsonData = File.ReadAllText(s);
                var resultData = JsonSerializer.Deserialize<Blog>(jsonData);
                result.Add(resultData);
            }

            return result.OrderBy(n => n.Id).ToList();

        } catch (Exception e) {
            Console.WriteLine("Failed at fetching. Error - " + e.Message);
            return new();
        }
    }

    #region helper methods
    private bool createFile(string fileName, string filePath) {
        try {
            if (!File.Exists(filePath)) 
                using (FileStream i = File.Create(filePath))
                    Console.WriteLine($"{fileName} created successfully.");
            
            return true;
        } catch (Exception e) {
            Console.WriteLine($"Failed at creating {fileName}. Error - " + e.Message);
            return false;
        }
    }
    #endregion
}