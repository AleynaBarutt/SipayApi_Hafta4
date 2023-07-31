using AutoMapper;
using BookStore.DBOperations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static BookStore.Operations.BookOperations.Commands.CreateBook.CreateBookCommand;
using static BookStore.Operations.BookOperations.Commands.UpdateBook.UpdateBookCommand;
using BookStore.Operations.BookOperations.Commands.CreateBook;
using BookStore.Operations.BookOperations.Commands.DeleteBook;
using BookStore.Operations.BookOperations.Commands.UpdateBook;
using BookStore.Operations.BookOperations.Queries.GetBooks;
using BookStore.Validations.BookValidations.CreateBook;
using BookStore.Validations.BookValidations.DeleteBook;
using BookStore.Validations.BookValidations.GetBooks;
using BookStore.Validations.BookValidations.UpdateBook;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _dbcontext = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery getBooksQuery = new GetBooksQuery(_dbcontext, _mapper);
            var result = getBooksQuery.Handle();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            BookGetByIdViewModel result;
            GetByIdQuery query = new GetByIdQuery(_dbcontext, _mapper);
            try
            {
                query.BookID = id;
                GetByIdQueryValidator validator = new GetByIdQueryValidator();
                validator.ValidateAndThrow(query);
               
                result = query.Handle();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);

        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookViewModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_dbcontext, _mapper);
            try
            {
                command.Model = newBook;
                CreateBookValidator validator = new CreateBookValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_dbcontext);
            try
            {
                command.BookID = id;
                command.Model = updateBook;
                UpdateBookValidator validator = new UpdateBookValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_dbcontext);

            try
            {
                command.BookID = id;
                DeleteBookValidator validator = new DeleteBookValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}