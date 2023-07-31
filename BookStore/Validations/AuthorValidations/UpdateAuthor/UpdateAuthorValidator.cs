using BookStore.Operations.AuthorOperations.Commands.UpdateAuthorCommand;
using FluentValidation;
using System;

namespace BookStore.Validations.AuthorValidations.UpdateAuthor
{
    public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorValidator()
        {
            RuleFor(a => a.Model.Name).MinimumLength(2).NotEmpty();
            RuleFor(a => a.Model.Surname).MinimumLength(2).NotEmpty();
            RuleFor(a => a.Model.DateOfBirth.Date).LessThan(DateTime.Now.Date);
        }
    }
}
