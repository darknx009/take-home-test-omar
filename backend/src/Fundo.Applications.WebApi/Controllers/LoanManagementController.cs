using Microsoft.AspNetCore.Authorization;

namespace Fundo.Applications.WebApi.Controllers;

[Route("/loans")]
[Authorize]
public class LoanManagementController : Controller
{
    private readonly LoanManagementService _loanService;
    private readonly ILogger<LoanManagementController> _logger;

    public LoanManagementController(LoanManagementService loanService, ILogger<LoanManagementController> logger)
    {
        _loanService = loanService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLoan([FromBody] LoanDto loan)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            int newLoadId = await _loanService.CreateLoan(loan);
            _logger.LogInformation($"Loan has been created with ID: {newLoadId}");

            return Ok(newLoadId);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Loan could not be created with this data: {loan} and contains this error message: {ex.Message}");
            return StatusCode(500, "Internal server error within loan creation"); 
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLoan(int id)
    {
        try
        {
            var loan = await _loanService.GetLoan(id);
            return loan == null ? NotFound() : Ok(loan);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Loan could not be retrieved with this ID: {id} and contains this error message: {ex.Message}");
            return StatusCode(500, "Internal server error on getting loan by ID");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLoans()
    {
        try
        {
            return Ok(await _loanService.GetAllLoans());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Loans could not be retrieved and contains this error message: {ex.Message}");
            return StatusCode(500, "Internal server error on getting loans");
        }
    }

    [HttpPost("{id}/payment")]
    public async Task<IActionResult> MakePayment(int id, [FromBody] PaymentDto payment)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            payment.LoanId = id;
            int paymentID = await _loanService.MakePayment(payment);
            _logger.LogInformation($"Payment made on loanID {payment.LoanId} of ${payment.PaymentAmount}");

            return Ok(paymentID);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Payment could not be created with LoadID: {id} and contains this error message: {ex.Message}");
            return StatusCode(500, "Internal server error on adding payment");
        }
    }
}