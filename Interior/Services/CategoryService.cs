﻿using Interior.Enums;
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
    public class CategoryService : ICategoryService
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

        public async Task<(IEnumerable<Category>, int count)> GetAllCategoriesAsync(bool onlySubCategories = false, bool onlyCategories = false)
        {
            try
            {
                var baseModel = _context.Categories
                    .Include(s => s.ContentsAttachment).ThenInclude(s => s.Content)
                    .AsNoTracking();
                IList<Category> model;
                if (onlySubCategories)
                    model = await baseModel.Where(s => s.ParentId != null).ToListAsync();
                else if (onlyCategories)
                    model = await baseModel.Where(s => s.ParentId == null).ToListAsync();
                else
                    model = await baseModel.ToListAsync();

                return (model, model.Count);

            }
            catch (Exception e)
            {
                throw;
            }

        }
        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.Include(s => s.ContentsAttachment).ThenInclude(s => s.Content)
            .Include(s => s.FilesAttachment).ThenInclude(s => s.File).AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);

        }
        public async Task<ResultCode> UpdateCategoryAsync(Category category)
        {
            try
            {
                var currentCategory = await _context.Categories
                    .AsNoTracking().SingleOrDefaultAsync(n => n.Id == category.Id);
                if (currentCategory == null)
                    return ResultCode.Error;
                _context.Categories.Update(category);
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
