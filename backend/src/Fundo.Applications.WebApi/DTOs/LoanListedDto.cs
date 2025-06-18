namespace Fundo.Applications.WebApi.Dtos;

public class LoanListedDto
{
    public int LoanId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal LoanAmount { get; set; }
    public decimal CurrentBalance { get; set; }
    public string Status { get; set; } = string.Empty;
}