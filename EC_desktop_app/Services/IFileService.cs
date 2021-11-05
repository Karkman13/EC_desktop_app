using EC_desktop_app.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_desktop_app.Services
{
    interface IFileService
    {
        List<UserData> Open(string filename);
        void Save(string filename, List<UserData> userDatas);
    }
}
