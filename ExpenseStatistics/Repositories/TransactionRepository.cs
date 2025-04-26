using Microsoft.EntityFrameworkCore;
using ExpenseStatistics.DB;
using ExpenseStatistics.Domain.Entities;

namespace ExpenseStatistics.Repositories
{
    public class TransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<Transaction> GetByIdAsync(Guid id, Guid userId)
        {
            return await _context.Transactions
                .Where(t => t.Id == id && t.UserId == userId)
                .Include(t => t.Category)
                .FirstOrDefaultAsync()
                ?? throw new Exception("Transaction not found");
        }
    }
}