using System.Text.Json.Serialization;

namespace HomeWork_ToDos.CommonLib.Dtos
{
    public class LabelDto
    {
        public long LabelId { get; set; }
        public string Description { get; set; }
        public long CreatedBy { get; set; }
    }

    public class CreateLabelDto
    {
        public string Description { get; set; }
        public long CreatedBy { get; set; }

    }
    public class DeleteLabelDto
    {
        public long LabelId { get; set; }
        public long CreatedBy { get; set; }
    }
    public class AssignLabelDto
    {
        public long[] LabelId;
        public long CreatedBy { get; set; }
    }
    public class AssignLabelToListDto : AssignLabelDto
    {
        public long ToDoListId { get; set; }
    }
    public class AssignLabelToItemDto : AssignLabelDto
    {
        public long ToDoItemId { get; set; }
    }
    public class MapLabelToListDto : LabelDto
    {
        [JsonIgnore]
        public new string Description { get; set; }
    }
    public class MapLabelToItemDto : LabelDto
    {
        [JsonIgnore]
        public new string Description { get; set; }

    }
}
