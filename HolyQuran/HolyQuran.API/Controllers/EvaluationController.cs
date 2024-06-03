using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HolyQuran.Core.Constants;
using HolyQuran.Core.Dtos;
using HolyQuran.Infrastructure.Services.Interfaces;
using HolyQuran.Core.ViewModels;
using HolyQuran.Data.Data;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.InkML;
using HolyQuran.Data.Models;
using Humanizer;
using HolyQuran.Core.General;

namespace HolyQuran.API.Controllers
{
    public class EvaluationController : BaseController
    {
        private readonly IInterfaceServices _interfaceServices;
        private readonly ApplicationDbContext _db;
        public EvaluationController(IInterfaceServices interfaceServices, ApplicationDbContext db)
        {
            _interfaceServices = interfaceServices;
            _db = db;
        }
        [HttpGet]
        public IActionResult GetDataModel(QueryDto filter)
        {
            var data = _interfaceServices.evaluationService.GetAll(filter);
            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
        }
    
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateEvaluationDto dto)
        {
            // Your logic to create the user goes here
            // You can access userDto properties to create the user
            if (ModelState.IsValid)
            {
                 await  _interfaceServices.evaluationService.Create(dto);
                 return Ok(GetRespons(MessageResults.AddSuccessResult()));
            }
            return Ok(GetRespons(MessageResults.ErrorResult()));
            
        }
        [HttpPost]
        public async Task<IActionResult> ChangeSumbitStatus(int id)
        {
            // Your logic to create the user goes here
            // You can access userDto properties to create the user
            if (ModelState.IsValid)
            {
                await _interfaceServices.evaluationService.ChangeSumbitStatus(id);
                return Ok(GetRespons(MessageResults.ChangeStatusSuccessResult()));
            }
            return Ok(GetRespons(MessageResults.ErrorResult()));

        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateEvaluationDto dto)
        {
            if (ModelState.IsValid)
            {
                await _interfaceServices.evaluationService.Update(dto);
                return Ok(GetRespons(MessageResults.EditSuccessResult()));
            }
            return Ok(GetRespons(dto,MessageResults.ErrorResult()));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _interfaceServices.evaluationService.Delete(id);
            return Ok(GetRespons(MessageResults.DeleteSuccessResult()));
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _interfaceServices.evaluationService.Get(id);
            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
        }

    }
}
