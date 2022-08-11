using System.ComponentModel.DataAnnotations;

namespace roomService.DTO
{
    public class RoomDTO
    {
        public int Room_Number { get; set; }
        public int Status { get; set; }
        public int Fee { get; set; }

        [StringLength(45)]
        public string Room_Type { get; set; }

        [StringLength(45)]
        public string Capacity { get; set; }

        [StringLength(45)]
        public string Area { get; set; }

        [StringLength(45)]
        public string Ext1 { get; set; }

        [StringLength(45)]
        public string Ext2 { get; set; }

        public int HostelId { get; set; }
    }
}
