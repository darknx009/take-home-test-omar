namespace Fundo.Applications.WebApi.Services;

public class LoanManagementService
{
    private readonly ILoanRepository _loanRepository;
    private readonly ICustomerRepository _customerRepository;

    public LoanManagementService(ILoanRepository loanRepository, ICustomerRepository customerRepository)
    {
        _loanRepository = loanRepository;
        _customerRepository = customerRepository;
    }

    public async Task<int> CreateLoan(LoanDto loan)
    {
        if (loan.Amount <= 0)
            throw new InvalidOperationException("Loan amount must be greater than zero.");

        Customer customer = await Task.Run( () => _customerRepository.GetByIdAsync(loan.CustomerId));
        if (customer == null)
            throw new InvalidOperationException("Loan customer should be valid and already exists.");

        Loan newLoan = new Loan(customer.Id, loan.Amount);
        await Task.Run(() => _loanRepository.AddAsync(newLoan));

        return newLoan.Id;
    }

    public async Task<IEnumerable<LoanListedDto>> GetAllLoans()
    {
        List<Loan> loanList = await Task.Run( () => _loanRepository.GetAsync());

        List<Task<LoanListedDto>> listOfTasks = new List<Task<LoanListedDto>>();
        foreach (var loan in loanList)
        {
            listOfTasks.Add(DoAsyncResult(loan));
        }

        return await Task.WhenAll<LoanListedDto>(listOfTasks);
    }

    private static Task<LoanListedDto> DoAsyncResult(Loan loan)
    {
        LoanListedDto newLoanListed = new LoanListedDto()
        {
            LoanId = loan.Id,
            CustomerId = loan.CustomerId,
            CustomerName = loan.Customer.Name,
            LoanAmount = loan.Amount,
            CurrentBalance = loan.CurrentBalance,
            Status = loan.Status
        };
        return Task.FromResult(newLoanListed);
    }

    public async Task<LoanListedDto> GetLoan(int loanId)
    {
        Loan loan = await Task.Run( () => _loanRepository.GetByIdAsync(loanId));
        return await DoAsyncResult(loan);
    }

    public async Task<int> MakePayment(PaymentDto payment)
    {
        if (payment.PaymentAmount <= 0)
            throw new InvalidOperationException("Payment amount must be greater than zero.");

        var loan = await Task.Run( () => _loanRepository.GetByIdAsync(payment.LoanId));
        if (loan == null)
            throw new InvalidOperationException("Loan should be valid and already exists.");

        if (loan.CurrentBalance <= 0)
            throw new InvalidOperationException("Loan is already paid.");

        //Add new payment
        loan.AddPayment(payment.PaymentAmount);

        //Save changes
        await Task.Run(() => _loanRepository.UpdateAsync(loan));
        return 1;
    }
}