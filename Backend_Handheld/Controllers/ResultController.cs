using Backend_Handheld.Entities.Const;
using Backend_Handheld.Entities.DataTransferObjects.Classification;
using Backend_Handheld.Entities.DataTransferObjects.Result;
using Backend_Handheld.Services.Interfaces;
using HandheldProject.Entities.Enum;
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
        [HttpGet("id={id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _serviceManager.ResultService.GetById(id);
            if (dto == null) return BadRequest("No data.");
            return Ok(dto);
        }
        [HttpPost("upload")]
        public async Task<ActionResult> UploadImageResult([FromForm] ResultCreateDto resultDto, [FromForm] IFormFile file)
        {
            var uploadSuccess = await _serviceManager.ResultService.Upload(file);
            if (!uploadSuccess)
            {
                return BadRequest("File upload failed.");
            }

            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Images", timestamp);
            var filePath = Path.Combine(uploadPath, file.FileName);
            resultDto.Image = filePath;

            var createSuccess = await _serviceManager.ResultService.Create(resultDto);
            if (!createSuccess)
            {
                return BadRequest("Failed to create result.");
            }

            return Ok("File uploaded and result created successfully.");
        }

        [HttpPost("upload-to-cloudinary")]
        public async Task<ActionResult> UploadImageToCloudinary([FromForm] ResultCreateDto resultDto, [FromForm] IFormFile file)
        {
            var uploadResult = await _serviceManager.ResultService.UploadToCloudDinary(file);
            if (uploadResult == null)
            {
                return BadRequest("File upload to Cloudinary failed.");
            }
            resultDto.Image = uploadResult.ToString();
            var createSuccess = await _serviceManager.ResultService.Create(resultDto);
            if (!createSuccess)
            {
                return BadRequest("Failed to create result.");
            }
            return Ok(resultDto);
        }
        [HttpPost("search")]
        public async Task<IActionResult> Search(ResultSearchDto searchDto)
        {
            var userId = searchDto.UserId;
            var user = new Entities.DataTransferObjects.User.UserDto();
            if (userId != null)
            {
                user = await _serviceManager.UserService.GetById((int)userId);
            }
            if (user.Role == (int)RoleUser.OPERATOR)
            {
                searchDto.UserId = userId;
            }
            else
            {
                searchDto.UserId = null;
            }
            var result = await _serviceManager.ResultService.Search(searchDto);
            if (result == null) return BadRequest("No data");
            return Ok(result);
        }


        [HttpPost("search-by-condition")]
        public async Task<IActionResult> SearchByCondition(ResultSearchDto searchDto)
        {
            var userId = searchDto.UserId;
            var user = new Entities.DataTransferObjects.User.UserDto();
            if(userId != null)
            {
                user = await _serviceManager.UserService.GetById((int)userId);
            }
            if(user.Role == (int)RoleUser.OPERATOR)
            {
                searchDto.UserId = userId;
            }
            else
            {
                searchDto.UserId = null;
            }
            var result = await _serviceManager.ResultService.GetListByCondition(searchDto);
            if (result == null) return BadRequest("No data");         
            return Ok(result);
        }

    }
}
