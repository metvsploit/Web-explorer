using Explorer.Models.ViewModels;
using Explorer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Explorer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService) => _fileService = fileService;

        [HttpPost]
        public async Task<IActionResult> AddFile([FromBody] FileVm model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var response = await _fileService.AddFileAsync(model);
            if(!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> RenameFile([FromBody] RenameModelVm model)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            var response = await _fileService.RenameFileAsync(model);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var response = await _fileService.DeleteFileAsync(id);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
