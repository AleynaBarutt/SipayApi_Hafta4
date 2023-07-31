using AutoMapper;
using BookStore.DBOperations;
using BookStore.Operations.BookOperations.Queries.GetBooks;
using BookStore.Operations.GenreOperations.Commands.CreateGenre;
using BookStore.Operations.GenreOperations.Commands.DeleteGenre;
using BookStore.Operations.GenreOperations.Commands.UpdateGenre;
using BookStore.Operations.GenreOperations.Queries.GetGenreDetail;
using BookStore.Operations.GenreOperations.Queries.GetGenres;
using BookStore.Validations.CreateGenre;
using BookStore.Validations.DeleteGenre;
using BookStore.Validations.GetGenreDetail;
using BookStore.Validations.UpdateGenre;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using static BookStore.Operations.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static BookStore.Operations.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        public readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            _dbcontext = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_dbcontext, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("id")]
        public ActionResult GetByID(int id)
        {
            GenreDetailViewModel result;
            GetGenreDetailQuery query = new GetGenreDetailQuery(_dbcontext, _mapper);
            try
            {
                query.GenreID = id;
                GetGenreDetailValidator validations = new GetGenreDetailValidator();
                validations.ValidateAndThrow(query);
                result = query.Handle();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreViewModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_dbcontext, _mapper);
            try
            {
                command.Model = newGenre;
                CreateGenreValidator validations = new CreateGenreValidator();
                validations.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreViewModel updateGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_dbcontext, _mapper);
            try
            {
                command.GenreId = id;
                command.Model = updateGenre;
                UpdateGenreValidator validations = new UpdateGenreValidator();
                validations.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_dbcontext);
            try
            {
                command.GenreID = id;
                DeleteGenreValidator validations = new DeleteGenreValidator();
                validations.ValidateAndThrow(command);
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
