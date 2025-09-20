using AutoMapper;
using RSMS.Repositories.Contracts;
using RSMS.Repositories.Implementation;
using RSMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Services.Implementations
{
    public class ActivitiesService : IActivitiesService
    {
        private readonly IActivitiesRepository _repository;
        private readonly IMapper _mapper;

        public ActivitiesService(IActivitiesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
