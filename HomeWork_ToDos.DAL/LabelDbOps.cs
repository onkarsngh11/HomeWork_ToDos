using AutoMapper;
using HomeWork_ToDos.CommonLib.Contracts.DbOps;
using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Models.DbModels;
using HomeWork_ToDos.DAL.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork_ToDos.DAL
{
    public class LabelDbOps : ILabelDbOps
    {
        private readonly ToDoDbContext _toDoDbContext;
        private readonly IMapper _mapper;
        public LabelDbOps(ToDoDbContext toDoDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _toDoDbContext = toDoDbContext;
        }
        /// <summary>
        /// Gets all the Label items.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>All Label items.</returns>
        public async Task<List<LabelDto>> GetAllLabels(long userId)
        {
            List<LabelDbModel> Labels = await _toDoDbContext.Labels.Where(p => p.CreatedBy == userId).ToListAsync();
            return _mapper.Map<List<LabelDto>>(Labels);
        }
        /// <summary>
        /// Gets Label item by Id from database.
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="userId"></param>
        /// <returns>Label item by Id for logged in user</returns>
        public async Task<LabelDto> GetLabelById(long labelId, long userId)
        {
            LabelDbModel LabelDbDto = await _toDoDbContext.Labels
                .FirstOrDefaultAsync(p => p.LabelId == labelId && p.CreatedBy == userId);
            return _mapper.Map<LabelDto>(LabelDbDto);
        }
        /// <summary>
        /// adds label in Label table
        /// </summary>
        /// <param name="createLabelDto"></param>
        /// <returns> added label record Dto. </returns>
        public async Task<LabelDto> AddLabel(CreateLabelDto createLabelDto)
        {
            LabelDbModel labelDbDto = _mapper.Map<LabelDbModel>(createLabelDto);
            labelDbDto.CreatedBy = createLabelDto.CreatedBy;
            _toDoDbContext.Labels.Add(labelDbDto);
            await _toDoDbContext.SaveChangesAsync();
            return _mapper.Map<LabelDto>(labelDbDto);
        }
        /// <summary>
        /// Delete Label record based on LabelId passed.
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="userId"></param>
        /// <returns> 1 if success else -1. </returns>
        public async Task<int> DeleteLabel(long labelId, long userId)
        {
            LabelDbModel labelDbDto = await _toDoDbContext.Labels
                .FirstOrDefaultAsync(p => p.LabelId == labelId && p.CreatedBy == userId);
            if (labelDbDto == null)
                return 0;

            _toDoDbContext.Labels.Remove(labelDbDto);
            return await _toDoDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Create mapping of LabelId/s to ToDoListId 
        /// </summary>
        /// <param name="assignLabelToListDto"></param>
        /// <returns> success/failure result </returns>
        public async Task<bool> AssignLabelToList(AssignLabelToListDto assignLabelToListDto)
        {
            //Remove existing mapping first 
            MapLabelsToListDbModel existingListMapping = _toDoDbContext.MapLabelsToLists
                .Where(mapping => mapping.ToDoListId == assignLabelToListDto.ToDoListId
                        && mapping.CreatedBy == assignLabelToListDto.CreatedBy).FirstOrDefault();
            ToDoListDbModel existingToDoListDbModel = _toDoDbContext.ToDoLists
                .Where(list => list.ToDoListId == assignLabelToListDto.ToDoListId).FirstOrDefault();

            if (existingListMapping != null && existingToDoListDbModel != null)                      // remove existing mapping first based on UserId and ToDoListId combination.
            {
                _toDoDbContext.MapLabelsToLists.Remove(existingListMapping);
                await _toDoDbContext.SaveChangesAsync();
            }
            int saveResult = 0;

            if (assignLabelToListDto.LabelId.Count() == 1)      // One to one mapping of LabelId to ToDoListId
            {
                MapLabelsToListDbModel mapLabelsToListDbDto = new MapLabelsToListDbModel
                {
                    CreatedBy = assignLabelToListDto.CreatedBy,
                    LabelId = assignLabelToListDto.LabelId.FirstOrDefault(),
                    ToDoListId = assignLabelToListDto.ToDoListId
                };
                _toDoDbContext.MapLabelsToLists.Add(mapLabelsToListDbDto);
                saveResult = await _toDoDbContext.SaveChangesAsync();
            }
            else if (assignLabelToListDto.LabelId.Count() > 1)      // Many to one mapping of LabelIds to ToDoListId
            {
                for (int labelId = 0; labelId < assignLabelToListDto.LabelId.Length; labelId++)
                {
                    MapLabelsToListDbModel mapLabelsToListDbDto = new MapLabelsToListDbModel
                    {
                        CreatedBy = assignLabelToListDto.CreatedBy,
                        LabelId = assignLabelToListDto.LabelId[labelId],
                        ToDoListId = assignLabelToListDto.ToDoListId
                    };
                    _toDoDbContext.MapLabelsToLists.Add(mapLabelsToListDbDto);
                }
                saveResult = await _toDoDbContext.SaveChangesAsync();
            }
            if (saveResult > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Create mapping of LabelId/s to ToDoItemId 
        /// </summary>
        /// <param name="assignLabelToItemDto"></param>
        /// <returns> success/failure result </returns>
        public async Task<bool> AssignLabelToItem(AssignLabelToItemDto assignLabelToItemDto)
        {
            //Remove existing mapping first 
            MapLabelsToItemDbModel existingItemMapping = _toDoDbContext.MapLabelsToItems
                .Where(mapping => mapping.ToDoItemId == assignLabelToItemDto.ToDoItemId
                        && mapping.CreatedBy == assignLabelToItemDto.CreatedBy).FirstOrDefault();
            ToDoItemDbModel existingToDoItemDbModel = _toDoDbContext.ToDoItems
                .Where(item => item.ToDoItemId == assignLabelToItemDto.ToDoItemId).FirstOrDefault();

            if (existingItemMapping != null && existingToDoItemDbModel != null)                // remove existing mapping first based on UserId and ToDoItemId combination.
            {
                _toDoDbContext.MapLabelsToItems.Remove(existingItemMapping);
                await _toDoDbContext.SaveChangesAsync();
            }
            int saveResult = 0;
            if (assignLabelToItemDto.LabelId.Count() == 1)      // One to one mapping of LabelId to ToDoItemId
            {
                MapLabelsToItemDbModel mapLabelsToItemDbDto = new MapLabelsToItemDbModel
                {
                    CreatedBy = assignLabelToItemDto.CreatedBy,
                    LabelId = assignLabelToItemDto.LabelId.FirstOrDefault(),
                    ToDoItemId = assignLabelToItemDto.ToDoItemId
                };
                _toDoDbContext.MapLabelsToItems.Add(mapLabelsToItemDbDto);
                saveResult = await _toDoDbContext.SaveChangesAsync();
            }
            else if (assignLabelToItemDto.LabelId.Count() > 1)      // Many to one mapping of LabelIds to ToDoItemId
            {
                for (int labelId = 0; labelId < assignLabelToItemDto.LabelId.Length; labelId++)
                {
                    MapLabelsToItemDbModel mapLabelsToItemDbDto = new MapLabelsToItemDbModel
                    {
                        CreatedBy = assignLabelToItemDto.CreatedBy,
                        LabelId = assignLabelToItemDto.LabelId[labelId],
                        ToDoItemId = assignLabelToItemDto.ToDoItemId
                    };
                    _toDoDbContext.MapLabelsToItems.Add(mapLabelsToItemDbDto);
                }
                saveResult = await _toDoDbContext.SaveChangesAsync();
            }
            if (saveResult > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}