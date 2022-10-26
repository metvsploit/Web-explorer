using Explorer.Models.ViewModels;
using Explorer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Explorer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FolderController:ControllerBase
    {
        private readonly IFolderService _folderService;

        public FolderController(IFolderService folderService) => _folderService = folderService;

        [HttpPost]
        public async Task<IActionResult> AddFolderAsync([FromBody] FolderVm folder)
        {
            if(ModelState.IsValid)
            {
                var response = await _folderService.AddAsync(folder);

                if (!response.Success)
                    return BadRequest(response);
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> RenameFolderAsync([FromBody] RenameModelVm folder)
        {
            if(ModelState.IsValid)
            {
                var response = await _folderService.RenameAsync(folder);

                if (!response.Success)
                    return BadRequest(response);
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _folderService.DeleteByIdAsync(id);

            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("project")]
        public async Task<IActionResult> CreateProject(FolderVm model)
        {
            if (ModelState.IsValid)
            {
                var response = await _folderService.CreateProject(model);

                if (!response.Success)
                    return BadRequest(response);
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
