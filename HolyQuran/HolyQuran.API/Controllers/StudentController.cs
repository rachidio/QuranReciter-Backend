using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HolyQuran.Core.Constants;
using HolyQuran.Core.Dtos;
using HolyQuran.Infrastructure.Services.Interfaces;
using HolyQuran.Core.ViewModels;
using HolyQuran.Core.Enums;

namespace HolyQuran.API.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IInterfaceServices _interfaceServices;

        public StudentController(IInterfaceServices interfaceServices)
        {
            _interfaceServices = interfaceServices;
        }
        [HttpGet]
        public IActionResult GetDataModel(int page = Constant.NumberOne)
        {
            var data = _interfaceServices.studentService.GetAll(page);
            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateStudentDto dto)
        {
            // Your logic to create the user goes here
            // You can access userDto properties to create the user
            if (ModelState.IsValid)
            {
                var user = await _interfaceServices.studentService.Create(dto);
                return Ok(GetRespons(MessageResults.AddSuccessResult()));
            }
            return Ok(GetRespons(MessageResults.ErrorResult()));
        }
        [HttpPost]
        public async Task<IActionResult> ChangeLevelType(int id,LevelType levelType)
        {
            // Your logic to create the user goes here
            // You can access userDto properties to create the user
            if (ModelState.IsValid)
            {
                await _interfaceServices.studentService.ChangeLevelType(id,levelType);
                return Ok(GetRespons(MessageResults.ChangeStatusSuccessResult()));
            }
            return Ok(GetRespons(MessageResults.ErrorResult()));

        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateStudentDto dto)
        {
            var result = await _interfaceServices.studentService.Update(dto);
            if (result.Succeeded)
            {
                return Ok(GetRespons(MessageResults.EditSuccessResult()));
            }
            return Ok(GetRespons(dto, MessageResults.ErrorResult()));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string ApplicationUserId)
        {
            await _interfaceServices.studentService.Delete(ApplicationUserId);
            return Ok(GetRespons(MessageResults.DeleteSuccessResult()));
        }


        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _interfaceServices.studentService.Get(id);
            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
        }

    }
}