using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeWork_ToDos.CommonLib.Models.DbModels
{
    public class MapLabelsToItemDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ItemMappingId { get; set; }

        public long LabelId { get; set; }
        [ForeignKey("LabelId")]
        public virtual LabelDbModel Labels { get; set; }

        public long ToDoItemId { get; set; }
        [ForeignKey("ToDoItemId")]
        public virtual ToDoItemDbModel ToDoItems { get; set; }

        public long CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual UserDbModel Users { get; set; }
    }
}