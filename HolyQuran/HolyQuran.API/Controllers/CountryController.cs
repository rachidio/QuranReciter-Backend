﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HolyQuran.Core.Constants;
using HolyQuran.Core.Dtos;
using HolyQuran.Infrastructure.Services.Interfaces;
using HolyQuran.Core.ViewModels;

namespace HolyQuran.API.Controllers
{
    public class CountryController : BaseController
    {
        private readonly IInterfaceServices _interfaceServices;

        public CountryController(IInterfaceServices interfaceServices)
        {
            _interfaceServices = interfaceServices;
        }
        [HttpGet]
        public IActionResult GetDataModel(int page = Constant.NumberOne)
        {
            var data = _interfaceServices.countriesService.GetAll(page);
            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
        }
    
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            // Your logic to create the user goes here
            // You can access userDto properties to create the user
            if (ModelState.IsValid)
            {
                 await  _interfaceServices.countriesService.CreateDefaultData();
                 return Ok(GetRespons(MessageResults.AddSuccessResult()));
            }
            return Ok(GetRespons(MessageResults.ErrorResult()));
            
        }
      
    }
}
