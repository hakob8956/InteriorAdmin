using Interior.Enums;
using Interior.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Interface
{
    public interface ICategoryService
    {
        Task<ResultCode> AddCategoryAsync(Category category);
        Task<ResultCode> DeleteCategoryAsync(Category category);
        Task<ResultCode> UpdateCategoryAsync(Category category);
        Task<(IEnumerable<Category>, int count)> GetAllCategoriesAsync();
    }
}
