using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class Index1Model : PageModel
{
    private readonly ILogger<Index1Model> _logger;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPartRepository _partRepository;

    public IList<Customer> Customers { get; set; }
    public IList<Product> Products { get; set; }
    public IList<Part> Parts { get; set; }

    public List<Part> winkelwagen { get; set; } = new List<Part>();
    public decimal TotaalPrijs => winkelwagen.Sum(p => p.Price);

    public Index1Model(
        ILogger<Index1Model> logger,
        ICustomerRepository customerRepository,
        IProductRepository productRepository,
        IPartRepository partRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _partRepository = partRepository;

        Customers = new List<Customer>();
        Products = new List<Product>();
        Parts = new List<Part>();
    }

    public void OnGet()
    {
        Customers = _customerRepository.GetAllCustomers().ToList();
        Products = _productRepository.GetAllProducts().ToList();
        Parts = _partRepository.GetAllParts().ToList();

        var sessionCart = HttpContext.Session.GetString("partCart");
        List<int> cartIds = string.IsNullOrEmpty(sessionCart)
            ? new List<int>()
            : JsonSerializer.Deserialize<List<int>>(sessionCart);

        winkelwagen = _partRepository.GetAllParts()
            .Where(p => cartIds.Contains(p.Id))
            .ToList();

        _logger.LogInformation("Winkelwagen bevat {Aantal} onderdelen", winkelwagen.Count);
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
            HttpContext.Session.SetString("partCart", JsonSerializer.Serialize(cart));
        }

        return new JsonResult(new { success = true });
    }

    [IgnoreAntiforgeryToken]
    public IActionResult OnPostRemoveFromCart([FromBody] int partId)
    {
        var sessionCart = HttpContext.Session.GetString("partCart");
        var cart = string.IsNullOrEmpty(sessionCart)
            ? new List<int>()
            : JsonSerializer.Deserialize<List<int>>(sessionCart);

        cart.Remove(partId);
        HttpContext.Session.SetString("partCart", JsonSerializer.Serialize(cart));

        return new JsonResult(new { success = true });
    }
}

