using AutoMapper;
using TournamentManagementSystem.Repositories;

namespace TournamentManagementSystem.BusinessServices
{
    public class MatchService
    {
        private readonly ISystemRepository _repo;
        private readonly IMapper _mapper;

        public MatchService(ISystemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }



    }
}
