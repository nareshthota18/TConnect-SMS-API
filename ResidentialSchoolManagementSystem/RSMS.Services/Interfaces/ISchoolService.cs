using RSMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Services.Interfaces
{
    public interface ISchoolService
    {
        Task AddAsync(HostelDTO dto);
        Task<IEnumerable<HostelDTO>> GetAllAsync(Guid userId);
    }
}
