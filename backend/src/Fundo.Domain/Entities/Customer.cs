using Fundo.Domain.ValueObjects;

namespace Fundo.Domain.Entities;

public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    [Required] public string Name { get; private set; }
    [Required] public string Email { get; private set; }
    [Required] public int AddressId { get; private set; }

    public virtual Address Address { get; private set; }
    public virtual ICollection<Loan> Loans { get; private set; } = new List<Loan>();


    public Customer(string name, string email, int addressId)
    {
        this.Name = name;
        this.Email = email;
        this.AddressId = addressId;
    }
}