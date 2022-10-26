using Explorer.Models.ViewModels;
using System.Threading.Tasks;

namespace Explorer.Services.Interfaces
{
    public interface IFolderService
    {
        public Task<Response<int>> AddAsync(FolderVm model);
        public Task<Response<int>> DeleteByIdAsync(int id);
        public Task<Response<int>> RenameAsync(RenameModelVm model);
        public Task<Response<int>> CreateProject(FolderVm model);
    }
}
