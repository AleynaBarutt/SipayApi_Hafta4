using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore.Operations.AuthorOperations.Commands.DeleteAuthorCommand
{
    public class DeleteAuthorCommand
    {
        public int AuthorID { get; set; }
        private readonly BookStoreDbContext _dbcontext;
        public DeleteAuthorCommand(BookStoreDbContext context)
        {
            _dbcontext = context;
        }

        public void Handle()
        {
            //Eğer AuthorID'ye sahip bir yazar veritabanında bulunursa, bu sorgu o yazar nesnesini döndürecektir.
            //Eğer AuthorID'ye sahip yazar veritabanında bulunmazsa, SingleOrDefault metodu null değeri döndürür.
            var author = _dbcontext.Authors.SingleOrDefault(x => x.AuthorID == AuthorID);

            if (author is null)
            {
                throw new InvalidOperationException("The author to be deleted could not be found.");
            }

            if (author.Book != null)
            {
                throw new InvalidOperationException("You must first delete the book associated with this author.");
            }

            _dbcontext.Authors.Remove(author); //AuthorID eşitse sil
            _dbcontext.SaveChanges();
        }
    }
}
