using Microsoft.EntityFrameworkCore;
using RSMS.Data;
using RSMS.Data.Models.Others;
using RSMS.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Repositories.Implementation
{
    public class ConsumptionRepository : IConsumptionRepository
    {
        private readonly RSMSDbContext _context;

        public ConsumptionRepository(RSMSDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ConsumptionConfig entity)
        {
            entity.Id = Guid.NewGuid();
            await _context.ConsumptionConfig.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ConsumptionConfig entity)
        {
            _context.ConsumptionConfig.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var obj = await _context.ConsumptionConfig.FindAsync(id);
            if (obj == null) return false;

            _context.ConsumptionConfig.Remove(obj);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ConsumptionConfig?> GetByIdAsync(Guid id)
        {
            return await _context.ConsumptionConfig
                                 .Include(x => x.RSHostel)
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<ConsumptionConfig>> GetAllAsync(Guid RSHostelId)
        {
            return await _context.ConsumptionConfig
                .Where(x => x.RSHostelId == RSHostelId)
                .ToListAsync();
        }
    }
}
