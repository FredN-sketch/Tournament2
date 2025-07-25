﻿using Microsoft.AspNetCore.Mvc;
using Tournament.Core.Repositories;
using AutoMapper;
using Tournament.Core.Dto;
using Service.Contracts;
using Tournament.Core.Request;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Tournament.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentDetailsController : ControllerBase
    {     
       // private readonly IMapper _mapper;
        private readonly IServiceManager _serviceManager;

        
        public TournamentDetailsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
           
        }

        // GET: api/TournamentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournamentDetails([FromQuery] TournamentRequestParams requestParams)
        {          
            //var tournamentDtos = await _serviceManager.TournamentService.GetAllAsync(requestParams);
            //return Ok(tournamentDtos);
            var pagedResult = await _serviceManager.TournamentService.GetAllAsync(requestParams);
            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.tournamentDtos);
        }

        // GET: api/TournamentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetTournamentDetails(int id, bool includeGames)
        {           
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
        [HttpPost]
        public async Task<ActionResult<TournamentDto>> PostTournamentDetails(TournamentDto dto)
        {
            //var tournamentDetails = _mapper.Map<TournamentDetails>(dto);

            //_uow.TournamentRepository.Add(tournamentDetails);
            //try
            //{
            //    await _uow.CompleteAsync();
            //}
            //catch
            //{
            //    return StatusCode(500);
            //}
            //var createdTournament = _mapper.Map<TournamentDto>(tournamentDetails);
            var createdTournament = await _serviceManager.TournamentService.PostTournament(dto);
            return CreatedAtAction(nameof(GetTournamentDetails), new { id = createdTournament.Id }, createdTournament);
        }

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
