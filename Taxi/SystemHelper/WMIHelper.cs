using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.SystemHelper
{
    public static class WMIHelper
    {
        /// <summary>
        /// WMI 查询帮助类(String)
        /// </summary>
        /// <param name="selectQuery">String类型查询语句传入</param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> WMIQueryHelper(string selectQuery)
        {
            return WMIQueryHelper(new SelectQuery(selectQuery));
        }

        /// <summary>
        /// WMI 查询帮助类(SelectQuery)
        /// </summary>
        /// <param name="sq">SelectQuery类型查询传入</param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> WMIQueryHelper(SelectQuery sq)
        {
            try
            {
                var l = new List<Dictionary<string, string>>();
                var mos = new ManagementObjectSearcher(sq).Get();
                foreach (ManagementObject bus in mos)
                {
                    var d = new Dictionary<string, string>();
                    foreach (var bItem in bus.Properties)
                    {
                        d.Add(bItem.Name, bItem.Value != null ? bItem.Value.ToString() : null);
                    }
                    l.Add(d);
                    d = new Dictionary<string, string>();
                }
                return l;
            }
            catch
            {
                return null;
            }
        }
    }
}
