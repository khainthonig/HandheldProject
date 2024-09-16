using Backend_Handheld.Entities.Const;
using Backend_Handheld.Entities.DataTransferObjects.Classification;
using Backend_Handheld.Services.Interfaces;
using HandheldProject.Entities.DataTransferObjects.Setting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandheldProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public SettingController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet("get-settings")]
        public async Task<IActionResult> GetSettings()
        {
            List<SettingDto> lstDto;
            lstDto = await _serviceManager.SettingService.GetAll();
            if (lstDto == null) lstDto = new();
            return Ok(lstDto);
        }

        [HttpPost("create-setting")]
        public async Task<IActionResult> CreateSetting([FromBody] SettingCreateDto createDto)
        {
            try
            {
                var result = await _serviceManager.SettingService.Create(createDto);
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
        [HttpGet("id={id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _serviceManager.SettingService.GetById(id);
            if (dto == null) return BadRequest("No data.");
            return Ok(dto);
        }
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingUpdateDto updateDto)
        {
            var result = await _serviceManager.SettingService.Update(updateDto);
            if (result) { return Ok(updateDto); }
            return BadRequest(MessageError.ErrorUpdate);
        }
        [HttpGet("get-setting-by-classification-and-user")]
        public async Task<IActionResult> GetByClassificationAndUser(int classificationId, int userId)
        {
            var dto = await _serviceManager.SettingService.GetByClassificationAndUser(classificationId, userId);
            if (dto == null) return BadRequest("No setting for this classification and user");
            return Ok(dto);
        }
    }
}
