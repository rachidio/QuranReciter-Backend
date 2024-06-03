using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HolyQuran.Core.Constants;
using HolyQuran.Core.Dtos;
using HolyQuran.Infrastructure.Services.Interfaces;
using HolyQuran.Core.ViewModels;

namespace HolyQuran.API.Controllers
{
    public class TeacherController : BaseController
    {
        private readonly IInterfaceServices _interfaceServices;

        public TeacherController(IInterfaceServices interfaceServices)
        {
            _interfaceServices = interfaceServices;
        }
        [HttpGet]
        public IActionResult GetDataModel(int page = Constant.NumberOne)
        {
            var data = _interfaceServices.teacherService.GetAll(page);
            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
        }
    
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateTeacherDto dto)
        {
            // Your logic to create the user goes here
            // You can access userDto properties to create the user
            if (ModelState.IsValid)
            {
                 var user = await  _interfaceServices.teacherService.Create(dto);
                 return Ok(GetRespons(MessageResults.AddSuccessResult()));
            }
            return Ok(dto);
            
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateTeacherDto dto)
        {
            var result = await _interfaceServices.teacherService.Update(dto);
            if (result.Succeeded)
            {
                return Ok(GetRespons(MessageResults.EditSuccessResult()));
            }
            return Ok(GetRespons(dto, MessageResults.ErrorResult()));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string ApplicationUserId)
        {
            await _interfaceServices.teacherService.Delete( ApplicationUserId);
            return Ok(GetRespons(MessageResults.DeleteSuccessResult()));
        }


        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _interfaceServices.teacherService.Get(id);
            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
        }

    }
}
