using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HolyQuran.Core.Constants;
using HolyQuran.Core.Dtos;
using HolyQuran.Infrastructure.Services.Interfaces;
using HolyQuran.Core.ViewModels;

namespace HolyQuran.API.Controllers
{
    public class SpecificNotesController : BaseController
    {
        private readonly IInterfaceServices _interfaceServices;

        public SpecificNotesController(IInterfaceServices interfaceServices)
        {
            _interfaceServices = interfaceServices;
        }
        [HttpGet]
        public IActionResult GetDataModel(int page = Constant.NumberOne)
        {
            var data = _interfaceServices.specificNoteService.GetAll(page);
            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
        }
    
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateSpecificNoteDto dto)
        {
            // Your logic to create the user goes here
            // You can access userDto properties to create the user
            if (ModelState.IsValid)
            {
                 await  _interfaceServices.specificNoteService.Create(dto);
                 return Ok(GetRespons(MessageResults.AddSuccessResult()));
            }
            return Ok(GetRespons(MessageResults.ErrorResult()));
            
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateSpecificNoteDto dto)
        {
            if (ModelState.IsValid)
            {
                await _interfaceServices.specificNoteService.Update(dto);
                return Ok(GetRespons(MessageResults.EditSuccessResult()));
            }
            return Ok(GetRespons(dto,MessageResults.ErrorResult()));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _interfaceServices.specificNoteService.Delete(id);
            return Ok(GetRespons(MessageResults.DeleteSuccessResult()));
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _interfaceServices.specificNoteService.Get(id);
            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
        }

    }
}
