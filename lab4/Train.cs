using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class Train
    {
        [Key]
        public int id_train { get; set; }

        private string category, route, arrival_date, arrival_time, departure_date, departure_time;

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public string Route
        {
            get { return route; }
            set { route = value; }
        }

        public string Arrival_date
        {
            get { return arrival_date; }
            set { arrival_date = value; }
        }

        public string Arrival_time
        {
            get { return arrival_time; }
            set { arrival_time = value; }
        }

        public string Departure_date
        {
            get { return departure_date; }
            set { departure_date = value; }
        }


        public string Departure_time
        {
            get { return departure_time; }
            set { departure_time = value; }
        }
        public Train() { }

        public Train(string category, string route, string arrival_date, string arrival_time, string departure_date, string departure_time)
        {
            this.category = category;
            this.route = route;
            this.arrival_date = arrival_date;
            this.arrival_time = arrival_time;
            this.departure_date = departure_date;
            this.departure_time = departure_time;
        }
    }
}
