using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_store_API.Contracts;
using Movie_store_API.Data;
using Movie_store_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store_API.Controllers
{
    /// <summary>
    /// EndPoint used to interact with the Movies in the movie stores database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepo;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository movieRepo, IMapper mapper)
        {
            _movieRepo = movieRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns List of Movies
        /// </summary>
        /// <returns>List of Movies</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAllMovies()
        {
            try
            {
                var movies = await _movieRepo.FindAll();
                var moviesDto = _mapper.Map<IList<MovieDTO>>(movies);

                return Ok(moviesDto);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Something went wrong. please contact your admin");
            }

        }

        /// <summary>
        /// Get A single Movie details
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A movie Record</returns>

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetMovie(int id)
        {
            try
            {
                var exists = await _movieRepo.Exists(id);
                if (!exists)
                {
                    ModelState.AddModelError("", "Movie not found");
                    return NotFound(ModelState);
                }

                var book = await _movieRepo.FindById(id);
                var bookDto = _mapper.Map<MovieDTO>(book);

                return Ok(bookDto);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Something went wrong. please contact your admin");
            }
            
        }
        /// <summary>
        /// Create Movie Record
        /// </summary>
        /// <param name="movieDto"></param>
        /// <returns></returns>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CreateMovie([FromBody] CreateMovieDTO movieDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (movieDto == null)
                {
                    return BadRequest(ModelState);
                }

                var movieObj = _mapper.Map<Movie>(movieDto);

                var success = await _movieRepo.Create(movieObj);

                if (!success)
                {
                    ModelState.AddModelError("", "Something went wrong when creating record");
                    return StatusCode(500, ModelState);
                }

                return Created("Movie was created", new { movieObj });

            }

            catch
            {
                return StatusCode(500, "Something went wrong. please contact your admin");

            }
           


        }


        /// <summary>
        /// Update Movie Details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="MovieDto"></param>
        /// <returns></returns>

        

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public async Task<IActionResult> UpdateMovie(int id,[FromBody] UpdateMovieDTO MovieDto)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != MovieDto.Id || MovieDto == null)
                {
                    return BadRequest(ModelState);
                }

                var exists = await _movieRepo.Exists(id);
                if (!exists)
                {
                    return NotFound();
                }

                var movie = _mapper.Map<Movie>(MovieDto);
                var success = await _movieRepo.Update(movie);

                if (!success)
                {
                    ModelState.AddModelError("", "Something went wrong when updating the record");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }
            catch(Exception e)
            {
                return StatusCode(500, "Something went wrong.Please contact your admin");

            }

        }

        /// <summary>
        /// Delete Movie from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {

                var exists = await _movieRepo.Exists(id);

                if (!exists)
                {
                    return NotFound();
                }

                var Movie = await _movieRepo.FindById(id);

                var success = await _movieRepo.Delete(Movie);

                if (!success)
                {
                    ModelState.AddModelError("", "Sorry went wrong when deleting Record");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }

            catch(Exception e)
            {
                return StatusCode(500, "Something went wrong.Please contact your admin");
            }


        }




    }
}
