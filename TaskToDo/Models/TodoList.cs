using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskToDo.Models
{
    public class TodoList
    {
        public int ID { get; set; }
        [Required]
        public string ID_USUARIO { get; set; }
        [Required]
        public string TASK { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? CREATED { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? LAST_UPD { get; set; }
        public bool COMPLETED { get; set; }
    }
}
