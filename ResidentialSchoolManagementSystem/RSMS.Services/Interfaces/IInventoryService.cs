using RSMS.Common.DTO;

namespace RSMS.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDTO>> GetAllBySchoolAsync(Guid schoolId);
        Task<InventoryDTO?> GetByIdAsync(Guid id);
        Task<InventoryDTO> AddAsync(InventoryDTO dto);
        Task<InventoryDTO> UpdateAsync(InventoryDTO dto);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<InventoryDTO>> GetAGroceryAsync(Guid schoolId);
    }
}
