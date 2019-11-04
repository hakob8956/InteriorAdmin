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

        public async Task<ResultCode> AddCategoryAsync(Category category)
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

        public async Task<ResultCode> DeleteCategoryAsync(Category category)
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

        public async Task<(IEnumerable<Category>, int count)> GetAllCategoriesAsync()
        {
            var model = await _context.Categories.AsNoTracking().ToListAsync();
            return (model, model.Count);
        }

        public async Task<ResultCode> UpdateCategoryAsync(Category category)
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
