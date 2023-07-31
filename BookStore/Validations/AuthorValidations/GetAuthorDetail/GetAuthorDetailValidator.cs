using BookStore.Operations.AuthorOperations.Queries.GetAuthorDetail;
using FluentValidation;

namespace BookStore.Validations.AuthorValidations.GetAuthorDetail
{
    public class GetAuthorDetailValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailValidator()
        {
            RuleFor(a => a.AuthorID).GreaterThan(0);
        }
    }
}
