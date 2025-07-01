using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TournamentApp.Core.Dto;
using TournamentApp.Core.Entities;

namespace TournamnetApp.Data.Data
{
    public class TournamentMappings : Profile
    {
        public TournamentMappings() {
            CreateMap<Tournament, TournamentDto>();
            CreateMap<TournamentCreateDto, Tournament>();
            CreateMap<TournamentUpdateDto, Tournament>();

            // Game mappings
            CreateMap<Game, GameDto>();
            CreateMap<GameCreateDto, Game>();
            CreateMap<GameUpdateDto, Game>();
        }
    }
}
