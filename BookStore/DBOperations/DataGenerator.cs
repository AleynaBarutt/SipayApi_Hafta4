﻿using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BookStore.DBOperations
{
    public class DataGenerator
    {
        public static void Initializer(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;   
                }
                context.Genres.AddRange(
                new Genre()
                {
                    Name = "Personel Growth",
                    IsActive = true,
                },
                new Genre()
                {
                    Name = "Science Finction",
                    IsActive = true,
                },
                new Genre()
                {
                    Name = "Noval",
                    IsActive = true,
                });

                context.Books.AddRange(
                new Book()
                {
                    BookID = 1,
                    Title = "Lean Startup",
                    GenreID = 0, // Personal Growth
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12)

                },
                new Book
                {
                    BookID = 2,
                    Title = "Herland",
                    GenreID = 1, // Science Fiction
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23)
                },
                new Book
                {
                    BookID = 3,
                    Title = "Dune",
                    GenreID = 2, // Noval
                    PageCount = 540,
                    PublishDate = new DateTime(2001, 12, 21)
                });

                context.SaveChanges(); 
            };
        }
    }
}
