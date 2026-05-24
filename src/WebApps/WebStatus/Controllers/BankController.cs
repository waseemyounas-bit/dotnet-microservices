using Microsoft.AspNetCore.Mvc;
using WebStatus.Models.DTOs;
using Customer.Application.Features.Customers.Commands.CreateCustomer;
namespace WebStatus.Controllers
{
    public class BankController : Controller
    {
        private readonly HttpClient _http;

        public BankController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            _http = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:58639/") // API Gateway URL
            };
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDto customer)
        {
            var command = new CreateCustomerCommand
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            };
            var response = await _http.PostAsJsonAsync("Customers", command);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(AccountDto account)
        {
            var response = await _http.PostAsJsonAsync("Accounts", account);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction(TransactionDto transaction)
        {
            string url = transaction.Type == "Adding" ? "Accounts/Adding" : "Accounts/Withdrawing";
            var response = await _http.PostAsJsonAsync(url, transaction);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Transactions(Guid accountId)
        {
            var transactions = await _http.GetFromJsonAsync<List<TransactionDto>>($"Transactions/{accountId}");
            return View(transactions);
        }
    }
}
