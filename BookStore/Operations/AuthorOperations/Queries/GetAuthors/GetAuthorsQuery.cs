using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Operations.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            //// AuthorID ile eşleşen yazar veritabanında aranır ve author değişkenine atanır.
            var author = _dbContext.Authors.Include(x => x.Book).OrderBy(x => x.AuthorID);
            //Eğer author değişkeni null ise, yani yazar bulunamamışsa, InvalidOperationException hatası fırlatılır ve işlem sonlanır.
            if (author is null)
            {
                throw new InvalidOperationException("The author could not be found.");
            }
            //Yazar bulunursa, AuthorslViewModel örneği oluşturulur ve döndürülür: 
            List<AuthorsViewModel> viewModel = _mapper.Map<List<AuthorsViewModel>>(author);
            return viewModel;
        }
        public class AuthorsViewModel
        {
            public int AuthorID { get; set; }
            public string Book { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string DateOfBirth { get; set; }
        }
    }
}
