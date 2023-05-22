using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WebNhaHang.Models.EF
{
    public class ReservationViewModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string Name { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        [StringLength(250)]
        public string Room { get; set; }

        public string NumberOfPeople { get; set; }
        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public int Status { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
        public DateTime GetDateTime()
        {
            return DateTime.Parse(String.Format("{0} {1}", Date, Time));
        }

        public string Action
        {
            get { return (id != 0) ? "Update" : "Create"; }
        }

    }
}