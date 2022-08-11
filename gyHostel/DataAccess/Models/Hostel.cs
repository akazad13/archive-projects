using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Hostel
    {
        [Key]
        public int Hostel_Id { get; set; }

        [StringLength(128)]
        public string Hostel_Name { get; set; }

        [StringLength(128)]
        public string Hostel_Address { get; set; }

        [StringLength(16)]
        public string Corporate_Person { get; set; }

        [StringLength(255)]
        public string Intro { get; set; }

        [StringLength(128)]
        public string Business_License_Img { get; set; }
        public DateTime Registration_Date { get; set; }

        [StringLength(45)]
        public string Status { get; set; }

        [StringLength(128)]
        public string Ext1 { get; set; }

        [StringLength(128)]
        public string Ext2 { get; set; }
    }
}
