using Explorer.Models.DTO;
using Explorer.Models.Entities;
using Explorer.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Explorer.Services.Interfaces
{
    public interface ITypeService
    {
        public Task<Response<List<Type>>> GetTypesAsync();
        public Task<Response<int>> AddTypeAsync(TypeDto model);
        public Task<Response<int>> DeleteTypeAsync(int id);

    }
}
