using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Platzi_Dotnet.Data;
using Platzi_Dotnet.VIewModels;
using Platzi_Dotnet.Models;
using System.Net;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> GetProduct(int id)
        {
            if (_context.Product == null)
            {
                return NotFound();
            }
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
            }

            return _mapper.Map<ProductViewModel>(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductViewModel product)
        {

            if (!ProductExists(id))
            {
                return NotFound();
            }

            _context.Entry(_mapper.Map<Product>(product)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw new System.Web.Http.HttpResponseException(HttpStatusCode.InternalServerError);

            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Product == null)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NoContent);
            }
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ProductExists(int id)
        {
            return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
