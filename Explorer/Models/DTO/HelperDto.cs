using Explorer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Explorer.Models.DTO
{
    public static class HelperDto
    {
        public static Expression<Func<Folder, FolderDto>> GetItemsProjection(int maxDepth, int currentDepth = 0)
        {
            currentDepth++;

            Expression<Func<Folder, FolderDto>> result = items => new FolderDto()
            {
                FolderId = items.FolderId,
                Files = items.Files.AsQueryable().Include(x => x.Type).OrderBy(x => x.FileName).ToList(),
                ParentId = items.ParentId,
                Folders = currentDepth == maxDepth
                    ? new List<FolderDto>()
                    : items.Folders.AsQueryable()
                            .Select(GetItemsProjection(maxDepth, currentDepth)).OrderBy(x => x.Name).ToList(),
                Name = items.Name,
            };

            return result;
        }

    }
}
