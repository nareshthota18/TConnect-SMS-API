using RSMS.Common.DTO;

namespace RSMS.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDTO>> GetAllAsync();
        Task<SupplierDTO?> GetByIdAsync(Guid id);
        Task<SupplierDTO> AddAsync(SupplierDTO supplier);
        Task<SupplierDTO> UpdateAsync(SupplierDTO supplier);
        Task<bool> DeleteAsync(Guid id);
    }
}
