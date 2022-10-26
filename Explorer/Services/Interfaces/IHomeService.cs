using Explorer.Models.DTO;
using Explorer.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Explorer.Services.Interfaces
{
    public interface IHomeService
    {
        public Task<Response<List<FolderDto>>> GetTreeNodeAsync();
    }
}
