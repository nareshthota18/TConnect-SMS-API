using RSMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Contracts
{
    public interface IAssetRepository
    {
        Task<AssetDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<AssetDTO>> GetAllAsync();
        Task<AssetDTO> AddAsync(AssetDTO issue);
        Task<AssetDTO> UpdateAsync(AssetDTO issue);
        Task<bool> DeleteAsync(Guid id);
    }
}
