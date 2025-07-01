using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApp.Core.Dto;
using TournamentApp.Core.Entities;
using TournamentApp.Core.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TournamentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly IUoW _uow;
        private readonly IMapper _mapper;

        public TournamentsController(IUoW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournaments()
        {
            var tournaments = await _uow.TournamentRepository.GetAllAsync(includeGames: true);
            if (!tournaments.Any())
                return NotFound("Inga turneringar hittades.");

            var dtos = _mapper.Map<IEnumerable<TournamentDto>>(tournaments);
            return Ok(dtos);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetTournament(int id)
        {
            var tournament = await _uow.TournamentRepository.GetAsync(id);
            if (tournament == null)
                return NotFound($"Turnering med ID {id} hittades inte.");

            return Ok(_mapper.Map<TournamentDto>(tournament));
        }

        // POST: api/Tournaments
        [HttpPost]
        public async Task<ActionResult<TournamentDto>> PostTournament(TournamentCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = _mapper.Map<Tournament>(createDto);
            _uow.TournamentRepository.Add(entity);

            try
            {
                await _uow.CompleteAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ett fel uppstod vid sparning till databasen.");
            }

            var resultDto = _mapper.Map<TournamentDto>(entity);
            return CreatedAtAction(nameof(GetTournament), new { id = resultDto.Id }, resultDto);
        }

        // PUT: api/Tournaments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, TournamentUpdateDto updateDto)
        {
            if (id != updateDto.Id)
                return BadRequest("ID:t i URL matchar inte kroppens ID.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _uow.TournamentRepository.GetAsync(id);
            if (existing == null)
                return NotFound($"Turnering med ID {id} hittades inte.");

            _mapper.Map(updateDto, existing);
            _uow.TournamentRepository.Update(existing);

            try
            {
                await _uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Ett fel uppstod vid uppdatering av databasen.");
            }

            return Ok(_mapper.Map<TournamentDto>(existing));
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = await _uow.TournamentRepository.GetAsync(id);
            if (tournament == null)
                return NotFound($"Turnering med ID {id} kunde inte hittas.");

            _uow.TournamentRepository.Remove(tournament);

            try
            {
                await _uow.CompleteAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ett fel uppstod vid borttagning från databasen.");
            }

            return Ok($"Turnering med ID {id} har tagits bort.");
        }

        [HttpPatch("{tournamentId}")]
        public async Task<ActionResult<TournamentDto>> PatchTournament(int tournamentId, JsonPatchDocument<TournamentDto> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest("Patchdokumentet får inte vara null.");

            var entity = await _uow.TournamentRepository.GetAsync(tournamentId);
            if (entity == null)
                return NotFound($"Turnering med ID {tournamentId} hittades inte.");

            var dto = _mapper.Map<TournamentDto>(entity);

            patchDocument.ApplyTo(dto, ModelState);

            if (!TryValidateModel(dto))
                return BadRequest(ModelState);

            _mapper.Map(dto, entity);
            _uow.TournamentRepository.Update(entity);

            try
            {
                await _uow.CompleteAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ett fel uppstod vid uppdatering av databasen.");
            }

            return Ok(_mapper.Map<TournamentDto>(entity));
        }
    }
}