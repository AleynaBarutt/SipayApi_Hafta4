using BookStore.DBOperations;
using System;
using System.Linq;

namespace BookStore.Operations.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreID { get; set; }
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {  
            //Eğer GenreID'ye sahip bir Genre veritabanında bulunursa, bu sorgu o genre nesnesini döndürecektir.
            //Eğer GenreID'ye sahip veri veritabanında bulunmazsa, SingleOrDefault metodu null değeri döndürür.
            var genre = _context.Genres.SingleOrDefault(x => x.GenreID == GenreID);

            if (genre is null)
            {
                throw new InvalidOperationException("The Genre Id to be deleted could not be found.");
            }

            _context.Genres.Remove(genre); //GenreID eşitse sil
            _context.SaveChanges();
        }
    }
}
