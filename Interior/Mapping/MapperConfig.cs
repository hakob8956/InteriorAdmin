using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Interior.Enums;
using Interior.Models.Entities;
using Interior.Models.Interface;
using Interior.Models.ViewModels;
using Newtonsoft.Json;

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

                conf.CreateMap<Category, CategoryShowViewModel>()
                .ForMember(s => s.Contents, opt => opt.MapFrom(c => c.ContentsAttachment.Select(s => s.Content).Where(s => s.ContentType == (byte)ContentType.Name)));

                conf.CreateMap<ContentViewModel, Content>();
                conf.CreateMap<ContentViewModel, Content>().ReverseMap();

                conf.CreateMap<Shop, ShopShowViewModel>()
                .ForMember(s => s.Contents, opt => opt.MapFrom(c => c.ContentsAttachment.Select(s => s.Content).Where(s => s.ContentType == (byte)ContentType.Name)));

                conf.CreateMap<Brand, BrandShowViewModel>()
                .ForMember(s => s.Contents, opt => opt.MapFrom(c => c.ContentsAttachment.Select(s => s.Content).Where(s => s.ContentType == (byte)ContentType.Name)));

                conf.CreateMap<Recommendation, RecommendationShowViewModel>()
                .ForMember(s => s.Contents, opt => opt.MapFrom(c => c.ContentsAttachment.Select(s => s.Content).Where(s => s.ContentType == (byte)ContentType.Name)));

                conf.CreateMap<Recommendation, CreateRequestRecommendationViewModel>()
               .ForMember(s => s.Contents, opt => opt.MapFrom(c => c.ContentsAttachment.Select(s => s.Content).Where(s => s.ContentType == (byte)ContentType.Name)));
                conf.CreateMap<Recommendation, CreateResponseRecommendationViewModel>().ReverseMap();

                conf.CreateMap<Brand, CreateRequestBrandViewModel>()
                .ForMember(s => s.Contents, opt => opt.MapFrom(c => c.ContentsAttachment.Select(s => s.Content).Where(s => s.ContentType == (byte)ContentType.Name)));

                conf.CreateMap<Category, CreateRequestCategoryViewModel>()
                .ForMember(s => s.Contents, opt => opt.MapFrom(c => c.ContentsAttachment.Select(s => s.Content).Where(s => s.ContentType == (byte)ContentType.Name)));

                conf.CreateMap<Shop, CreateRequestShopViewModel>()
                .ForMember(s => s.Contents, opt => opt.MapFrom(c => c.ContentsAttachment.Select(s => s.Content).Where(s => s.ContentType == (byte)ContentType.Name)));


                conf.CreateMap<Interior.Models.Entities.Interior, InteriorShowViewModel>()
                .ForMember(s => s.Contents, opt => opt.MapFrom(c => c.ContentAttachments.Select(s => s.Content).Where(s => s.ContentType == (byte)ContentType.Name)));

                conf.CreateMap<Models.Entities.Interior, InteriorRequestModel>()
                .ForMember(s => s.NameContent, opt => opt.MapFrom(
                    c => c.ContentAttachments.Select(s => s.Content).Where(s => s.ContentType == (byte)ContentType.Name)))
                .ForMember(s => s.DescriptionContent, opt => opt.MapFrom(
                       c => c.ContentAttachments.Select(s => s.Content).Where(s => s.ContentType == (byte)ContentType.Description))
                 );
                conf.CreateMap<InteriorResponseModel, Models.Entities.Interior>().ForMember(s => s.OptionContents, opt => opt.MapFrom(o => ""));

            });

            return mapperConfig.CreateMapper();
        }
    }
}
