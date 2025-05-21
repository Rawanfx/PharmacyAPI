

using Azure;
using projectAPI.Core.DTO;

using projectAPI.Result;

namespace projectAPI.Core.ServiceCotract
{
    public interface ICategoryServices
    {
         List<CategoryDTO> GetAllCategory();
        List<CategoryDTO> Search(string name);
        OperationResult Add(CategoryAddDTO dto);
        OperationResult Delete(int id);
        OperationResult Update(int id, CategoryAddDTO dto);
    }
}
