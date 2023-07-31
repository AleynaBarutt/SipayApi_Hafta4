using BookStore.Operations.GenreOperations.Commands.DeleteGenre;
using FluentValidation;

namespace BookStore.Validations.DeleteGenre
{
    public class DeleteGenreValidator : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreValidator()
        {
            RuleFor(x => x.GenreID).NotEmpty().GreaterThan(0);
        }
    }
}
