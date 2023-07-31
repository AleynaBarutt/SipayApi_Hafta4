using BookStore.Operations.BookOperations.Queries.GetBooks;
using FluentValidation;

namespace BookStore.Validations.BookValidations.GetBooks
{
    public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
    {
        public GetByIdQueryValidator()
        {
            RuleFor(x => x.BookID).NotEmpty().GreaterThan(0).WithMessage("Please provide a valid BookID.");
        }
    }
}
