using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HomeWork_ToDos.CommonLib.Models.APIModels
{
    public class ToDoItemModel
    {
        public long ToDoItemId { get; set; }
        public string Notes { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdationDate { get; set; }
        public long ToDoListId { get; set; }
    }
    [SwaggerSchemaFilter(typeof(CreateToDoItemModel))]
    public class CreateToDoItemModel
    {
        public long ToDoListId { get; set; }
        public string Notes { get; set; }
        [JsonIgnore]
        public long CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
    [SwaggerSchemaFilter(typeof(UpdateToDoItemModel))]
    public class UpdateToDoItemModel
    {
        public string Notes { get; set; }
        [Required]
        public long ToDoItemId { get; set; }
        [JsonIgnore]
        public DateTime UpdationDate { get; set; } = DateTime.UtcNow;
    }
    [SwaggerSchemaFilter(typeof(DeleteToDoItemModel))]
    public class DeleteToDoItemModel
    {
        [Required]
        public long ToDoItemId { get; set; }
        [JsonIgnore]
        public long CreatedBy { get; set; }
    }
}