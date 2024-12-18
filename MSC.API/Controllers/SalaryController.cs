using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSC.Core.Handlers.Intefaces;
using MSC.Core.Presentations;
using MSC.Core.Presentations.Base;
using MSC.Core.Services.Interfaces;
using MSC.Domain.Models;

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

        [HttpPost]
        [Route("{dataType}/salary/add")]
        [AllowAnonymous]
        public MessageViewModel add([FromBody] RequestSalaryViewModel entity,string dataType)
        {
            var result = _handler.Add(entity,dataType);
            return result;
        }

        [HttpGet]
        [Route("salary/remove/{id}")]
        [AllowAnonymous]
        public MessageViewModel remove(int id)
        {
            var result = _service.Delete(id);
            return result;
        }

        [HttpGet]
        [Route("salary/get/{id}")]
        [AllowAnonymous]
        public ResultViewModel<SalaryViewModel> get(int id)
        {
            var result = _service.GetByID(id);
            return result;
        }


        [HttpPut]
        [Route("salary/update")]
        [AllowAnonymous]
        public MessageViewModel update([FromBody] Salary entity)
        {
            var result = _service.Update(entity);
            return result;
        }

        //[HttpGet]
        //[Route("salary/getAll/{filter}")]
        //[AllowAnonymous]
        //public ResultViewModel<SalaryViewModel> getAll(string filter)
        //{
        //    var result = _service.GetAll();
        //    return result;
        //}
    }
}
