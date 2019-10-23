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
    public class RecommendationService : IRecommendationService
    {
        private readonly ApplicationContext _context;
        public RecommendationService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ResultCode> AddRecommendation(Recommendation recommendation)
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

        public async Task<ResultCode> DeleteRecommendation(Recommendation recommendation)
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

        public async Task<IEnumerable<Recommendation>> GetAllRecommendations()
        {
            return await _context.Recommendations.ToListAsync();
        }

        public async Task<ResultCode> UpdateRecommendation(Recommendation recommendation)
        {
            try
            {
                var currentRecommendation = await _context.Recommendations.SingleOrDefaultAsync(n => n.Id == recommendation.Id);
                if (currentRecommendation == null)
                    return ResultCode.Error;
                _context.Recommendations.Update(currentRecommendation);
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
