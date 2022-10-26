using Explorer.DataAccess;
using Explorer.Models.DTO;
using Explorer.Models.ViewModels;
using Explorer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Explorer.Services.Implementation
{
    public class HomeService:IHomeService
    {
        private readonly explorerdbContext _db;

        public HomeService(explorerdbContext db) => _db = db;

        public async Task<Response<List<FolderDto>>> GetTreeNodeAsync()
        {
            var response = new Response<List<FolderDto>>();

            try
            {
                var folders = await _db.Folders.Where(x => x.ParentId == null)
                                                .Select(HelperDto.GetItemsProjection(10, 0))
                                                .ToListAsync();
                response.Data = folders;
                response.Message = "Успешное выполнение запроса";
                response.Success = true;
                return response;
            }
            catch
            {
                return new Response<List<FolderDto>>
                { Success = false, Message = "Не удалось подключиться к базе данных"};
            }         
        }
    }
}
