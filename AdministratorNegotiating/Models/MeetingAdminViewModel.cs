using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdministratorNegotiating.Models
{
    public class MeetingAdminViewModel
    {
        public int Id { get; set; }

        [DisplayName("Дата бронирования")]
        public DateTime DayOfBooking { get; set; }

        [Required(ErrorMessage = "Требуется название встречи")]
        [MaxLength(50, ErrorMessage = "Длинна строки должна быть не более 50"), MinLength(3)]
        [DisplayName("Название встречи")]
        public string NameOfMeeting { get; set; }

        [Required(ErrorMessage = "Требуется указать начало встречи")]
        [DisplayName("Начало")]
        public DateTime BeginTime { get; set; }

        [Required(ErrorMessage = "Требуется указать конец встречи")]
        [DisplayName("Конец")]
        public DateTime EndTime { get; set; }

        [DisplayName("Переговорная")]
        public int MeetingRoomId { get; set; }

        //[ForeignKey("User")]
        [DisplayName("Создатель")]
        public string UserName { get; set; }

        [Required]
        public StatusTypes Status { get; set; }

        public enum StatusTypes
        {
            Confirmed,
            Rejected,
            Ended,
            Waiting
        }
    }
}