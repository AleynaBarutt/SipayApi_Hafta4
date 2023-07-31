using BookStore.Operations.BookOperations.Queries.GetBookDetail;
using FluentValidation;

namespace BookStore.Validations.BookValidations.GetBookDetail
{
    public class GetBookDetailValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailValidator()
        {
            RuleFor(x => x.BookID).NotEmpty().GreaterThan(0).WithMessage("Please provide a valid BookID.");
        }
    }
}
