using Explorer.Models.DTO;
using Explorer.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Explorer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _typeService;
        private readonly IWebHostEnvironment _appEnvironment;

        public TypeController(ITypeService typeService, IWebHostEnvironment appEnvironment)
        {
            _typeService = typeService;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetTypes()
        {
            var response = await _typeService.GetTypesAsync();

            if(!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddTypes([FromForm(Name ="Icon")]IFormFile formFile, string id)
        {
            if(formFile == null)
                return BadRequest();

            string path = _appEnvironment.WebRootPath + "/images/" + formFile.FileName;

            using (var fileStream = new FileStream(path, FileMode.Create))
                await formFile.CopyToAsync(fileStream);
 
            var type = new TypeDto { Format = id, Icon = "./images/"+formFile.FileName };

            var response = await _typeService.AddTypeAsync(type);

            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            var response = await _typeService.DeleteTypeAsync(id);

            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
