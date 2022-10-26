using Explorer.Models.Entities;
using Explorer.Models.ViewModels;
using System.Threading.Tasks;

namespace Explorer.Services.Interfaces
{
    public interface IFileService
    {
        public Task<Response<File>> AddFileAsync(FileVm model);
        public Task<Response<string>> RenameFileAsync(RenameModelVm model);
        public Task<Response<string>> DeleteFileAsync(int id);
    }
}
