using BookStore.Operations.GenreOperations.Commands.CreateGenre;
using FluentValidation;

namespace BookStore.Validations.GenreValidations.CreateGenre
{
    public class CreateGenreValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreValidator()
        {
            RuleFor(g => g.Model.Name).NotNull().MinimumLength(2);
        }
    }
}
