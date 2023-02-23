using BlogProject.DTO.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.ValidationRules.FluentValidation
{
    public class CategoryBlogValidator: AbstractValidator<CategoryBlogDto>
    {
        public CategoryBlogValidator()
        {
            RuleFor(x => x.BlogId).InclusiveBetween(0, int.MaxValue).WithMessage("Blog Id alanı boş geçilemez.");
            RuleFor(x => x.CategoryId).InclusiveBetween(0, int.MaxValue).WithMessage("Category Id alanı boş geçilemez.");
        }
    }
}
