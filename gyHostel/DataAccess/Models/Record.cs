using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Record
    {
        [Key]
        public int Record_Id { get; set; }

        public DateTime Record_Time { get; set; }

        [StringLength(45)]
        public string Record_Fee { get; set; }

        [StringLength(45)]
        public string Record_Type { get; set; }

        [StringLength(45)]
        public string Ext1 { get; set; }

        [StringLength(45)]
        public string Ext2 { get; set; }

        public Hostel Hostel { get; set; }
        public int HostelId { get; set; }
    }
}
