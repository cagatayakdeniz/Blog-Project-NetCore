using AutoMapper;
using BlogProject.DTO.DTOs;
using BlogProject.Entities.Concrete;
using BlogProject.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.WebApi.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<BlogListDto, Blog>();
            CreateMap<Blog, BlogListDto>();
            CreateMap<BlogAddModel, Blog>();
            CreateMap<Blog, BlogAddModel>();
            CreateMap<BlogUpdateModel, Blog>();
            CreateMap<Blog, BlogUpdateModel>();

            CreateMap<CategoryListDto, Category>();
            CreateMap<Category, CategoryListDto>();
            CreateMap<CategoryAddDto, Category>();
            CreateMap<Category, CategoryAddDto>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryUpdateDto>();

            CreateMap<CommentListDto, Comment>();
            CreateMap<Comment, CommentListDto>();
            CreateMap<CommentAddDto, Comment>();
            CreateMap<Comment, CommentAddDto>();
        }
    }
}
