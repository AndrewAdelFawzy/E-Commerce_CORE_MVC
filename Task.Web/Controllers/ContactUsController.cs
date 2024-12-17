using Microsoft.AspNetCore.Mvc;
using Task.Core.Entities;
using Task.Infrastructure.Presistence;
using Task.Web.Models;

namespace Task.Web.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactUsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("ContactUsForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactUsViewModel model)
        {
            if (!ModelState.IsValid)
                return View("ContactUsForm", model);

            ContactUs contactUs = new ContactUs()
            {
                FullName=model.FullName,
                PhoneNumber=model.PhoneNumber,
                Email=model.Email,
                Message=model.Message,
            };

            await _context.ContactUs.AddAsync(contactUs);
            await _context.SaveChangesAsync();


            TempData["SuccessMessage"] = "Your form has been successfully submitted! We will get back to you shortly.";
            return RedirectToAction(nameof(Create));  
        }
    }
}
