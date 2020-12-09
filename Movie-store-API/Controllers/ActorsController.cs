using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    /// EndPoint used to interact with the Actors in the movie stores database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class ActorsController : ControllerBase
    {
        private readonly IActorRepository _actorRepo;
        private readonly IMapper _mapper;

        public ActorsController(IActorRepository actorRepo, IMapper mapper)
        {
            _actorRepo = actorRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of all Actors
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetActorsAsync()
        {
            try
            {
                var actorsObj = await _actorRepo.FindAll();
                var actorsDTO = _mapper.Map<IList<ActorDTO>>(actorsObj);
                return Ok(actorsDTO);
            }
            catch (Exception E)
            {
                return StatusCode(500, "Something went wrong. please contact your admin");
            }

        }
        /// <summary>
        /// Get an Actor by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Actor Record</returns>

        [HttpGet("{id:int}", Name = "GetActor")]
        [ProducesResponseType(statusCode: 200, Type = typeof(ActorDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetActor(int id)
        {
            try
            {
                var actorExists = await _actorRepo.Exists(id);
                if (!actorExists)
                {
                    return NotFound();
                }
                var actor = await _actorRepo.FindById(id);
                var actorDTO = _mapper.Map<ActorDTO>(actor);
                return Ok(actorDTO);

            }
            catch (Exception E)
            {
                return StatusCode(500, "Something went wrong. please contact your admin");
            }
        }

        /// <summary>
        /// Create a Actor Record
        /// </summary>
        [HttpPost]
        [Authorize(Roles ="Administrator")] 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CreateActor([FromBody] CreateActorDTO actorDto)
        {
            try
            {
                if (actorDto == null)
                {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var actorObj = _mapper.Map<Actor>(actorDto);

                var success = await _actorRepo.Create(actorObj);

                if (!success)
                {
                    ModelState.AddModelError("", $"Something went wrong when creating the record");
                    return StatusCode(500, ModelState);
                }

                return Created("Succesfully Created", new { actorObj });

            }
            catch (Exception E)
            {
                return StatusCode(500, "Something went wrong. Please contact your admin");
            }

        }

        /// <summary>
        /// Update Actor Details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actorDto"></param>
        /// <returns></returns>

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public async Task<IActionResult> UpdateActor(int id,[FromBody] UpdateActorDto actorDto)
        {
            try
            {
                if(id<1 || actorDto == null || id != actorDto.Id)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var exists = await _actorRepo.Exists(id);

                if (!exists)
                {
                    return NotFound();
                }

                var actorObj = _mapper.Map<Actor>(actorDto);
                var success = await _actorRepo.Update(actorObj);

                if (!success)
                {
                    ModelState.AddModelError("", "Something went wrong when deleting the record");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }

            catch(Exception e)
            {
                return StatusCode(500, "Something went wrong. Please contact your admin");

            }
            

        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]


        public async Task<IActionResult> DeleteActor(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }

                var exists = await _actorRepo.Exists(id);

                if (!exists)
                {
                    return NotFound();

                }

                var actor =await _actorRepo.FindById(id);


                var success = await _actorRepo.Delete(actor);

                if (!success)
                {
                    ModelState.AddModelError("", "Something went wrong when Deleting the record");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }

            catch (Exception e)
            {
                return StatusCode(500, "Something went wrong. Please contact your admin");

            }


        }



    }
}
