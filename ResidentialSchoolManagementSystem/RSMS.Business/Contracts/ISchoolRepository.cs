using RSMS.Data.Models.LookupEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Repositories.Contracts
{
    public interface ISchoolRepository
    {
        Task AddAsync(RSHostel hostel);
        Task<IEnumerable<RSHostel>> GetAllAsync(Guid UserId);
    }
}
