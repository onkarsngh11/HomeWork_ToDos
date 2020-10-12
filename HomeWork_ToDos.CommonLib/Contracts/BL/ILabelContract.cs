using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Models.APIModels;
using System.Threading.Tasks;

namespace HomeWork_ToDos.CommonLib.Contracts.BL
{
    public interface ILabelContract
    {
        /// <summary>
        /// Get label by id.
        /// </summary>
        /// <param name="labelId">label id.</param>
        /// <param name="userId">user id.</param>
        Task<LabelDto> GetLabelById(long labelId, long userId);

        /// <summary>
        /// Get all labels
        /// </summary>
        /// <param name="paginationParams"></param>
        /// <param name="userId"></param>
        /// <returns></returns>        
        Task<PagedList<LabelDto>> GetAllLabels(PaginationParameters paginationParams, long userId);

        /// <summary>
        /// Add label.
        /// </summary>
        /// <param name="createLabelDto">Label details.</param>
        Task<LabelDto> AddLabel(CreateLabelDto createLabelDto);

        /// <summary>
        /// Delete label record
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> DeleteLabel(long labelId, long userId);

        /// <summary>
        /// Assign labels to list.
        /// </summary>
        /// <param name="assignLabelToListDto"></param>
        Task<bool> AssignLabelToList(AssignLabelToListDto assignLabelToListDto);

        /// <summary>
        /// Assign labels to item.
        /// </summary>
        /// <param name="assignLabelToItemDto"></param>
        Task<bool> AssignLabelToItem(AssignLabelToItemDto assignLabelToItemDto);
    }
}
