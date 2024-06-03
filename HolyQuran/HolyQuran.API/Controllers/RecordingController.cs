using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HolyQuran.Core.Constants;
using HolyQuran.Core.Dtos;
using HolyQuran.Infrastructure.Services.Interfaces;
using HolyQuran.Core.ViewModels;
using HolyQuran.Core.Enums;
using HolyQuran.Core.General;

namespace HolyQuran.API.Controllers
{
    public class RecordingController : BaseController
    {
        private readonly IInterfaceServices _interfaceServices;

        public RecordingController(IInterfaceServices interfaceServices)
        {
            _interfaceServices = interfaceServices;
        }
        [HttpGet]
        public IActionResult GetDataModel(QueryDto filter)
        {
            var data = _interfaceServices.recordingService.GetAll(filter);
            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
        }
    
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateRecordingDto dto)
        {
            // Your logic to create the user goes here
            // You can access userDto properties to create the user
            if (ModelState.IsValid)
            {
                 await  _interfaceServices.recordingService.Create(dto);
                 return Ok(GetRespons(MessageResults.AddSuccessResult()));
            }
            return Ok(GetRespons(MessageResults.ErrorResult()));
            
        }
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id,RecordingStatus status)
        {
            // Your logic to create the user goes here
            // You can access userDto properties to create the user
            if (ModelState.IsValid)
            {
                await _interfaceServices.recordingService.ChangeStatus(id, status);
                return Ok(GetRespons(MessageResults.ChangeStatusSuccessResult()));
            }
            return Ok(GetRespons(MessageResults.ErrorResult()));

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _interfaceServices.recordingService.Delete(id);
            return Ok(GetRespons(MessageResults.DeleteSuccessResult()));
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _interfaceServices.recordingService.Get(id);
            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
        }

    }
}
