using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPartRepository _partRepository;

    public IList<Customer> Customers { get; private set; } = new List<Customer>();
    public IList<Product> Products { get; private set; } = new List<Product>();
    public IList<Part> Parts { get; private set; } = new List<Part>();
    public List<Part> Winkelwagen { get; private set; } = new List<Part>();

    public IndexModel(
        ILogger<IndexModel> logger,
        ICustomerRepository customerRepository,
        IProductRepository productRepository,
        IPartRepository partRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _partRepository = partRepository;
    }

    public void OnGet()
    {
        
        Customers = _customerRepository.GetAllCustomers().ToList();
        Products = _productRepository.GetAllProducts().ToList();
        Parts = _partRepository.GetAllParts().ToList();

        
        var sessionCart = HttpContext.Session.GetString("partCart");
        var cartIds = string.IsNullOrEmpty(sessionCart)
            ? new List<int>()
            : JsonSerializer.Deserialize<List<int>>(sessionCart);

        
        Winkelwagen = Parts.Where(p => cartIds.Contains(p.Id)).ToList();

        _logger.LogInformation("Winkelwagen bevat {Aantal} onderdelen", Winkelwagen.Count);
    }

    [IgnoreAntiforgeryToken]
    public IActionResult OnPostAddToCart([FromBody] int partId)
    {

        var sessionCart = HttpContext.Session.GetString("partCart");
        var cart = string.IsNullOrEmpty(sessionCart)
            ? new List<int>()
            : JsonSerializer.Deserialize<List<int>>(sessionCart);

       
        if (!cart.Contains(partId))
        {
            cart.Add(partId);
            _logger.LogInformation("Onderdeel met ID {Id} toegevoegd aan winkelwagen", partId);
        }
        else
        {
            _logger.LogInformation("Onderdeel met ID {Id} zit al in winkelwagen", partId);
        }

        
        HttpContext.Session.SetString("partCart", JsonSerializer.Serialize(cart));

        return new JsonResult(new { success = true });
    }
}






