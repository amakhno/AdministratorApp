using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdministratorNegotiating.Models
{
    public class MeetingAddViewModel
    {
        [Required(ErrorMessage = "Требуется название встречи")]
        [MaxLength(50, ErrorMessage = "Длинна строки должна быть не более 50"), MinLength(3)]
        [DisplayName("Название встречи")]
        public string NameOfMeeting { get; set; }

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

        //[ForeignKey("User")]
        [DisplayName("Создатель")]
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}