using Fundo.Domain.Entities;

namespace Fundo.Domain.Repository;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAsync();
    Task<Customer> GetByIdAsync(int id);
}