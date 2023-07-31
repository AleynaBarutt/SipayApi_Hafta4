using AutoMapper;
using static BookStore.Operations.BookOperations.Commands.CreateBook.CreateBookCommand;
using BookStore.Operations.BookOperations.Queries.GetBooks;
using BookStore.Entities;
using BookStore.Operations.GenreOperations.Queries.GetGenres;
using BookStore.Operations.GenreOperations.Queries.GetGenreDetail;
using static BookStore.Operations.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static BookStore.Operations.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;
using BookStore.Operations.BookOperations.Commands.CreateBook;
using BookStore.Operations.BookOperations.Queries.GetBookDetail;
using static BookStore.Operations.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<CreateBookViewModel, Book>();
            CreateMap<UpdateBookModel, Book>();

            // Genre
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreViewModel, Genre>();
            CreateMap<UpdateGenreViewModel, Genre>();
        }
    }
}
