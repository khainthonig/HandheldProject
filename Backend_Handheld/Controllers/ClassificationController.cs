using Backend_Handheld.Entities.Const;
using Backend_Handheld.Entities.DataTransferObjects.Classification;
using Backend_Handheld.Entities.DataTransferObjects.User;
using Backend_Handheld.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Handheld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassificationController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public ClassificationController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet("get-classifications")]
        public async Task<IActionResult> GetClassifications()
        {
            List<ClassificationDto> lstDto;
            lstDto = await _serviceManager.ClassificationService.GetAll();
            if (lstDto == null) lstDto = new();
            return Ok(lstDto);
        }
        [HttpPost("create-classification")]
        public async Task<IActionResult> CreateClassification([FromBody] ClassificationCreateDto createDto)
        {
            try
            {
                var result = await _serviceManager.ClassificationService.Create(createDto);
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
        [HttpGet("get-classification-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _serviceManager.ClassificationService.GetById(id);
            if (dto == null) return NoContent();
            return Ok(dto);
        }
    }
}
