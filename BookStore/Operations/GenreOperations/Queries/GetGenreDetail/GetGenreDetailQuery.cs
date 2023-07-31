using AutoMapper;
using BookStore.DBOperations;
using System;
using System.Linq;

namespace BookStore.Operations.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreID { get; set; }
        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _dbcontext = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            //GenreID ile eşleşen veri veritabanında aranır. Eğer böyle bir veri varsa,
            //genre değişkenine atanır; yoksa null olur.
            var genre = _dbcontext.Genres.SingleOrDefault(x => x.IsActive && x.GenreID == GenreID);
            //Eğer genre değişkeni null ise, yani genre bulunamamışsa, InvalidOperationException hatası fırlatılır ve işlem sonlanır.
            if (genre is null)
            {
                throw new InvalidOperationException("The Genre Id to be deleted could not be found.");
            }
            //Veri bulunursa, GenreDetailViewModel örneği oluşturulur ve döndürülür: 
            GenreDetailViewModel viewModel = _mapper.Map<GenreDetailViewModel>(genre);

            return viewModel;
        }
    }

    public class GenreDetailViewModel
    {
        public int GenreID { get; set; }
        public string Name { get; set; }
    }
}
