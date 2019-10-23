using Interior.Enums;
using Interior.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Interface
{
    public interface IRecommendationService
    {
        Task<ResultCode> AddRecommendationAsync(Recommendation recommendation);
        Task<ResultCode> DeleteRecommendationAsync(Recommendation recommendation);
        Task<ResultCode> UpdateRecommendationAsync(Recommendation recommendation);
        Task<IEnumerable<Recommendation>> GetAllRecommendationsAsync();
    }
}
