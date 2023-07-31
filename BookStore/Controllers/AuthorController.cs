using AutoMapper;
using BookStore.DBOperations;
using BookStore.Operations.AuthorOperations.Commands.CreateAuthorCommand;
using BookStore.Operations.AuthorOperations.Commands.DeleteAuthorCommand;
using BookStore.Operations.AuthorOperations.Commands.UpdateAuthorCommand;
using BookStore.Operations.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.Operations.AuthorOperations.Queries.GetAuthors;
using BookStore.Operations.BookOperations.Queries.GetBooks;
using BookStore.Validations.AuthorValidations.CreateAuthor;
using BookStore.Validations.AuthorValidations.DeleteAuthor;
using BookStore.Validations.AuthorValidations.GetAuthorDetail;
using BookStore.Validations.AuthorValidations.UpdateAuthor;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using static BookStore.Operations.AuthorOperations.Commands.UpdateAuthorCommand.UpdateAuthorCommand;
using static BookStore.Operations.AuthorOperations.Queries.GetAuthorDetail.GetAuthorDetailQuery;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        public readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public AuthorController(BookStoreDbContext context, IMapper mapper)
        {
            _dbcontext = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAuthors() 
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_dbcontext, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")] 
        public ActionResult GetAuthorDetail(int id)
        {
            AuthorDetailViewModel result;
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_dbcontext, _mapper);
            try 
            {
                query.AuthorID = id;
                GetAuthorDetailValidator validations = new GetAuthorDetailValidator();
                validations.ValidateAndThrow(query);
                result = query.Handle();
            }

            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);
        }

        [HttpPost] 
        public IActionResult AddAuthor([FromBody] CreateAuthorViewModel newGenre)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_mapper, _dbcontext);
            try
            {
                command.Model = newGenre;
                CreateAuthorValidator validations = new CreateAuthorValidator();
                validations.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPut("{id}")] 
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorViewModel updatedAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_dbcontext);
            try
            {
                command.AuthorID = id;
                command.Model = updatedAuthor;
                UpdateAuthorValidator cv = new UpdateAuthorValidator();
                cv.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("{id}")] 
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_dbcontext);
            try 
            {
                command.AuthorID = id;
                DeleteAuthorValidator cv = new DeleteAuthorValidator(); 
                cv.ValidateAndThrow(command);
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
