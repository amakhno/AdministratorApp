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

        [Required(ErrorMessage = "Требуется название встречи")]
        [MaxLength(50, ErrorMessage = "Длинна строки должна быть не более 50"), MinLength(3)]
        [DisplayName("Название встречи")]
        public string NameOfMeeting { get;  set; }

        [Required(ErrorMessage = "Требуется указать качало встречи")]
        [DisplayName("Начало")]
        public DateTime BeginTime
        {
            get
            {
                return beginTime;
            }
            set
            {
                if (DayOfBooking != DateTime.MinValue)
                {
                    if (value < DayOfBooking)
                    {
                        throw new Exception("Дата бронирования больше чем дата начала встречи");
                    }
                    beginTime = value;
                    return;
                }
                if (value < DateTime.Now)
                {
                    throw new Exception("Нельзя выбирать дату в прошлом");
                }
                beginTime = value;
            }
        }

        private DateTime beginTime;
        [Required]
        [DisplayName("Конец")]
        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                if (DayOfBooking != DateTime.MinValue)
                {
                    if (value < DayOfBooking)
                    {
                        throw new Exception("Дата бронирования больше чем дата конца встречи");
                    }
                    endTime = value;
                    return;
                }
                if (value < DateTime.Now)
                {
                    throw new Exception("Нельзя выбирать дату в прошлом");
                }
                endTime = value;
            }
        }

        private DateTime endTime;

        [DisplayName("Переговорная")]
        public int MeetingRoomId { get; set; }

        // Ссылка на покупателя
        [DisplayName("Переговорная")]
        public MeetingRoom MeetingRoom { get; set; }

        // Ссылка на покупателя
        public virtual ApplicationUser User { get; set; }

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