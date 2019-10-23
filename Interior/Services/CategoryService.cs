using Interior.Enums;
using Interior.Models.EFContext;
using Interior.Models.Entities;
using Interior.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ApplicationContext _context;
        public CategoryService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ResultCode> AddCategory(Category category)
        {
            try
            {
                category.Id = 0;
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }

        public async Task<ResultCode> DeleteCategory(Category category)
        {
            try
            {
                var currentCategory = await _context.Categories.SingleOrDefaultAsync(n => n.Id == category.Id);
                if (currentCategory == null)
                    return ResultCode.Error;
                _context.Categories.Remove(currentCategory);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<ResultCode> UpdateCategory(Category category)
        {
            try
            {
                var currentCategory = await _context.Categories.SingleOrDefaultAsync(n => n.Id == category.Id);
                if (currentCategory == null)
                    return ResultCode.Error;
                _context.Categories.Update(currentCategory);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }
    }
}
