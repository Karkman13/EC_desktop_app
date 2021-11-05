using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EC_desktop_app.Model
{
    [Serializable]
    public class UserData
    {
        public DayOfWeek WeekDay { get; set; }
        public int MagicNumber { get; set; }
        public string UserText { get; set; }

        public UserData()
        {

        }

        //public UserData(string weekday, int magicnumber, string usertext)
        //{
        //    this.WeekDay = weekday;
        //    this.MagicNumber = magicnumber;
        //    this.UserText = usertext;
        //}
    }
}
