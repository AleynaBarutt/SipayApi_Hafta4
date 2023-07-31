using BookStore.Operations.AuthorOperations.Commands.DeleteAuthorCommand;
using FluentValidation;

namespace BookStore.Validations.AuthorValidations.DeleteAuthor
{
    public class DeleteAuthorValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorValidator()
        {
            RuleFor(cmd => cmd.AuthorID).GreaterThan(0);
        }
    }
}
