namespace Fundo.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> GetAsync()
    {
        return await _context.Customers.AsNoTracking().ToListAsync();
    }

    public async Task<Customer> GetByIdAsync(int customerId)
    {
        return await _context.Customers.AsNoTracking()
            .Include(o => o.Address)
            .FirstAsync(o => o.Id == customerId);
    }
}