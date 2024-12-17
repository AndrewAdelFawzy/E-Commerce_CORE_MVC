using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Task.Core.Entities;
using Task.Infrastructure.Presistence;
using Task.Web.Models;
using Task.Web.Services;
using static System.Net.Mime.MediaTypeNames;

namespace Task.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IImageService imageService, IMapper mapper)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                         .Include(p => p.Brand)
                         .ToList();

            var viewModel = _mapper.Map<IEnumerable<ProductViewModel>>(products);

            ViewBag.Products = viewModel;

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            var viewModel = new ProductFormViewModel
            {
                Brands = _context.Brands
                             .Select(b => new SelectListItem
                             {
                                 Value = b.Id.ToString(),
                                 Text = b.Name
                             })
                             .ToList()
            };

            return View("ProductForm", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Brands = _context.Brands
                               .Select(b => new SelectListItem
                               {
                                   Value = b.Id.ToString(),
                                   Text = b.Name
                               })
                               .ToList();
                return View("ProductForm", model);
            }
               

            Product product = new()
            {
                Title = model.Title,
                Description = model.Description,
                CurrentPrice = model.CurrentPrice,
                OrignalPrice = model.OrignalPrice,
                AvailableQuantaty = model.AvailableQuantaty,
                Code = model.Code,
                BrandId = model.BrandId,
            };

            if (model.Image is not null)
            {
                var ImageName = $"{product.Id}{Path.GetExtension(model.Image.FileName)}";

                var (isUploaded, errorMessage) = await _imageService.UploadASync(model.Image, ImageName, "/Images/Products");

                if (isUploaded)
                {
                    product.ImageUrl = ImageName;
                }
                else
                {
                    ModelState.AddModelError(nameof(Image), errorMessage!);
                    return View("ProductForm", model);
                }
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AllowProductCode(ProductFormViewModel viewModel)
        {
            var Product = await _context.Products.SingleOrDefaultAsync(p=>p.Code == viewModel.Code);

            var isAllowed = Product is null;

            return Json(isAllowed);
        }
    }
}
