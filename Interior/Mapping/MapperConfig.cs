using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Interior.Models.Entities;
using Interior.Models.ViewModels;

namespace Interior.Mapping
{
    public class MapperConfig
    {
        public static IMapper CreateMapper()
        {
            var mapperConfig = new MapperConfiguration(conf =>
            {
                conf.CreateMap<UserRegisterViewModel, User>();
                conf.CreateMap<UserRegisterViewModel, User>().ReverseMap();


                conf.CreateMap<UserLoginViewModel, User>();
                conf.CreateMap<UserLoginViewModel, User>().ReverseMap();
   

            });

            return mapperConfig.CreateMapper();
        }
    }
}
