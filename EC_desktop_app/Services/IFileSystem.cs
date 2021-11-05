using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_desktop_app.Services
{
    public interface IFileSystem
    {
        string ReadAllText(string path, Encoding encoding);
    }
}
