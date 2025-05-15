using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

public class Index1Model : PageModel
{
    private readonly ILogger<Index1Model> _logger;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;

    public IList<Customer> Customers { get; set; }
    public IList<Product> Products { get; set; }

    public Index1Model(
        ILogger<Index1Model> logger,
        ICustomerRepository customerRepository,
        IProductRepository productRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _productRepository = productRepository;

        Customers = new List<Customer>();
        Products = new List<Product>();
    }

    public void OnGet()
    {
        Customers = _customerRepository.GetAllCustomers().ToList();
        Products = _productRepository.GetAllProducts().ToList();

        _logger.LogInformation($"Getting {Customers.Count} customers and {Products.Count} products");
    }
}








