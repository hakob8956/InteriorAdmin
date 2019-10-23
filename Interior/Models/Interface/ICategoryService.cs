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
        Task<ResultCode> AddCategory(Category category);
        Task<ResultCode> DeleteCategory(Category category);
        Task<ResultCode> UpdateCategory(Category category);
        Task<IEnumerable<Category>> GetAllCategories();
    }
}
