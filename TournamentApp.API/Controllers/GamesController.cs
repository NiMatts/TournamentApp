using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TournamentApp.Core.Dto;
using TournamentApp.Core.Entities;
using TournamentApp.Core.Repositories;

namespace TournamentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IUoW _uow;
        private readonly IMapper _mapper;

        public GamesController(IUoW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames()
        {
            var games = await _uow.GameRepository.GetAllAsync();
            if (!games.Any())
                return NotFound("Inga matcher hittades.");

            var gameDtos = _mapper.Map<IEnumerable<GameDto>>(games);
            return Ok(gameDtos); // ✅ Allt gick som det ska
        }

        //// GET: api/Games/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<GameDto>> GetGame(int id)
        //{
        //    var game = await _uow.GameRepository.GetAsync(id);
        //    if (game == null)
        //        return NotFound($"Match med id {id} hittades inte.");

        //    return Ok(_mapper.Map<GameDto>(game)); // ✅ Allt gick som det ska
        //}
        [HttpGet("title")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGameByTitle([FromQuery] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Titeln måste anges.");

            var games = await _uow.GameRepository.GetAllAsync();
            var matchedGames = games.Where(g => g.Title == title).ToList();

            if (!matchedGames.Any())
                return NotFound($"Ingen match hittades med titeln '{title}'.");

            var gameDtos = _mapper.Map<IEnumerable<GameDto>>(matchedGames);
            return Ok(gameDtos);
        }
        // PUT: api/Games/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GameUpdateDto updateDto)
        {
            if (id != updateDto.Id)
                return BadRequest("ID:t i URL:en stämmer inte överens med objektet."); // ❌ Validering misslyckas

            var game = await _uow.GameRepository.GetAsync(id);
            if (game == null)
                return NotFound($"Match med id {id} hittades inte.");

            _mapper.Map(updateDto, game);
            _uow.GameRepository.Update(game);

            try
            {
                await _uow.CompleteAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Misslyckades att spara uppdateringar till databasen."); // ❌ Spara misslyckas
            }

            return Ok(_mapper.Map<GameDto>(game)); // ✅ Allt gick som det ska
        }

        // POST: api/Games
        [HttpPost]
        public async Task<ActionResult<GameDto>> PostGame(GameCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // ❌ Validering misslyckas

            var game = _mapper.Map<Game>(createDto);
            _uow.GameRepository.Add(game);

            try
            {
                await _uow.CompleteAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Misslyckades att spara till databasen."); // ❌ Spara misslyckas
            }

            var resultDto = _mapper.Map<GameDto>(game);
            return CreatedAtAction(nameof(PostGame), new { id = game.Id }, resultDto); // ✅ Allt gick som det ska
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _uow.GameRepository.GetAsync(id);
            if (game == null)
                return NotFound($"Match med id {id} hittades inte.");

            _uow.GameRepository.Remove(game);

            try
            {
                await _uow.CompleteAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Misslyckades att ta bort från databasen."); // ❌ Spara misslyckas
            }

            return Ok($"Match med id {id} har tagits bort."); // ✅ Bekräftelse
        }

        [HttpPatch("{gameId}")]
        public async Task<ActionResult<GameDto>> PatchGame(int gameId, JsonPatchDocument<GameDto> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest("Patchdokumentet får inte vara null.");

            var entity = await _uow.GameRepository.GetAsync(gameId);
            if (entity == null)
                return NotFound($"Match med ID {gameId} hittades inte.");

            var dto = _mapper.Map<GameDto>(entity);

            patchDocument.ApplyTo(dto, ModelState);

            if (!TryValidateModel(dto))
                return BadRequest(ModelState);

            _mapper.Map(dto, entity);
            _uow.GameRepository.Update(entity);

            try
            {
                await _uow.CompleteAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ett fel uppstod vid uppdatering av databasen.");
            }

            return Ok(_mapper.Map<GameDto>(entity));
        }
    }
}