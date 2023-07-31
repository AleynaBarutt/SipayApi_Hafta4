using BookStore.Operations.GenreOperations.Commands.CreateGenre;
using FluentValidation;

namespace BookStore.Validations.CreateGenre
{
    public class CreateGenreValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreValidator()
        {
            RuleFor(g => g.Model.Name).NotNull().MinimumLength(2);
        }
    }
}
