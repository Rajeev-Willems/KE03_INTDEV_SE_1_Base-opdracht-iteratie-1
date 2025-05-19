using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class Index5Model : PageModel
{
    private readonly ILogger<Index5Model> _logger;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPartRepository _PartRepository;

    public IList<Customer> Customers { get; set; }
    public IList<Product> Products { get; set; }

    public IList<Part> Parts { get; set; }

    public Index5Model(
        ILogger<Index5Model> logger,
        ICustomerRepository customerRepository,
        IProductRepository productRepository,
        IPartRepository partRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _PartRepository = partRepository;

        Customers = new List<Customer>();
        Products = new List<Product>();
        Parts = new List<Part>();
    }

    public void OnGet()
    {
        Customers = _customerRepository.GetAllCustomers().ToList();
        Products = _productRepository.GetAllProducts().ToList();
        Parts = _PartRepository.GetAllParts().ToList();

        _logger.LogInformation("Getting {CustomerCount} customers, {ProductCount} products, and {PartCount} parts",
            Customers.Count, Products.Count, Parts.Count);
    }
    [IgnoreAntiforgeryToken]
    public IActionResult OnPostAddToCart([FromBody] int productId)
    {

        return new JsonResult(new { success = true });
    }

}
