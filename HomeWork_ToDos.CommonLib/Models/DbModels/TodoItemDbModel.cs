using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeWork_ToDos.CommonLib.Models.DbModels
{
    public class ToDoItemDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ToDoItemId { get; set; }
        public string Notes { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdationDate { get; set; }

        public List<MapLabelsToItemDbModel> Labels { get; set; }

        public long CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual UserDbModel Users { get; set; }

        public long ToDoListId { get; set; }
        [ForeignKey("ToDoListId")]
        public virtual ToDoListDbModel ToDoLists { get; set; }
    }
}