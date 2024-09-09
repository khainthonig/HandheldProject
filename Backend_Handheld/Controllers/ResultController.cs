﻿using Backend_Handheld.Entities.Const;
using Backend_Handheld.Entities.DataTransferObjects.Classification;
using Backend_Handheld.Entities.DataTransferObjects.Result;
using Backend_Handheld.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Handheld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public ResultController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet("get-results")]
        public async Task<IActionResult> GetResults()
        {
            List<ResultDto> lstDto;
            lstDto = await _serviceManager.ResultService.GetAll();
            if (lstDto == null) lstDto = new();
            return Ok(lstDto);
        }
        [HttpPost("create-result")]
        public async Task<IActionResult> CreateResult([FromBody] ResultCreateDto createDto)
        {
            try
            {
                var result = await _serviceManager.ResultService.Create(createDto);
                if (result)
                {
                    return Ok();
                }
                return BadRequest(MessageError.ErrorCreate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-result-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _serviceManager.ResultService.GetById(id);
            if (dto == null) return NoContent();
            return Ok(dto);
        }
    }
}
