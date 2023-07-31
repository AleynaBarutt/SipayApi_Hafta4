using BookStore.Operations.BookOperations.Commands.DeleteBook;
using FluentValidation;

namespace BookStore.Validations.BookValidations.DeleteBook
{
    public class DeleteBookValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookValidator()
        {
            RuleFor(x => x.BookID).NotEmpty().GreaterThan(0).WithMessage("Please provide a valid BookID.");
        }
    }
}
