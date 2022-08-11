using System;
using System.ComponentModel.DataAnnotations;

namespace recordService.DTO
{
    public class RecordDTO
    {
        public DateTime Record_Time { get; set; }

        [StringLength(45)]
        public string Record_Fee { get; set; }

        [StringLength(45)]
        public string Record_Type { get; set; }

        [StringLength(45)]
        public string Ext1 { get; set; }

        [StringLength(45)]
        public string Ext2 { get; set; }

        public int HostelId { get; set; }
    }
}
