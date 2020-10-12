using AutoMapper;
using HomeWork_ToDos.CommonLib.Contracts.DbOps;
using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Models.DbModels;
using HomeWork_ToDos.DAL.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork_ToDos.DAL
{
    public class ToDoItemDbOps : IToDoItemDbOps
    {
        private readonly ToDoDbContext _toDoDbContext;
        private readonly IMapper _mapper;
        public ToDoItemDbOps(ToDoDbContext toDoDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _toDoDbContext = toDoDbContext;
        }

        /// <summary>
        /// Gets all the todoitem records..
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>All ToDoItem records for the logged in user.</returns>
        public async Task<List<ToDoItemDto>> GetAllToDoItems(long userId)
        {
            List<ToDoItemDbModel> toDoItems = await _toDoDbContext.ToDoItems
                .Include(p => p.Labels).ThenInclude(p => p.Labels)
                .Where(p => p.CreatedBy == userId).ToListAsync();
            return _mapper.Map<List<ToDoItemDto>>(toDoItems);
        }

        /// <summary>
        /// Gets todoitem records for specific todolist record.
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="userId"></param>
        /// <returns> List of ToDoItem records corresponding to specific ToDoListId. </returns>
        public async Task<List<ToDoItemDto>> GetToDoItemsForToDoListId(long listId, long userId)
        {
            List<ToDoItemDbModel> toDoItems = await _toDoDbContext.ToDoItems.Include(p => p.Labels)
                .Where(p => p.CreatedBy == userId && p.ToDoListId == listId).ToListAsync();
            return _mapper.Map<List<ToDoItemDto>>(toDoItems);
        }

        /// <summary>
        /// Gets specific todoitem record.
        /// </summary>
        /// <param name="toDoItemId"></param>
        /// <param name="userId"></param>
        /// <returns>ToDoItem record for the given Id.</returns>
        public async Task<ToDoItemDto> GetToDoItemById(long toDoItemId, long userId)
        {
            ToDoItemDbModel toDoItemDbDto = await _toDoDbContext.ToDoItems.Include(p => p.Labels).FirstOrDefaultAsync(p => p.ToDoItemId == toDoItemId && p.CreatedBy == userId);
            return _mapper.Map<ToDoItemDto>(toDoItemDbDto);
        }
        /// <summary>
        /// Adds ToDoItem record to ToDoItem table.
        /// </summary>
        /// <param name="createToDoItemDto"></param>
        /// <returns>added ToDoItem record.</returns>
        public async Task<ToDoItemDto> AddToDoItem(CreateToDoItemDto createToDoItemDto)
        {
            ToDoItemDbModel toDoItemDbDto = _mapper.Map<ToDoItemDbModel>(createToDoItemDto);
            toDoItemDbDto.CreationDate = DateTime.UtcNow;
            _toDoDbContext.ToDoItems.Add(toDoItemDbDto);
            await _toDoDbContext.SaveChangesAsync();
            return _mapper.Map<ToDoItemDto>(toDoItemDbDto);
        }
        /// <summary>
        /// Updates todoitem record based on input.
        /// </summary>
        /// <param name="updateToDoItemDto">ToDoItemObject to be updated.</param>
        /// <returns> Updated ToDoItem record.</returns>
        public async Task<ToDoItemDto> UpdateToDoItem(UpdateToDoItemDto updateToDoItemDto)
        {
            ToDoItemDbModel toDoItemDbDto = await _toDoDbContext.ToDoItems
                .FirstOrDefaultAsync(p => p.ToDoItemId == updateToDoItemDto.ToDoItemId);
            if (toDoItemDbDto == null)
                return null;
            toDoItemDbDto.Notes = updateToDoItemDto.Notes;
            toDoItemDbDto.UpdationDate = DateTime.UtcNow;

            await _toDoDbContext.SaveChangesAsync();
            return _mapper.Map<ToDoItemDto>(toDoItemDbDto);
        }
        /// <summary>
        /// Delete ToDoItem record based on ToDoItemId passed.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns> 1 if success else -1. </returns>
        public async Task<int> DeleteToDoItem(long id, long userId)
        {
            ToDoItemDbModel toDoItemDbDto = await _toDoDbContext.ToDoItems
                .FirstOrDefaultAsync(p => p.ToDoItemId == id && p.CreatedBy == userId);
            if (toDoItemDbDto == null)
                return 0;
            _toDoDbContext.ToDoItems.Remove(toDoItemDbDto);
            return await _toDoDbContext.SaveChangesAsync();
        }

    }
}
