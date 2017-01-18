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
        /// WMI 远程查询帮助类(String)
        /// </summary>
        /// <param name="ipAddress">远程IP</param>
        /// <param name="remoteUsername">远程主机用户名</param>
        /// <param name="remotePassword">远程主机密码</param>
        /// <param name="selectQuery"></param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> WMIRemoteQueryHelper(string ipAddress, string remoteUsername, string remotePassword, string selectQuery)
        {
            return WMIRemoteQueryHelper(ipAddress, remoteUsername, remotePassword, new SelectQuery(selectQuery));
        }


        /// <summary>
        /// WMI 远程查询帮助类(SelectQuery)
        /// </summary>
        /// <param name="ipAddress">远程IP</param>
        /// <param name="remoteUsername">远程主机用户名</param>
        /// <param name="remotePassword">远程主机密码</param>
        /// <param name="selectQuery"></param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> WMIRemoteQueryHelper(string ipAddress, string remoteUsername, string remotePassword, SelectQuery selectQuery)
        {
            ConnectionOptions op = new ConnectionOptions();
            op.Username = remoteUsername;
            op.Password = remotePassword;
            var scope = new ManagementScope($"\\\\{ipAddress}\\root\\cimv2", op);
            return WMIRemoteQueryHelper(scope, selectQuery);
        }

        /// <summary>
        /// WMI 远程查询帮助类
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="selectQuery"></param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> WMIRemoteQueryHelper(ManagementScope ms, SelectQuery selectQuery)
        {
            return WMIQueryHelper(ms, selectQuery);
        }


        /// <summary>
        /// WMI 查询帮助类(String)
        /// </summary>
        /// <param name="selectQuery">String类型查询语句传入</param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> WMIQueryHelper(string selectQuery)
        {
            return WMIQueryHelper(new ManagementScope("\\root\\cimv2"), new SelectQuery(selectQuery));
        }

        /// <summary>
        /// WMI 查询帮助类(SelectQuery)
        /// </summary>
        /// <param name="sq">SelectQuery类型查询传入</param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> WMIQueryHelper(ManagementScope scope, SelectQuery sq)
        {
            try
            {
                var l = new List<Dictionary<string, string>>();
                var mos = new ManagementObjectSearcher(scope, sq).Get();
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
