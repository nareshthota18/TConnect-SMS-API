using RSMS.Common.DTO;
using RSMS.Data.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Services.Interfaces
{
    public interface IConsumptionService
    {
        Task AddAsync(ConsumptionConfigDTO entity, Guid userId);
        Task UpdateAsync(ConsumptionConfigDTO entity, Guid userId);
        Task<bool> DeleteAsync(Guid id);
        Task<ConsumptionConfigDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<ConsumptionConfigDTO>> GetAllAsync(Guid hostelId);
    }
}
