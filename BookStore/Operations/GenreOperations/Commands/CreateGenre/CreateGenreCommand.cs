using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using System;
using System.Linq;

namespace BookStore.Operations.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        //Bu sınıfın üyeleri (Model) Genre oluşturma işlemiyle ilgili
        //verilere erişebilir veya bu verileri güncelleyebilir.
        public CreateGenreViewModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateGenreCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(g => g.Name == Model.Name);

            if (genre is not null)
                throw new InvalidOperationException("Genre is already added!");

            genre = _mapper.Map<Genre>(Model);//Modelden gelen genre verilerini maple

            _dbContext.Genres.Add(genre);// Genre listesine ekle
            _dbContext.SaveChanges(); //Özellikleri kaydet
        }
        public class CreateGenreViewModel
        {
            public string Name { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
