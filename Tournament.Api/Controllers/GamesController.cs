using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tournament.Core.Dto;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;
using Tournament.Services;

namespace Tournament.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IMapper _mapper;
      //  private readonly IUnitOfWork _uow;
        private readonly IServiceManager _serviceManager;

        public GamesController(IServiceManager serviceManager, IMapper mapper, IUnitOfWork uow)
        {
            _serviceManager = serviceManager;
            //_mapper = mapper;
            //_uow = uow;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGame(bool sortByTitle)
        {         
            //var games = _mapper.Map<IEnumerable<GameDto>>(await _uow.GameRepository.GetAllAsync(sortByTitle));
            
            var games = await _serviceManager.GameService.GetAllAsync(sortByTitle);
            return Ok(games);
        }

        // GET: api/Games/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<GameDto>> GetGame(int id)
        //{
        //    var game = _mapper.Map<GameDto>(await _uow.GameRepository.GetAsync(id));

        //    if (game == null)
        //    {
        //        return NotFound();
        //    }

        //    return game;
        //}
        [HttpGet("{id?}")]
        public async Task<ActionResult<GameDto>> GetGame(int id, [FromQuery] string? searchTitle=null)
        {

            var game = (string.IsNullOrEmpty(searchTitle))
                //? _mapper.Map<GameDto>(await _uow.GameRepository.GetAsync(id))
                //: _mapper.Map<GameDto>(await _uow.GameRepository.GetAsync(searchTitle));           
                ? await _serviceManager.GameService.GetAsync(id)
                : await _serviceManager.GameService.GetAsync(searchTitle);
            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGame(int id, GameDto dto)
        //{
        //    if (id != dto.Id)
        //    {
        //        return BadRequest();
        //    }
        // //   _uow.GameRepository.Update(game);
        //    var existingGame = await _uow.GameRepository.GetAsync(id);
        //    if (existingGame == null)
        //    {
        //        return NotFound("Game does not exist");
        //    }

        //    _mapper.Map(dto, existingGame);
        //    try
        //    {
        //        await _uow.CompleteAsync();
        //    }
        //    catch
        //    {
        //        return StatusCode(500);
        //    }
          
        //    return NoContent();
        //}

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<GameDto>> PostGame(GameCreateDto dto)
        //{
        //    var game = _mapper.Map<Game>(dto);
        //    _uow.GameRepository.Add(game);
        //    try
        //    {
        //        await _uow.CompleteAsync();
        //    }
        //    catch
        //    {
        //        return StatusCode(500);
        //    }

        //    var createdGame = _mapper.Map<GameDto>(game);
        //    return CreatedAtAction(nameof(GetGame), new { id = game.Id }, createdGame);
        //}

        // DELETE: api/Games/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteGame(int id)
        //{
        //    var game = await _uow.GameRepository.GetAsync(id);
        //    if (game == null)
        //    {
        //        return NotFound();
        //    }

        //    _uow.GameRepository.Remove(game);
        //    try
        //    {
        //        await _uow.CompleteAsync();
        //    }
        //    catch
        //    {
        //        return StatusCode(500);
        //    }

        //    return NoContent();
        //}
        //[HttpPatch("{gameId}")]
        //public async Task<IActionResult> PatchGame(int gameId, [FromBody] JsonPatchDocument<GameCreateDto> patchDoc)
        //{
        //    if (patchDoc == null)
        //    {
        //        return BadRequest("Invalid patch document.");
        //    }
        //    if (!await GameExistsAsync(gameId))
        //    {
        //        return NotFound("Game does not exist");
        //    }
        //    var existingGame = await _uow.GameRepository.GetAsync(gameId);
        //    if (existingGame == null)
            //{
            //    return NotFound("Game does not exist");
            //}
            //var dto = _mapper.Map<GameCreateDto>(existingGame);
            //patchDoc.ApplyTo(dto, ModelState);
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //_mapper.Map(dto, existingGame);
            //try
            //{
            //    await _uow.CompleteAsync();
            //}
            //catch
            //{
            //    return StatusCode(500);
            //}
        //    return NoContent();
        //}

        private async Task<bool> GameExistsAsync(int id)
        {
          //  return await _uow.GameRepository.AnyAsync(id);
            return await _serviceManager.GameService.AnyAsync(id);
        }
    }
}
