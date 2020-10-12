using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeWork_ToDos.CommonLib.Models.DbModels
{
    public class MapLabelsToListDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ListMappingId { get; set; }

        public long LabelId { get; set; }
        [ForeignKey("LabelId")]
        public virtual LabelDbModel Labels { get; set; }

        public long ToDoListId { get; set; }
        [ForeignKey("ToDoListId")]
        public virtual ToDoListDbModel ToDoLists { get; set; }

        public long CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual UserDbModel Users { get; set; }
    }
}
