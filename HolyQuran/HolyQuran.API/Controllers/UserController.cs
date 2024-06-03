using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HolyQuran.Core.Constants;
using HolyQuran.Core.Dtos;
using HolyQuran.Infrastructure.Services.Interfaces;
using HolyQuran.Core.ViewModels;

namespace HolyQuran.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IInterfaceServices _interfaceServices;

        public UserController(IInterfaceServices interfaceServices)
        {
            _interfaceServices = interfaceServices;
        }
        [HttpGet]
        public IActionResult GetDataModel(int page = Constant.NumberOne)
        {
            var data = _interfaceServices.userService.GetAll(page);
            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
        }
    
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateApplicationUserDto dto)
        {
            // Your logic to create the user goes here
            // You can access userDto properties to create the user
            if (ModelState.IsValid)
            {
                 var result = await  _interfaceServices.userService.Create(dto);
                if (result.Succeeded)
                {
                    return Ok(GetRespons(MessageResults.AddSuccessResult()));

                }
                return Ok(GetRespons(MessageResults.ErrorResult()));
            }
            return Ok(GetRespons(MessageResults.ErrorResult()));
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateApplicationUserDto dto)
        {
            //if (ModelState.IsValid)
            //{
            var result = await _interfaceServices.userService.Update(dto);

            if (result.Succeeded)
            {
                return Ok(GetRespons(MessageResults.EditSuccessResult()));

            }
            //}
            return Ok(GetRespons(dto,MessageResults.ErrorResult()));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _interfaceServices.userService.Delete(id);
            return Ok(GetRespons(MessageResults.DeleteSuccessResult()));
        }


        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var data = await _interfaceServices.userService.Get(id);
            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
        }

    }
}
