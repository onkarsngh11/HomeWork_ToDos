using HomeWork_ToDos.CommonLib.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeWork_ToDos.CommonLib.Contracts.DbOps
{
    public interface ILabelDbOps
    {
        /// <summary>
        /// Delete label.
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> DeleteLabel(long labelId, long userId);

        /// <summary>
        /// Get label by id.
        /// </summary>
        /// <param name="labelId">label id.</param>
        /// <param name="userId">user id.</param>
        Task<LabelDto> GetLabelById(long labelId, long userId);

        /// <summary>
        /// Get all labels.
        /// </summary>
        /// <param name="userId">User id.</param>
        Task<List<LabelDto>> GetAllLabels(long userId);

        /// <summary>
        /// Add label.
        /// </summary>
        /// <param name="createLabelDto">Label details.</param>
        Task<LabelDto> AddLabel(CreateLabelDto createLabelDto);

        /// <summary>
        /// Assign labels to ToDoList.
        /// </summary>
        /// <param name="assignLabelToListDto"> LabelIds corresponding to ToDoListId</param>
        Task<bool> AssignLabelToList(AssignLabelToListDto assignLabelToListDto);

        /// <summary>
        /// Assign Labels to ToDoItem.
        /// </summary>
        /// <param name="assignLabelToItemDto">LabelIds corresponding to ToDoItemId</param>
        Task<bool> AssignLabelToItem(AssignLabelToItemDto assignLabelToItemDto);
    }
}
