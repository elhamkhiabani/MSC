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


        [HttpGet]
        [Route("salary/remove/{id}")]
        [AllowAnonymous]
        public MessageViewModel remove(int id)
        {
            var result = _service.Delete(id);
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


        [HttpPost]
        [Route("{dataType}/salary/add")]
        [AllowAnonymous]
        public MessageViewModel add([FromBody] RequestSalaryViewModel entity, string dataType)
        {
            var result = _handler.Add(entity, dataType);
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

        [HttpPost]
        [Route("salary/getMonthlyByFullname")]
        [AllowAnonymous]
        public ResultViewModel<SalaryViewModel> getMonthlyByFullname([FromBody] FilterViewModel entity)
        {
            var result = _service.SelectByFullName(entity);
            return result;
        }


      
        [HttpPost]
        [Route("salary/selectAll")]
        [AllowAnonymous]
        public ResultViewModel<SalaryViewModel> selectAll([FromBody] FilterViewModel entity)
        {
            var result = _service.SelectAll(entity);
            return result;
        }
    }
}
