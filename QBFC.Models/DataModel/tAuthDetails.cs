﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QBFC.Models.DataModel
{
    [Table("tAuthDetails")]
    public class tAuthDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int AccountID { get; set; }
        public string QBID { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public string QBEnv { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDT { get; set; }
        public DateTime ConsumedDT { get; set; }
    }
}
