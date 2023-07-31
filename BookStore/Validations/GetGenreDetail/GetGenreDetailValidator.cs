using BookStore.Operations.GenreOperations.Queries.GetGenreDetail;
using FluentValidation;

namespace BookStore.Validations.GetGenreDetail
{
    public class GetGenreDetailValidator : AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailValidator()
        {
            RuleFor(x => x.GenreID).NotEmpty().GreaterThan(0);
        }
    }
}
