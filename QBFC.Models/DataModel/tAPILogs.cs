using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QBFC.Models.DataModel
{
    [Table("tAPILogs")]
    public class tAPILogs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Source {get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDT { get; set; }
    }
}
