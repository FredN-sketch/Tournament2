using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using AutoMapper;
using Tournament.Core.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts;

namespace Tournament.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentDetailsController : ControllerBase
    {     
       // private readonly IMapper _mapper;
        private readonly IServiceManager _serviceManager;

        //private readonly IUnitOfWork _uow;
        public TournamentDetailsController(IServiceManager serviceManager, IMapper mapper, IUnitOfWork uow)
        {
            _serviceManager = serviceManager;
            //_mapper = mapper;
            //_uow = uow;
        }

        // GET: api/TournamentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournamentDetails(bool includeGames, bool sortByTitle)
        { 
            //var tournaments = includeGames 
            //    ? _mapper.Map<IEnumerable<TournamentDto>>(await _uow.TournamentRepository.GetAllAsync(true, sortByTitle))
            //    : _mapper.Map<IEnumerable<TournamentDto>>(await _uow.TournamentRepository.GetAllAsync(false, sortByTitle));

            var tournamentDtos = await _serviceManager.TournamentService.GetAllAsync(includeGames, sortByTitle);
            return Ok(tournamentDtos);
        }

        // GET: api/TournamentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetTournamentDetails(int id, bool includeGames)
        {
           
            //var tournamentDetails = includeGames
            //    ? _mapper.Map<TournamentDto>(await _uow.TournamentRepository.GetAsync(id, true))
            //    : _mapper.Map<TournamentDto>(await _uow.TournamentRepository.GetAsync(id));
            var tournamentDetails = await _serviceManager.TournamentService.GetAsync(id, includeGames);
            if (tournamentDetails == null)
            {
                return NotFound();
            }

            return Ok(tournamentDetails);
        }

        //PUT: api/TournamentDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTournamentDetails(int id, TournamentDto dto)
        //{
        //    if (id != dto.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var existingTournament = await _serviceManager.TournamentService.GetAsync(id);
        //    if (existingTournament == null)
        //    {
        //        return NotFound("Tournament does not exist");
        //    }

        //    _mapper.Map(dto, existingTournament);


        //    try
        //    {
        //       // await _uow.CompleteAsync();
        //      // await _serviceManager.TournamentService.
        //    }
        //    catch
        //    {
        //        return StatusCode(500);
        //    }

        //    return NoContent();
        //}

        // POST: api/TournamentDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TournamentDto>> PostTournamentDetails(TournamentDto dto)
        //{
        //    var tournamentDetails = _mapper.Map<TournamentDetails>(dto);

        //    _uow.TournamentRepository.Add(tournamentDetails);
        //    try
        //    {
        //        await _uow.CompleteAsync();
        //    }
        //    catch
        //    {
        //        return StatusCode(500);
        //    }
        //    var createdTournament = _mapper.Map<TournamentDto>(tournamentDetails);
        //    return CreatedAtAction(nameof(GetTournamentDetails), new { id = tournamentDetails.Id }, createdTournament);
        //}

        // DELETE: api/TournamentDetails/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTournamentDetails(int id)
        //{
        //    var tournamentDetails = await _uow.TournamentRepository.GetAsync(id);
        //    if (tournamentDetails == null)
        //    {
        //        return NotFound();
        //    }

        //    _uow.TournamentRepository.Remove(tournamentDetails);
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
        //[HttpPatch("{tournamentId}")]
        //public async Task<IActionResult> PatchTournamentDetails(int tournamentId, [FromBody] JsonPatchDocument<TournamentDto> patchDoc)
        //{
        //    if (patchDoc == null)
        //    {
        //        return BadRequest("Invalid patch document.");
        //    }
        //    var tournamentDetails = await _uow.TournamentRepository.GetAsync(tournamentId);
        //    if (tournamentDetails == null)
        //    {
        //        return NotFound("Tournament does not exist");
        //    }
        //    var tournamentDto = _mapper.Map<TournamentDto>(tournamentDetails);
        //    patchDoc.ApplyTo(tournamentDto, ModelState);
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    _mapper.Map(tournamentDto, tournamentDetails);
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

        private async Task<bool> TournamentDetailsExists(int id)
        {
          // return await _uow.TournamentRepository.AnyAsync(id);
            return await _serviceManager.TournamentService.AnyAsync(id);
        }
    }
}
