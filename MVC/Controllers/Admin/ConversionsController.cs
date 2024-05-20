using Microsoft.AspNetCore.Mvc;
using MVC.Controllers.Util;
using DTO;
using MVC.Services.Interfaces;

namespace MVC.Controllers.Admin
{
    public class ConversionsController : Controller
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IConversionService _conversionService;

        public ConversionsController(ILogger<AccountsController> logger, IConversionService conversionService)
        {
            _logger = logger;
            _conversionService = conversionService;
        }

        // GET: Conversions
        public async Task<IActionResult> Index()
        {
            IEnumerable<ConversionDTO>? conversions = await _conversionService.GetAllConversions();
            if (conversions == null)
            {
                ToastrUtil.ToastrError(this, "Unable to fetch conversions, please contact support");
                return Redirect("/");
            }
            return View(conversions);
        }

        // GET: Conversions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return idNotProvided();
            }

            var conversion = await _conversionService.GetConversionById(id.Value);

            if (conversion == null)
            {
                return conversionNotFound();
            }

            return View(conversion);
        }

        // GET: Conversions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conversions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConversionName, ConversionValue")] ConversionDTO conversion)
        {
            if (ModelState.IsValid)
            {
                if (await _conversionService.CreateConversion(conversion) == null)
                {
                    ToastrUtil.ToastrError(this, "Unable to create conversion");
                    return new EmptyResult();
                }
                // redirect to the new conversion page
                ToastrUtil.ToastrSuccess(this, "Conversion successfully created");
                return RedirectToAction(nameof(Index));
            }
            return View(conversion);
        }

        // GET: Conversions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return idNotProvided();
            }

            var account = await _conversionService.GetConversionById(id.Value);

            if (account == null)
            {
                return conversionNotFound();
            }
            return View(account);
        }

        // POST: Conversions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConversionId,ConversionName,ConversionValue")] ConversionDTO conversion)
        {
            if (id != conversion.ConversionId)
            {
                ToastrUtil.ToastrError(this, "An error has occured with the edit of conversions, please contact support");
                return new EmptyResult();
            }

            //remove the UserName from the model state
            if (ModelState.IsValid)
            {
                if (!await _conversionService.UpdateConversion(conversion))
                {
                    ToastrUtil.ToastrError(this, "Conversion update failed");
                    return RedirectToAction(nameof(Index));
                }
                ToastrUtil.ToastrSuccess(this, "Conversion successfully updated");
                return RedirectToAction(nameof(Index));
            }

            return View(conversion);
        }

        // GET: Conversions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return idNotProvided();
            }

            var account = await _conversionService.GetConversionById(id.Value);

            if (account == null)
            {
                return conversionNotFound();
            }

            return View(account);
        }

        // POST: Conversions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _conversionService.DeleteConversion(id))
            {
                ToastrUtil.ToastrSuccess(this, "Conversion successfully deleted");
                return RedirectToAction(nameof(Index));
            }
            ToastrUtil.ToastrError(this, "Conversion deletion failed");
            return RedirectToAction(nameof(Delete), id);
        }

        private IActionResult idNotProvided()
        {
            ToastrUtil.ToastrError(this, "Id was not provided");
            return RedirectToAction(nameof(Index));
        }

        private IActionResult conversionNotFound()
        {
            ToastrUtil.ToastrError(this, "Account not found");
            return RedirectToAction(nameof(Index));
        }
    }
}
