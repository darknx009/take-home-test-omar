namespace Fundo.Infrastructure.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly AppDbContext _context;

    public LoanRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Loan>> GetAsync()
    {
        return await _context.Loans.AsNoTracking()
                             .Include(x => x.Customer)
                             .Include(x => x.Payments) 
                             .ToListAsync();
    }

    public async Task<Loan> GetByIdAsync(int loanId)
    {
        return await _context.Loans.AsNoTracking()
            .Include(o => o.Payments)
            .Include(o => o.Customer)
            .FirstAsync(o => o.Id == loanId);
    }

    public async Task AddAsync(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Loan loan)
    {
        _context.Loans.Update(loan);
        await _context.SaveChangesAsync();
    }
}