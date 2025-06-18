using Fundo.Domain.Entities;

namespace Fundo.Domain.ValueObjects;

public class Address
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    [Required] public string Street { get; private set; }
    [Required] public string City { get; private set; }
    [Required] public string PostalCode { get; private set; }

    public virtual ICollection<Customer> Customers { get; private set; } = new List<Customer>();

    public Address(string street, string city, string postalCode)
    {
        this.Street = street;
        this.City = city;
        this.PostalCode = postalCode;
    }
}


