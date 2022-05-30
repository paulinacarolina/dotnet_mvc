using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Platzi_Dotnet.Data;
using Platzi_Dotnet.VIewModels;
using Platzi_Dotnet.Models;

namespace Platzi_Dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetProducts()
        {
          if (_context.Product == null)
          {
              return NotFound();
          }
           List<Product> products = await _context.Product.ToListAsync();
          var mappedProducts = _mapper.Map<IEnumerable<ProductViewModel>>(products);
           return mappedProducts != null  ? Ok(mappedProducts) : NotFound();  
        }

    }
}
