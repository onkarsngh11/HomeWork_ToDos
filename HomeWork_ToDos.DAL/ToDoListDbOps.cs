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
    public class ToDoListDbOps : IToDoListDbOps
    {
        private readonly ToDoDbContext _toDoDbContext;
        private readonly IMapper _mapper;
        public ToDoListDbOps(ToDoDbContext toDoDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _toDoDbContext = toDoDbContext;
        }
        /// <summary>
        /// Gets all the todolist records.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>All ToDoList items for logged in User.</returns>
        public async Task<List<ToDoListDto>> GetAllToDoLists(long userId)
        {
            List<ToDoListDbModel> toDoLists = await _toDoDbContext.ToDoLists
                .Include(p => p.Labels).ThenInclude(p => p.Labels)
                .Include(p => p.ToDoItems)
                .Where(p => p.CreatedBy == userId).ToListAsync();
            return _mapper.Map<List<ToDoListDto>>(toDoLists);
        }
        /// <summary>
        /// Get specific todolist record.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="userId"></param>
        /// <returns>ToDoList item by Id for logged in user</returns>
        public async Task<ToDoListDto> GetToDoListById(long Id, long userId)
        {
            ToDoListDbModel toDoListDbDto = await _toDoDbContext.ToDoLists
                .Include(p => p.ToDoItems).Include(p => p.Labels).FirstOrDefaultAsync(p => p.ToDoListId == Id && p.CreatedBy == userId);
            return _mapper.Map<ToDoListDto>(toDoListDbDto);
        }
        /// <summary>
        /// Adds ToDoList record to ToDoList table.
        /// </summary>
        /// <param name="createToDoListDto"></param>
        /// <returns> added ToDoList record. </returns>
        public async Task<ToDoListDto> CreateToDoList(CreateToDoListDto createToDoListDto)
        {
            ToDoListDbModel toDoListDbDto = _mapper.Map<ToDoListDbModel>(createToDoListDto);
            toDoListDbDto.CreationDate = DateTime.UtcNow;
            _toDoDbContext.ToDoLists.Add(toDoListDbDto);
            await _toDoDbContext.SaveChangesAsync();
            return _mapper.Map<ToDoListDto>(toDoListDbDto);
        }
        /// <summary>
        /// Update ToDoListId.
        /// </summary>
        /// <param name="updateToDoListDto"></param>
        /// <returns> Updated ToDoListId. </returns>
        public async Task<ToDoListDto> UpdateToDoList(UpdateToDoListDto updateToDoListDto)
        {
            ToDoListDbModel toDoListDbModel = await _toDoDbContext.ToDoLists
                .FirstOrDefaultAsync(p => p.ToDoListId == updateToDoListDto.ToDoListId);
            if (toDoListDbModel == null)
                return null;
            toDoListDbModel.Description = updateToDoListDto.Description;
            toDoListDbModel.UpdationDate = DateTime.UtcNow;
            await _toDoDbContext.SaveChangesAsync();
            return _mapper.Map<ToDoListDto>(toDoListDbModel);
        }
        /// <summary>
        /// Delete ToDoList record based on ToDoListId passed.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns> 1 if success else -1. </returns> 
        public async Task<int> DeleteToDoList(long id, long userId)
        {
            ToDoListDbModel toDoListDbDto = await _toDoDbContext.ToDoLists
                .FirstOrDefaultAsync(p => p.ToDoListId == id && p.CreatedBy == userId);
            if (toDoListDbDto == null)
                return 0;

            _toDoDbContext.ToDoLists.Remove(toDoListDbDto);
            return await _toDoDbContext.SaveChangesAsync();
        }
    }
}
