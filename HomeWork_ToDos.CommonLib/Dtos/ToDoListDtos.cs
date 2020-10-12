using System;
using System.Collections.Generic;

namespace HomeWork_ToDos.CommonLib.Dtos
{
    public class ToDoListDto
    {
        public long ToDoListId { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdationDate { get; set; }
        public long CreatedBy { get; set; }
        public List<MapLabelToListDto> Labels { get; set; }
        public List<ToDoItemDto> ToDoItems { get; set; }
    }
    public class CreateToDoListDto
    {
        public string Description { get; set; }
        public long CreatedBy { get; set; }
    }
    public class UpdateToDoListDto
    {
        public string Description { get; set; }
        public long ToDoListId { get; set; }
    }
    public class DeleteToDoListDto
    {
        public long ToDoListId { get; set; }
        public long CreatedBy { get; set; }
    }
}
