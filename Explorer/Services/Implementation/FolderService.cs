using Explorer.DataAccess;
using Explorer.Models.Entities;
using Explorer.Models.ViewModels;
using Explorer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Explorer.Services.Implementation
{
    public class FolderService : IFolderService
    {
        private readonly explorerdbContext _db;

        public FolderService(explorerdbContext db) => _db = db;

        public async Task<Response<int>> AddAsync(FolderVm model)
        {
            var response = new Response<int>();

            try
            {
                var entity = await _db.Folders
                    .FirstOrDefaultAsync(x => x.Name == model.Name && x.ParentId == model.ParentId);

                if (entity != null)
                {
                    response.Success = false;
                    response.Message = "Папка с таким именем в данном каталоге уже существует";
                    return response;
                }

                var folder = new Folder
                {
                    Name = model.Name,
                    ParentId = model.ParentId
                };

                await _db.Folders.AddAsync(folder);
                await _db.SaveChangesAsync();
                response.Success = true;
                response.Message = "Папка успешно создана";
                response.Data = folder.FolderId;
                return response;
            }
            catch(Exception ex)
            {
                return new Response<int>
                { Success = false, Message = ex.Message };
            }
        }

        public async Task<Response<int>> DeleteByIdAsync(int id)
        {
            var response = new Response<int>();

            try
            {
                var entity = await _db.Folders.FirstOrDefaultAsync(x => x.FolderId == id);

                if (entity == null)
                {
                    response.Success = false;
                    response.Message = "Папка не найдена";
                    response.Data = id;
                    return response;
                }

                _db.Folders.Remove(entity);
                await _db.SaveChangesAsync();

                response.Success = true;
                response.Message = $"Папка {entity.Name} успешно удалена";
                response.Data = id;
                return response;
            }
            catch(Exception ex)
            {
                return new Response<int>
                { Success = false, Message = ex.Message, Data = id };
            }
        }

        public async Task<Response<int>> RenameAsync(RenameModelVm model)
        {
            var response = new Response<int>();

            try
            {
                var entity = await _db.Folders.FirstOrDefaultAsync(x => x.FolderId == model.Id);
                if(entity == null)
                {
                    response.Success = false;
                    response.Message = "Папка не найдена";
                    response.Data = model.Id;
                    return response;
                }

                entity.Name = model.Name;
                _db.Folders.Update(entity);
                await _db.SaveChangesAsync();
                response.Success = true;
                response.Message = "Папка переименована";
                response.Data = model.Id;
                return response;
            }
            catch (Exception ex)
            {
                return new Response<int>
                { Success = false, Message = ex.Message, Data = model.Id };
            }
        }

        public async Task<Response<int>> CreateProject(FolderVm model)
        {
            var response = new Response<int>();

            try
            {
                var entity = await _db.Folders
                    .FirstOrDefaultAsync(x => x.ParentId == null && x.Name == model.Name);

                if(entity != null)
                {
                    response.Message = "Проект с таким именем уже существует";
                    response.Success = false;
                    return response;
                }
                var project = new Folder
                {
                    ParentId = null,
                    Name = model.Name,
                };

                await _db.Folders.AddAsync(project);
                await _db.SaveChangesAsync();

                response.Success = true;
                response.Message = "Проект успешно создан";
                response.Data = project.FolderId;
                return response;
            }
            catch(Exception ex)
            {
                return new Response<int> { Message = ex.Message, Success = false };
            }
        }
    }
}
