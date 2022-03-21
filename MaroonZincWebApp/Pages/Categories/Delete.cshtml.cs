using MaroonZincWebApp.Data;
using MaroonZincWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaroonZincWebApp.Pages.Categories
{
    [BindProperties] //Pozwala usunąć (Category category) z argumentów OnPost
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            Category = _db.Category.Find(id);
            //Category = _db.Category.FirstOrDefault(u=>u.ID == id);
            //Category = _db.Category.SingleOrDefault(u=>u.ID == id);
            //Category = _db.Category.Where(u => u.ID == id).FirstOrDefault();
        }
        public async Task<IActionResult> OnPost()
        {
                var categoryFromDb = _db.Category.Find(Category.ID);
                if (categoryFromDb != null)
                {
                    _db.Category.Remove(categoryFromDb);
                    await _db.SaveChangesAsync();
                TempData["success"] = "Category deleted successfully";
                return RedirectToPage("Index");
                }
            return Page();
        }
    }
}
