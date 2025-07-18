using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System.Xml.XPath;
using Tournament.Core.Dto;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Core.Request;

namespace Tournament.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public GamesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;        
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGame([FromQuery] GameRequestParams requestParams)
        {                  
            var gameDtos = await _serviceManager.GameService.GetAllAsync(requestParams);
            return Ok(gameDtos);
        }

        // GET: api/Games/5     
        [HttpGet("{id?}")]
        public async Task<ActionResult<GameDto>> GetGame(int id, [FromQuery] string? searchTitle=null)
        {

            var game = string.IsNullOrEmpty(searchTitle)                       
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

        //POST: api/Games
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameDto>> PostGame(GameCreateDto dto)
        {
            var createdGame = await _serviceManager.GameService.PostGame(dto);
            try
            {
               await _serviceManager.GameService.CompleteAsync();
            }
            catch
            {
                return StatusCode(500);
            }

           
            return CreatedAtAction(nameof(GetGame), new { id = createdGame.Id }, createdGame);
        }


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
        [HttpPatch("{gameId}")]
        public async Task<IActionResult> PatchGame(int gameId, [FromBody] JsonPatchDocument<GameCreateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document.");
            }
            if (!await GameExistsAsync(gameId))
            {
                return NotFound("Game does not exist");
            }
            Game existingGame = await _serviceManager.GameService.GetGameAsync(gameId);// _uow.GameRepository.GetAsync(gameId);
            if (existingGame == null)
            {
                return NotFound("Game does not exist");
            }

            var dto = _serviceManager.GameService.MapGame(existingGame); //_mapper.Map<GameCreateDto>(existingGame);
            //var dto =_mapper.Map<GameCreateDto>(existingGame);
            //patchDoc.ApplyTo(dto, ModelState);
            patchDoc.ApplyTo(dto);
            TryValidateModel(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //_mapper.Map(dto, existingGame);
            await _serviceManager.GameService.MapGameCreateDto(dto, existingGame); //_mapper.Map<Game>(dto);
            //try
            //{
            //    await _serviceManager.GameService.CompleteAsync();
            //}
            //catch
            //{
            //    return StatusCode(500);
            //}
            return NoContent();
        }

        private async Task<bool> GameExistsAsync(int id)
        {      
            return await _serviceManager.GameService.AnyAsync(id);
        }
    }
}
