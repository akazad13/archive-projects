﻿using System.ComponentModel.DataAnnotations;

namespace userService.DTO
{
    public class UserDTO
    {
        [StringLength(20)]
        public string Password { get; set; }

        [StringLength(45)]
        public string Account { get; set; }

        [StringLength(45)]
        public string Name { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(45)]
        public string Role { get; set; }

        [StringLength(15)]
        public string Serial_Number { get; set; }
        public byte Gender { get; set; }
        public byte Status { get; set; }

        [StringLength(45)]
        public string Ext1 { get; set; }

        [StringLength(45)]
        public string Ext2 { get; set; }
        public int HostelId { get; set; }
    }
}