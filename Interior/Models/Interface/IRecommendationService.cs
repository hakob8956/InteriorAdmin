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
        Task<ResultCode> AddRecommendation(Recommendation recommendation);
        Task<ResultCode> DeleteRecommendation(Recommendation recommendation);
        Task<ResultCode> UpdateRecommendation(Recommendation recommendation);
        Task<IEnumerable<Recommendation>> GetAllRecommendations();
    }
}
