using Azure;
using projectAPI.Core.Domain.Entities;
using projectAPI.Core.DTO;
using projectAPI.Core.ServiceCotract;
using projectAPI.Result;
using System.Net.Http.Headers;
namespace projectAPI.Services
{
    public class CategoryServices: ICategoryServices
    {
     private readonly   AppDbContext context;
        public CategoryServices(AppDbContext context) => this.context = context;

        public OperationResult Add(CategoryAddDTO dto)
        {
            if (context.Category.Any(x => x.name == dto.name))
                return new OperationResult() { success =false,message="Category already exist"};
            Category category = new Category() { description = dto.description, name = dto.name };
            try
            {
                context.Category.Add(category);
                context.SaveChanges();
                return new OperationResult() { success = true,message="Category added successfully" };
            }
            catch
            {
                return new OperationResult() { message = "Error while saving in database", success = false };
            }
        }

        public OperationResult Delete(int id)
        {
            Category category = context.Category.FirstOrDefault(x => x.id == id);
            if (category == null)
            {
                return new OperationResult() { message = "Category not found", success = false };
            }
            try
            {
                context.Category.Remove(category);
                context.SaveChanges();
            return new OperationResult() { message = "Category deleted successuflly", success = true };
            }
            catch
            {
                return new OperationResult() { message = "Can't delete this category", success = false };
            }
        }

        public List<CategoryDTO> GetAllCategory()
        {
            List<Category> category = context.Category.ToList();
            List<CategoryDTO> categoryDTOs = new List<CategoryDTO>();
            foreach (var i in category)
            {
                CategoryDTO categoryDTO = new CategoryDTO() { name = i.name };
                categoryDTOs.Add(categoryDTO);
            }
            return categoryDTOs;
        }

        public List<CategoryDTO> Search(string input)
        {
            var res = context.Category
                .Where(x => x.name.Contains(input.ToLower()))
                .Select(x => new CategoryDTO { name = x.name }).ToList();
            return res;
        }

      
        public OperationResult Update(int id, CategoryAddDTO dto)
        {
            Category existingCategory = context.Category.FirstOrDefault(x=>x.id==id);
            if (existingCategory == null)
                return new OperationResult() { message ="category not found",success=false };
            existingCategory.name = dto.name;
            existingCategory.description = dto.description;

            context.SaveChanges();
            return new OperationResult() { message = "category updated successfully", success = true };

        
        }
    }
}
