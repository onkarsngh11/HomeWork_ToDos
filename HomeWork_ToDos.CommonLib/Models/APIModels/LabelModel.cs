using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HomeWork_ToDos.CommonLib.Models.APIModels
{
    public class LabelModel
    {
        public long LabelId { get; set; }
        public string Description { get; set; }
        public long CreatedBy { get; set; }
    }
    [SwaggerSchemaFilter(typeof(CreateLabelModel))]
    public class CreateLabelModel
    {
        public string Description { get; set; }
        [JsonIgnore]
        public long CreatedBy { get; set; }
    }
    [SwaggerSchemaFilter(typeof(DeleteLabelModel))]
    public class DeleteLabelModel
    {
        [Required]
        public long LabelId { get; set; }
        [JsonIgnore]
        public long CreatedBy { get; set; }
    }
    public class AssignLabelModel
    {
        public long[] LabelId;
        [JsonIgnore]
        public long CreatedBy { get; set; }
    }
    [SwaggerSchemaFilter(typeof(AssignLabelToListModel))]
    public class AssignLabelToListModel : AssignLabelModel
    {
        [Required]
        public long ToDoListId { get; set; }
    }
    [SwaggerSchemaFilter(typeof(AssignLabelToItemModel))]
    public class AssignLabelToItemModel : AssignLabelModel
    {
        [Required]
        public long ToDoItemId { get; set; }
    }
}
