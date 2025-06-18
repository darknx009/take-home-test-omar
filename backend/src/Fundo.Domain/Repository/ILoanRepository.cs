using Fundo.Domain.Entities;

namespace Fundo.Domain.Repository;

public interface ILoanRepository
{
    Task<List<Loan>> GetAsync();
    Task<Loan> GetByIdAsync(int id);
    Task AddAsync(Loan loan);
    Task UpdateAsync(Loan loan);
}