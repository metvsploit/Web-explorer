using Explorer.DataAccess;
using Explorer.Models.DTO;
using Explorer.Models.ViewModels;
using Explorer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Explorer.Services.Implementation
{
    public class TypeService : ITypeService
    {
        private readonly explorerdbContext _db;
        public TypeService(explorerdbContext db) => _db = db;
        public async Task<Response<int>> AddTypeAsync(TypeDto model)
        {
            var response = new Response<int>();

            try
            {
                var type = await _db.Types.FirstOrDefaultAsync(x => x.Format == model.Format);
                if(type != null)
                {
                    response.Success = false;
                    response.Message = "Такой формат уже добавлен";
                    response.Data = type.TypeId;
                    return response;
                }
                var entity = new Models.Entities.Type { Format = model.Format, Icon = model.Icon };

                await _db.Types.AddAsync(entity);
                await _db.SaveChangesAsync();

                response.Success = true;
                response.Message = "Формат успешно добавлен";
                response.Data = entity.TypeId;
                return response;
            }
            catch(Exception ex)
            {
                return new Response<int> { Message = ex.Message, Success = false};
            }
        }

        public async Task<Response<int>> DeleteTypeAsync(int id)
        {
            var response = new Response<int>();

            try
            {
                var type = await _db.Types.FirstOrDefaultAsync(x => x.TypeId == id);
                if (type == null)
                {
                    response.Success = false;
                    response.Message = "Данного формат не существует";
                    response.Data = id;
                    return response;
                }
                _db.Types.Remove(type);
                await _db.SaveChangesAsync();

                response.Success = true;
                response.Message = $"Формат {type.Format} успешно удален";
                response.Data = id;
                return response;
            }
            catch(Exception ex)
            {
                return new Response<int> { Message = ex.Message, Success = false, Data = id };
            }
        }

        public async Task<Response<List<Models.Entities.Type>>> GetTypesAsync()
        {
            var response = new Response<List<Models.Entities.Type>>();

            try
            {
                var types = await _db.Types.ToListAsync();
                
                response.Success = true;
                response.Message = "Успешное выполнение запроса";
                response.Data = types;
                return response;
            }
            catch (Exception ex)
            {
                return new Response<List<Models.Entities.Type>> { Message = ex.Message, Success = false };
            }
        }
    }
}
