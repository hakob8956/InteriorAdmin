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
    public class RecommendationService : IRecommendationService
    {
        private readonly ApplicationContext _context;
        public RecommendationService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ResultCode> AddRecommendationAsync(Recommendation recommendation)
        {
            try
            {
                recommendation.Id = 0;
                _context.Recommendations.Add(recommendation);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }

        public async Task<ResultCode> DeleteRecommendationAsync(Recommendation recommendation)
        {
            try
            {
                var currentRecommendation = await _context.Recommendations.SingleOrDefaultAsync(n => n.Id == recommendation.Id);
                if (currentRecommendation == null)
                    return ResultCode.Error;
                _context.Recommendations.Remove(currentRecommendation);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }
        public async Task<Recommendation> GetRecommendationById(int id)
        {
            return await _context.Recommendations.Include(s => s.ContentsAttachment).ThenInclude(s => s.Content).Include(s => s.FilesAttachment).ThenInclude(s => s.File).AsNoTracking().SingleOrDefaultAsync(i => id == i.Id);
        }

        public async Task<(IEnumerable<Recommendation>, int count)> GetAllRecommendationsAsync()
        {
            try
            {
                var model = await _context.Recommendations.Include(s => s.ContentsAttachment).ThenInclude(s=>s.Content).AsNoTracking().ToListAsync();
                return (model, model.Count);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<ResultCode> UpdateRecommendationAsync(Recommendation recommendation)
        {
            try
            {
                var currentRecommendation = await _context.Recommendations.AsNoTracking().SingleOrDefaultAsync(n => n.Id == recommendation.Id);
                if (currentRecommendation == null)
                    return ResultCode.Error;
                _context.Recommendations.Update(recommendation);
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
