using BookStore.Operations.AuthorOperations.Commands.CreateAuthorCommand;
using FluentValidation;
using System;

namespace BookStore.Validations.AuthorValidations.CreateAuthor
{
    public class CreateAuthorValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorValidator()
        {
            RuleFor(a => a.Model.Name).MinimumLength(2).NotEmpty();
            RuleFor(a => a.Model.Surname).MinimumLength(2).NotEmpty();
            RuleFor(a => a.Model.DateOfBirth.Date).LessThan(DateTime.Now.Date);
        }
    }
}
