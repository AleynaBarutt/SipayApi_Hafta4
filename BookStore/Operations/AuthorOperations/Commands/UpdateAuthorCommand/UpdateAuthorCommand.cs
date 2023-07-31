using BookStore.DBOperations;
using System;
using System.Linq;

namespace BookStore.Operations.AuthorOperations.Commands.UpdateAuthorCommand
{
    public class UpdateAuthorCommand
    {
        public int AuthorID { get; set; }
        public UpdateAuthorViewModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public UpdateAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            //AuthorID ile eşleşen yazarı veritabanında aranır. Eğer böyle bir yazar varsa,
            //author değişkenine atanır; yoksa null olur.
            var author = _dbContext.Authors.SingleOrDefault(x => x.AuthorID == AuthorID);
            if (author is null)
            {
                throw new InvalidOperationException("The author to be deleted could not be found.");
            }
            //Yazarda  güncellenecek kısımlar eşleştirilir ve son olarak SaveChanges ile veritabanına kaydedilir.
            author.Name = Model.Name == default ? author.Name : Model.Name;
            author.Surname = Model.Surname == default ? author.Surname : Model.Surname;
            author.DateOfBirth = Convert.ToDateTime(Model.DateOfBirth);

            _dbContext.Authors.Update(author);
            _dbContext.SaveChanges();
        }
        public class UpdateAuthorViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime DateOfBirth { get; set; }
        }
    }
}
