using System.ComponentModel.DataAnnotations;

namespace bookingService.DTO
{
    public class BookingDTO
    {
        [StringLength(45)]
        public string Booking_Time { get; set; }

        [StringLength(45)]
        public string Booking_Fee { get; set; }

        [StringLength(45)]
        public string Room_Type { get; set; }

        [StringLength(45)]
        public string Ext1 { get; set; }

        [StringLength(45)]
        public string Ext2 { get; set; }
        public int UserId { get; set; }
    }
}
