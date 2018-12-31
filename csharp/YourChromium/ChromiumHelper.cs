using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace YourChromium
{
    class ChromiumHelper
    {
        string baseURL = "https://storage.googleapis.com.cnpmjs.org/chromium-browser-snapshots/";

        // partial implemenation of https://github.com/beaufortfrancois/download-chromium/blob/master/utils.py

        public class Platforms {
            public string Windows = "Win";
            public string WindowsX64 = "Win_x64";
            public string MacOS = "Mac";
            public string Linux = "Linux";
            public string LinuxX64 = "Linux_x64";
            public string ForChromiumOS = "Linux_ChromiumOS_Full";
            public string AndroidAny = "Android";
            public class ZipNames {
                public string Windows = "chrome-win.zip";
                public string MacOS = "chrome-mac.zip";
                public string Linux = "chrome-linux.zip";
                public string ChromiumOS = "chrome-chromeos.zip";
                public string Android = "chrome-android.zip";
            }
        }

        public string getZipUrl(string platfromStr)
        {
            string rev = getLastRevisionForPlatform(platfromStr);
            return baseURL + platfromStr + "/" + rev + "/" + getPlfZ(platfromStr);
        }
        public string getPlfZ(string plf2)
        {
            ChromiumHelper.Platforms.ZipNames zn = new ChromiumHelper.Platforms.ZipNames();
            ChromiumHelper.Platforms pfn = new ChromiumHelper.Platforms();
            if (plf2 == pfn.Windows || plf2 == pfn.WindowsX64)
            {
                return zn.Windows;
            }
            else if (plf2 == pfn.MacOS)
            {
                return zn.MacOS;
            }
            else if (plf2 == pfn.Linux || plf2 == pfn.LinuxX64)
            {
                return zn.Linux;
            }
            else if (plf2 == pfn.ForChromiumOS)
            {
                return zn.ChromiumOS;
            }
            else if (plf2 == pfn.AndroidAny)
            {
                return zn.Android;
            }
            return null;
        }

        public string getLastRevisionForPlatform(string platfromStr)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL+platfromStr+"/LAST_CHANGE");//这里的url指要获取的数据网址
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
        }
}
