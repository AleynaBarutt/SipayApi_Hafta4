using AutoMapper;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore.Operations.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorID { get; set; }
        public readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;

        public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            //AuthorID ile eşleşen yazarı veritabanında aranır. Eğer böyle bir yazar varsa,
            //author değişkenine atanır; yoksa null olur.
            var author = _context.Authors.Include(x => x.Book).SingleOrDefault(x => x.AuthorID == AuthorID);
            if (author is null)
            {
                throw new InvalidOperationException("The author to be deleted could not be found.");
            }
            //Yazar bulunursa, AuthorDetailViewModel örneği oluşturulur ve döndürülür: 
            return _mapper.Map<AuthorDetailViewModel>(author);
        }
        public class AuthorDetailViewModel
        {
            public int AuthorID { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Book { get; set; }
        }
    }
}
