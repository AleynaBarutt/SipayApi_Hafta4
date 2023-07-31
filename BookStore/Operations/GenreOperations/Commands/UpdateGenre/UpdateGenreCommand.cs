using AutoMapper;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore.Operations.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreViewModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateGenreCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {

            //GenreID ile eşleşen veri veritabanında aranır. Eğer böyle bir veri varsa,
            //genre değişkenine atanır; yoksa null olur.
            var genre = _dbContext.Genres.SingleOrDefault(g => g.GenreID == GenreId);

            if (genre is null)
                throw new InvalidOperationException("The Genre Id to be deleted could not be found.");
            if (_dbContext.Genres.Any(g => genre.Name.ToLower() == Model.Name.ToLower() && g.GenreID == GenreId))
                throw new InvalidOperationException("Genre Id is already existing.");

            // Genrede  güncellenecek kısımlar eşleştirilir ve son olarak SaveChanges ile veritabanına kaydedilir.
            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) == default ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;
            _dbContext.Genres.Update(genre);
            _dbContext.SaveChanges();
        }
        public class UpdateGenreViewModel
        {
            public string Name { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
