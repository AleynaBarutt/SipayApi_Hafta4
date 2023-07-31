using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore.Operations.AuthorOperations.Commands.CreateAuthorCommand
{
    public class CreateAuthorCommand
    {
        //Bu sınıfın üyeleri (Model) yazar oluşturma işlemiyle ilgili
        //verilere erişebilir veya bu verileri güncelleyebilir.

        public CreateAuthorViewModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateAuthorCommand(IMapper mapper, BookStoreDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname);
            if (author is not null)
            {
                throw new InvalidOperationException("The author is already available.");
            }
            author = _mapper.Map<Author>(Model); //Modelden gelen yazar verilerini maple
            _dbContext.Authors.Add(author);// Yazar listesine ekle
            _dbContext.SaveChanges();//Özellikleri kaydet
        }
    }

    public class CreateAuthorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

