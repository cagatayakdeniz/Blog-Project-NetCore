using BlogProject.DTO.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.ValidationRules.FluentValidation
{
    public class CategoryAddValidator: AbstractValidator<CategoryAddDto>
    {
        public CategoryAddValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş geçilemez");
        }
    }
}
