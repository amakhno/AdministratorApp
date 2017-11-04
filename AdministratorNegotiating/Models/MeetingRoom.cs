using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdministratorNegotiating.Models
{
    public class MeetingRoom
    {
        public int Id { get; set; }

        private int countOfChairs;

        [Required]
        [MaxLength(50, ErrorMessage = "Название встречи должно быть короче 50 символов"), MinLength(3, ErrorMessage = "Слишком короткое название")]
        [DisplayName("Название переговорной")]
        public string Name { get; set; }

        public List<Meeting> Meetings { get; set; }

        [Required]
        [DisplayName("Колчество сидячих мест")]
        public int CountOfChairs
        {
            get
            {
                return countOfChairs;
            }
            set
            {
                if ((value < 0) || (value > 100000))
                {
                    throw new Exception("Некорректное количество сидячих мест");
                }
                countOfChairs = value;
            }
        }

        [DisplayName("Наличие доски")]
        public bool IsBoard { get; set; }

        [DisplayName("Наличие проектора")]
        public bool IsProjector { get; set; }
    }
}