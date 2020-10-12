using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HomeWork_ToDos.CommonLib.Models.APIModels
{
    public class ToDoListModel
    {
        public long ToDoListId { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdationDate { get; set; }
        public LabelModel LabelModel { get; set; }
        [JsonIgnore]
        public long CreatedBy { get; set; }
    }
    [SwaggerSchemaFilter(typeof(CreateToDoListModel))]
    public class CreateToDoListModel
    {
        public string Description { get; set; }
        [JsonIgnore]
        public long CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    }
    [SwaggerSchemaFilter(typeof(UpdateToDoListModel))]
    public class UpdateToDoListModel
    {
        public string Description { get; set; }
        [Required]
        public long ToDoListId { get; set; }
        [JsonIgnore]
        public DateTime UpdationDate { get; set; } = DateTime.UtcNow;
    }
    [SwaggerSchemaFilter(typeof(DeleteToDoListModel))]
    public class DeleteToDoListModel
    {
        [Required]
        public long ToDoListId { get; set; }
        [JsonIgnore]
        public long CreatedBy { get; set; }
    }
}
