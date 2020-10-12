using System;
using System.Collections.Generic;

namespace HomeWork_ToDos.CommonLib.Dtos
{
    public class ToDoItemDto
    {
        public long ToDoItemId { get; set; }
        public string Notes { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdationDate { get; set; }
        public long ToDoListId { get; set; }
        public List<MapLabelToItemDto> Labels { get; set; }
    }
    public class CreateToDoItemDto
    {
        public long ToDoListId { get; set; }
        public string Notes { get; set; }
        public long CreatedBy { get; set; }
    }
    public class UpdateToDoItemDto
    {
        public string Notes { get; set; }
        public long ToDoItemId { get; set; }
    }
    public class DeleteToDoItemDto
    {
        public long ToDoItemId { get; set; }
        public long CreatedBy { get; set; }
    }
}