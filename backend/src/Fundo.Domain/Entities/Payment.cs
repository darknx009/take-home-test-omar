namespace Fundo.Domain.Entities;

public class Payment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; } //Auto increment Id
    [Required] public int LoanId { get; private set; }
    [Required] public decimal Amount { get; private set; }
    public string Status { get; private set; } // "active" or "cancelled"
    public DateTime PaymentDate { get; private set; }

    public virtual Loan Loan { get; private set; }

    // Constructor with only Payment as parameter
    public Payment(int loanId, decimal amount)
    {
        this.LoanId = loanId;
        this.Amount = amount;
        this.Status = "active";
        this.PaymentDate = DateTime.UtcNow;
    }
}