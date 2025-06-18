namespace Fundo.Domain.Entities;

public class Loan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    [Required] public int CustomerId { get; private set; }
    [Required] public decimal Amount { get; private set; }
    public DateTime LoanDate { get; private set; }

    [Required] public virtual Customer Customer { get; set; }
    public virtual ICollection<Payment> Payments { get; private set; } = new List<Payment>();

    // Constructor with required parameters
    public Loan(int customerId, decimal amount)
    {
        this.CustomerId = customerId;
        this.Amount = amount;
        this.LoanDate = DateTime.UtcNow;
    }

    // CurrentBalance calculated property
    public decimal CurrentBalance => this.Amount - Payments.Sum(i => i.Amount);
    //Status calculated property
    public string Status => this.CurrentBalance <= 0 ? "paid" : "active";

    // AddPayment method for adding a new payment
    public void AddPayment(decimal amount)
    {
        if (amount <= 0) throw new ArgumentNullException(nameof(amount));
        Payments.Add(new Payment(this.Id, amount));
    }    
}