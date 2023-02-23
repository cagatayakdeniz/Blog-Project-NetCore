using BlogProject.Business.Abstract;
using BlogProject.Business.Concrete;
using BlogProject.Business.Utilities.Jwt;
using BlogProject.Business.ValidationRules.FluentValidation;
using BlogProject.DataAccess.Abstract;
using BlogProject.DataAccess.Concrete.EfCore.Repositories;
using BlogProject.DTO.DTOs;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.Containers.MicrosoftIoc
{
    public static class CustomExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));

            services.AddScoped<IBlogService, BlogManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<ICommentService, CommentManager>();

            services.AddScoped<IBlogDal, EfBlogRepository>();
            services.AddScoped<ICategoryDal, EfCategoryRepository>();
            services.AddScoped<IAppUserDal, EfAppUserRepository>();
            services.AddScoped<ICommentDal, EfCommentRepository>();

            services.AddScoped<IJwtService, JwtManager>();

            services.AddTransient<IValidator<AppUserLoginDto>, AppUserLoginValidator>();
            services.AddTransient<IValidator<CategoryAddDto>, CategoryAddValidator>();
            services.AddTransient<IValidator<CategoryUpdateDto>, CategoryUpdateValidator>();
            services.AddTransient<IValidator<CategoryBlogDto>, CategoryBlogValidator>();
            services.AddTransient<IValidator<CommentAddDto>, CommentAddValidator>();
        }
    }
}
