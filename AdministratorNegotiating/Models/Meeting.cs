using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdministratorNegotiating.Models
{
    public class Meeting
    {        
        public int Id { get;  set; }

        [DisplayName("Дата бронирования")]
        public DateTime DayOfBooking { get;  set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Длинна строки должна быть не более 50"), MinLength(3)]
        []
        public string NameOfMeeting { get;  set; }

        [Required]
        public DateTime BeginTime { get;  set; }

        [Required]
        public DateTime EndTime { get;  set; }

        //[ForeignKey("MeetingRoom")]
        public int MeetingRoomId { get; set; }        

        // Ссылка на покупателя
        public MeetingRoom MeetingRoom { get; set; }

        // Ссылка на покупателя
        public ApplicationUser User { get; set; }

        //[ForeignKey("User")]
        public string UserName { get; set; }

        [Required]
        public StatusTypes Status { get; set; }

        public enum StatusTypes
        { 
            Confirmed, 
            Rejected,
            InProcess,
            Ended,
            Waiting
        }
    }
}