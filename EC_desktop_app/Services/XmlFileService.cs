using EC_desktop_app.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EC_desktop_app.Services
{
    class XmlFileService : IFileService
    {
        /// <summary>
        /// This method deserializing data from file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public List<UserData> Open(string filename)
        {
            var userDatas = new List<UserData>();

            var xmlformatter = new XmlSerializer(typeof(List<UserData>));
            using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                userDatas = xmlformatter.Deserialize(fs) as List<UserData>;
            }

            return userDatas;
        }

        /// <summary>
        /// This method serializing data in XML file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="userDatas"></param>
        public void Save(string filename, List<UserData> userDatas)
        {
            var xmlFormater = new XmlSerializer(typeof(List<UserData>));
            var attribute = new XmlAttributes();


            using(var fs = new FileStream(filename, FileMode.Create))
            {
                xmlFormater.Serialize(fs, userDatas);
            }
        }
    }
}
