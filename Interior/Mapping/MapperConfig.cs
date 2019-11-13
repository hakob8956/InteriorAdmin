﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Interior.Models.Entities;
using Interior.Models.Interface;
using Interior.Models.ViewModels;

namespace Interior.Mapping
{
    public class MapperConfig
    {

        public static IMapper CreateMapper()
        {
            var mapperConfig = new MapperConfiguration(conf =>
            {
                conf.CreateMap<UserRegisterByAdminViewModel, User>();
                conf.CreateMap<UserRegisterByAdminViewModel, User>().ReverseMap();

                conf.CreateMap<UserRegisterByUserViewModel, User>();
                conf.CreateMap<UserRegisterByUserViewModel, User>().ReverseMap();

                conf.CreateMap<UserShowViewModel, User>();
                conf.CreateMap<UserShowViewModel, User>().ReverseMap();


                conf.CreateMap<UserLoginViewModel, User>();
                conf.CreateMap<UserLoginViewModel, User>().ReverseMap();

                conf.CreateMap<UserUpdateByAdminViewModel, User>();
                conf.CreateMap<UserUpdateByAdminViewModel, User>().ReverseMap();

                conf.CreateMap<LanguageShowViewModel, Language>();
                conf.CreateMap<LanguageShowViewModel, Language>().ReverseMap();




                conf.CreateMap<CategoryShowViewModel, Category>();
                conf.CreateMap<CategoryShowViewModel, Category>().ReverseMap()
                .ForMember(s=>s.Contents,opt=>opt.MapFrom(c=>c.ContentsAttachment.Select(s=>s.Content)));

                conf.CreateMap<ContentViewModel, Content>();
                conf.CreateMap<ContentViewModel, Content>().ReverseMap();

                conf.CreateMap<ShopShowViewModel, Shop>();
                conf.CreateMap<ShopShowViewModel, Shop>().ReverseMap()
                .ForMember(s => s.Contents, opt => opt.MapFrom(c => c.ContentsAttachment.Select(s => s.Content)));

                conf.CreateMap<BrandShowViewModel, Brand>();
                conf.CreateMap<BrandShowViewModel, Brand>().ReverseMap()
                .ForMember(s => s.Contents, opt => opt.MapFrom(c => c.ContentsAttachment.Select(s => s.Content)));

                conf.CreateMap<Brand,CreateRequestBrandViewModel>().ForMember(s => s.Contents, opt => opt.MapFrom(c => c.ContentsAttachment.Select(s => s.Content)));
                conf.CreateMap<Category, CreateRequestCategoryViewModel>().ForMember(s => s.Contents, opt => opt.MapFrom(c => c.ContentsAttachment.Select(s => s.Content)));
                conf.CreateMap<Shop, CreateRequestShopViewModel>().ForMember(s => s.Contents, opt => opt.MapFrom(c => c.ContentsAttachment.Select(s => s.Content)));


                conf.CreateMap<Interior.Models.Entities.Interior, InteriorShowViewModel>();
                conf.CreateMap<Interior.Models.Entities.Interior, InteriorShowViewModel>().ReverseMap();

               
            });

            return mapperConfig.CreateMapper();
        }
    }
}
