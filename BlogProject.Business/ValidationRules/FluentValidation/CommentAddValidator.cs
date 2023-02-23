using BlogProject.DTO.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.ValidationRules.FluentValidation
{
    public class CommentAddValidator: AbstractValidator<CommentAddDto>
    {
        public CommentAddValidator()
        {
            RuleFor(x => x.AuthorEmail).NotEmpty().WithMessage("E-mail alanı boş geçilemez.");
            RuleFor(x => x.AuthorName).NotEmpty().WithMessage("Ad alanı boş geçilemez.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama alanı boş geçilemez.");
        }
    }
}
