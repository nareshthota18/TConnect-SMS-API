using AutoMapper;
using RSMS.Business.Contracts;
using RSMS.Common.Models;
using RSMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Implementation
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly IReportsService _reportsService;
        private readonly IMapper _mapper;
        public ReportsRepository(IReportsService reportsService, IMapper mapper)
        {
            _reportsService = reportsService;
            _mapper = mapper;
        }

        public Task<ReportRequestDTO?> GetAllAttendanceTimeRange(ReportRequestDTO att)
        {
            throw new NotImplementedException();
        }
    }
}
