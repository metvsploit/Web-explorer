using Explorer.DataAccess;
using Explorer.Models.Entities;
using Explorer.Models.ViewModels;
using Explorer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Explorer.Services.Implementation
{
    public class FileService : IFileService
    {
        private readonly explorerdbContext _db;

        public FileService(explorerdbContext db) => _db = db;

        public async Task<Response<File>> AddFileAsync(FileVm model)
        {
            var response = new Response<File>();
            try
            {
                var entity = await _db.Files.FirstOrDefaultAsync(x => x.FileName == model.FileName && x.FolderId == model.FolderId);
                if(entity != null)
                {
                    response.Success = false;
                    response.Message = "Файл с таким имененм уже существует";
                    return response;
                }

                var file = new File
                {
                    FileName = model.FileName,
                    TypeId = model.TypeId == 0 ? null : model.TypeId,
                    FolderId = model.FolderId,
                    Content = model.Content,
                    Description = model.Description,
                };

                await _db.Files.AddAsync(file);
                await _db.SaveChangesAsync();

                file.Type = await _db.Types.FirstOrDefaultAsync(x => x.TypeId == file.TypeId);
                if(file.Type == null)
                {
                    file.Type = new Models.Entities.Type
                            { TypeId = 0, Format = "other", Icon = "./images/unknown.png" };
                }

                response.Data = file;
                response.Message = "Файл успешно добавлен";
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                return new Response<File> { Message = ex.Message, Success = false };
            }
        }

        public async Task<Response<string>> DeleteFileAsync(int id)
        {
            var response = new Response<string>();

            try
            {
                var file = await _db.Files.FirstOrDefaultAsync(x => x.FileId == id);
                if(file == null)
                {
                    response.Message = "Файл не найден";
                    response.Data = id.ToString();
                    response.Success = false;
                }
                _db.Files.Remove(file);
                await _db.SaveChangesAsync();
                response.Message = "Файл удален";
                response.Success = true;
                return response;
            }
            catch(Exception ex)
            {
                return new Response<string> { Message = ex.Message, Success = false };
            }
        }

        public async Task<Response<string>> RenameFileAsync(RenameModelVm model)
        {
            var response = new Response<string>();

            try
            {
                var file = await _db.Files.FirstOrDefaultAsync(x => x.FileId == model.Id);
                if (file == null)
                {
                    response.Message = "Файл не найден";
                    response.Data = model.Id.ToString();
                    response.Success = false;
                }

                file.FileName = model.Name;
                _db.Update(file);
                await _db.SaveChangesAsync();
                response.Message = "Файл успешно переименован";
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                return new Response<string> { Message = ex.Message, Success = false };
            }
        }
    }
}
