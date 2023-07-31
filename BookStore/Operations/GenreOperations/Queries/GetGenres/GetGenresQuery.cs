using AutoMapper;
using BookStore.DBOperations;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Operations.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
        {
            _dbcontext = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            //GenreID ile eşleşen ver veritabanında aranır ve genreList değişkenine atanır.
            var genreList = _dbcontext.Genres.Where(x => x.IsActive).OrderBy(x => x.GenreID).ToList();
            List<GenresViewModel> viewModel = _mapper.Map<List<GenresViewModel>>(genreList);

            return viewModel;
        }
    }

    public class GenresViewModel
    {
        public int GenreID { get; set; }
        public string Name { get; set; }
    }
}
