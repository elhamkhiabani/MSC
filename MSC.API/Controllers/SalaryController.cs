using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSC.Core.Handlers.Intefaces;
using MSC.Core.Presentations;
using MSC.Core.Presentations.Base;
using MSC.Core.Services.Interfaces;
using MSC.Domain.Models;
using System.Text.Json;

namespace MSC.API.Controllers
{
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly IMonthSalaryCalculateHandler _handler;
        private readonly ISalaryService _service;
        public SalaryController(IMonthSalaryCalculateHandler handler, ISalaryService service)
        {
            _handler = handler;
            _service = service;
        }


        /// <summary>
        /// به‌روزرسانی اطلاعات حقوق
        /// </summary>
        /// <remarks>
        /// این متد برای به‌روزرسانی اطلاعات حقوق یک کارمند برای یک ماه خاص استفاده می‌شود.
        /// </remarks>
        /// <param name="entity">مدل ورودی شامل اطلاعات حقوق و مزایا</param>
        /// <returns>پیامی شامل وضعیت عملیات به‌روزرسانی</returns>
        /// <response code="200">اگر عملیات موفقیت‌آمیز باشد</response>
        /// <response code="400">اگر اطلاعات ورودی نامعتبر باشد</response>
        [HttpPost]
        [Route("salary/update")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public MessageViewModel update([FromBody] UpdateViewModel entity)
        {
            var result = _handler.UpdateForMonth(entity);
            return result;
        }

        /// <summary>
        /// حذف اطلاعات با استفاده از شناسه
        /// </summary>
        /// <remarks>
        /// این متد برای حذف سخت و نرم با استفاده از شناسه می باشد.
        /// </remarks>
        /// <param name="id">شناسه</param>
        /// <returns></returns>
        /// <response code="200">اگر عملیات موفقیت‌آمیز باشد</response>
        /// <response code="400">اگر اطلاعات ورودی نامعتبر باشد</response>
        [HttpGet]
        [Route("salary/remove/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public MessageViewModel remove(int id)
        {
            var result = _service.Delete(id);
            return result;
        }

        /// <summary>
        /// حذف اطلاعات یک ماه یک کارمند
        /// </summary>
        /// <remarks>
        /// این متد اطلاعات یک ماه یک کارمند را به صورت نرم حذف می نماید.
        /// </remarks>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <response code="200">اگر عملیات موفقیت‌آمیز باشد</response>
        /// <response code="400">اگر اطلاعات ورودی نامعتبر باشد</response> 
        [HttpPost]
        [Route("salary/removeForMonth")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public MessageViewModel removeForMonth([FromBody] FilterViewModel entity)
        {
            var result = _handler.DeleteSalaryForMonth(entity);
            return result;
        }



        /// <summary>
        /// افزودن اطلاعات یک کارمند
        /// </summary>
        /// <param name="entity">اطلاعات شخص به همراه نحوه محاسبه حقوق و دستمزد</param>
        /// <remarks>
        /// این متد اطلاعات کارمند را دریافت نموده و براساس نوع محاسبه حقوق و دستمزد آن را محاسبه و ذخیره می نماید.
        /// </remarks>
        /// <param name="dataType">نوع داده های ورودی</param>
        /// <returns></returns>
        /// <response code="200">اگر عملیات موفقیت‌آمیز باشد</response>
        /// <response code="400">اگر اطلاعات ورودی نامعتبر باشد</response> 
        [HttpPost]
        [Route("{dataType}/salary/add")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public MessageViewModel add([FromBody] RequestSalaryViewModel entity, string dataType)
        {
            var result = _handler.Add(entity, dataType);
            return result;
        }


        /// <summary>
        /// دریافت اطلاعات یک کارمند با استفاده از شناسه
        /// </summary>
        /// <remarks>
        /// این متد اطلاعات یک کارمند را با استفاده از شناسه دریافت و نمایش می دهد.
        /// </remarks>
        /// <param name="id">شناسه</param>
        /// <returns></returns>
        /// <response code="200">اگر عملیات موفقیت‌آمیز باشد</response>
        /// <response code="400">اگر اطلاعات ورودی نامعتبر باشد</response> 
        [HttpGet]
        [Route("salary/get/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ResultViewModel<SalaryViewModel> get(int id)
        {
            var result = _service.GetByID(id);
            return result;
        }


        /// <summary>
        /// دریافت اطلاعات یک ماه یک کارمند
        /// </summary>
        /// <remarks>
        /// این متد با دریافت نام و نام خانوادگی کامند و همینطور ماه مورد نیاز اطلاعات یک کارمند را نمایش می دهد.
        /// </remarks>
        /// <param name="entity">فیلتر های مورد نظر برای دریافت اطلاعات</param>
        /// <returns></returns>        
        /// <response code="200">اگر عملیات موفقیت‌آمیز باشد</response>
        /// <response code="400">اگر اطلاعات ورودی نامعتبر باشد</response> 
        [HttpPost]
        [Route("salary/getMonthlyByFullname")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ResultViewModel<SalaryViewModel> getMonthlyByFullname([FromBody] FilterViewModel entity)
        {
            var result = _service.SelectByFullName(entity);
            return result;
        }


        /// <summary>
        /// نمایش اطلاعات کارمند در یک ماه خاص
        /// </summary>
        /// <remarks>
        /// این متد اطلاعات یک کارمند را با استفاده از فیلتر های نام و نام خانوادگی و ماه مورد درخواست(1403/09/29) دریافت و نمایش می دهد.
        /// </remarks>
        /// <param name="entity">فیلتر های مورد نیاز برای دریافت اطلاعات یک ماه یک کارمند</param>
        /// <returns></returns>
        /// <response code="200">اگر عملیات موفقیت‌آمیز باشد</response>
        /// <response code="400">اگر اطلاعات ورودی نامعتبر باشد</response> 
        [HttpPost]
        [Route("salary/selectAll")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ResultViewModel<SalaryViewModel> selectAll([FromBody] FilterViewModel entity)
        {
            var result = _service.SelectAll(entity);
            return result;
        }
    }
}
