using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task.Core.Entities;
using Task.Core.Enums;
using Task.Infrastructure.Presistence;
using Task.Web.Models;
using Task.Web.Services;
using static System.Net.Mime.MediaTypeNames;

namespace Task.Web.Controllers
{

    public class BrandsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _imageService;


        public BrandsController(ApplicationDbContext context, IImageService imageService, IWebHostEnvironment webHostEnvironment , IMapper mapper)
        {
            _context = context;
            _imageService = imageService;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var brands = _context.Brands
                         .ToList();

            var viewModel = _mapper.Map<IEnumerable<BrandViewModel>>(brands);

            ViewBag.Products = viewModel;

            return View(viewModel);
        }

        public IActionResult AddBrand()
        {
            var viewModel = new BrandFormViewModel
            {
                // Populate the dropdowns with enum values
                Fields = Enum.GetValues(typeof(Field))
                         .Cast<Field>()
                         .Select(f => new SelectListItem
                         {
                             Value = ((int)f).ToString(),
                             Text = f.ToString()
                         }).ToList(),

                Countries = Enum.GetValues(typeof(Country))
                            .Cast<Country>()
                            .Select(c => new SelectListItem
                            {
                                Value = ((int)c).ToString(),
                                Text = c.ToString()
                            }).ToList(),
            };
            return View("BrandForm", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddBrand(BrandFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Fields = Enum.GetValues(typeof(Field))
                    .Cast<Field>()
                    .Select(f => new SelectListItem
                    {
                        Value = ((int)f).ToString(),
                        Text = f.ToString()
                    }).ToList();

                model.Countries = Enum.GetValues(typeof(Country))
                    .Cast<Country>()
                    .Select(c => new SelectListItem
                    {
                        Value = ((int)c).ToString(),
                        Text = c.ToString()
                    }).ToList();

                return View("BrandForm", model);
            }

            Brand brand = new()
            {
                Name = model.Name,
                Field = model.Field,
                Country = model.Country
            };

            // Handle Background Image
            if (model.BackgroundImage is not null)
            {
                var backgroundImageName = $"{brand.Id}_background{Path.GetExtension(model.BackgroundImage.FileName)}";

                var (isUploaded, errorMessage) = await _imageService.UploadASync(model.BackgroundImage, backgroundImageName, "/Images/Brands");
                if (isUploaded)
                {
                    brand.BackgroundImageUrl = backgroundImageName;
                }
                else
                {
                    ModelState.AddModelError(nameof(model.BackgroundImage), errorMessage!);
                    return View("BrandForm", model);
                }
            }

            // Handle Profile Image
            if (model.ProfileImage is not null)
            {
                var profileImageName = $"{brand.Id}_profile{Path.GetExtension(model.ProfileImage.FileName)}";

                var (isUploaded, errorMessage) = await _imageService.UploadASync(model.ProfileImage, profileImageName, "/Images/Brands");
                if (isUploaded)
                {
                    brand.ProfileImageUrl = profileImageName;
                }
                else
                {
                    ModelState.AddModelError(nameof(model.ProfileImage), errorMessage!);
                    return View("BrandForm", model);
                }
            }

            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> BrandDetails(Guid id)
        {
            var brand = await _context.Brands
            .Include(b => b.Products)
            .FirstOrDefaultAsync(b => b.Id == id);

            if (brand is null)
                return NotFound();

            var productViewModel = _mapper.Map<IEnumerable<ProductViewModel>>(brand.Products);
            ViewBag.BrandName = brand.Name;

            return View("Details",productViewModel);
        }

    }

}
