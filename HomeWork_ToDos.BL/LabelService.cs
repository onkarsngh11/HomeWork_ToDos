using HomeWork_ToDos.CommonLib.Contracts.BL;
using HomeWork_ToDos.CommonLib.Contracts.DbOps;
using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Models.APIModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork_ToDos.BL
{
    /// <summary>
    /// Implemenation of ILabelService contract.
    /// </summary>
    public class LabelService : ILabelContract
    {
        private readonly ILabelDbOps _labelDbOps;
        public LabelService(ILabelDbOps labelDbOps)
        {
            _labelDbOps = labelDbOps;
        }
        /// <summary>
        /// Get Label record By Id 
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="userId"></param>
        /// <returns>Label record based on LabelId</returns>
        public async Task<LabelDto> GetLabelById(long labelId, long userId)
        {
            return await _labelDbOps.GetLabelById(labelId, userId);
        }

        /// <summary>
        /// Get all labels.
        /// </summary>
        /// <param name="paginationParams"></param>
        /// <param name="userId"></param>
        /// <returns>Returns all labels for user.</returns>
        public async Task<PagedList<LabelDto>> GetAllLabels(PaginationParameters paginationParams, long userId)
        {
            List<LabelDto> Labels = await _labelDbOps.GetAllLabels(userId);
            if (!string.IsNullOrWhiteSpace(paginationParams.SearchText))
            {
                Labels = Labels.Where(p => p.Description.Contains(paginationParams.SearchText)).ToList();
            }
            return PagedList<LabelDto>.ToPagedList(Labels, paginationParams.PageNumber, paginationParams.PageSize);
        }
        /// <summary>
        /// adds label record to label table.
        /// </summary>
        /// <param name="createLabelDto"></param>
        /// <returns> Added Label record. </returns>
        public async Task<LabelDto> AddLabel(CreateLabelDto createLabelDto)
        {
            return await _labelDbOps.AddLabel(createLabelDto);
        }

        /// <summary>
        /// Delete label record.
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> DeleteLabel(long labelId, long userId)
        {
            return await _labelDbOps.DeleteLabel(labelId, userId);
        }

        /// <summary>
        /// assign labelids to specified ToDoList record.
        /// </summary>
        /// <param name="assignLabelToListDto"></param>
        /// <returns> Boolean result of Assignment, i.e. true/false. </returns>
        public async Task<bool> AssignLabelToList(AssignLabelToListDto assignLabelToListDto)
        {
            return await _labelDbOps.AssignLabelToList(assignLabelToListDto);
        }

        /// <summary>
        /// Assign labelIds to specified ToDoItemId record.
        /// </summary>
        /// <param name="assignLabelToItemDto"></param>
        /// <returns> Boolean result of Assignment, i.e. true/false. </returns>
        public async Task<bool> AssignLabelToItem(AssignLabelToItemDto assignLabelToItemDto)
        {
            return await _labelDbOps.AssignLabelToItem(assignLabelToItemDto);
        }
    }
}
