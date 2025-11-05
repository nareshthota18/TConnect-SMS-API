using RSMS.Common.DTO;
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
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<HostelDTO>> GetAllAsync();
    }
}
