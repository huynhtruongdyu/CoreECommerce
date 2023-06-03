using CEC.Application.Services;
using CEC.Application.UnitOfWork;
using CEC.Shared.Models.DTO;
using CEC.WebMVC.Areas.Admin.Controllers.Base;

using Microsoft.AspNetCore.Mvc;

namespace CEC.WebMVC.Areas.Admin.Controllers
{
    //[Route("[controller]/[action]")]
    public class MasterdataController : BaseAdminController
    {
        public MasterdataController(IUnitOfWork unitOfWork, IServiceManagement serviceManagement) : base(unitOfWork, serviceManagement)
        {
        }

        #region Currency

        [Route("Currency")]
        public IActionResult Currency()
        {
            var rawCurrencies = unitOfWork.CurrencyRepository.GetAll();
            var currenciesDto = new List<CurrencyDto>();
            foreach (var rawCurrency in rawCurrencies)
            {
                currenciesDto.Add(new CurrencyDto(rawCurrency.Id, rawCurrency.Name, rawCurrency.Code, rawCurrency.Symbol));
            }

            return View(currenciesDto);
        }

        #endregion Currency
    }
}