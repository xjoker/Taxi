using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Taxi.SystemHelper;
using Taxi.StringHelper;

namespace Taxi.Network
{
    class NetworkTools
    {
        /// <summary>
        /// Ping测试
        /// </summary>
        /// <param name="host">主机或IP</param>
        /// <returns>返回延时，如果不通则返回NULL</returns>
        public static string Ping(string host)
        {
            var p = WMIHelper.WMIQueryHelper(new ManagementScope("\\root\\cimv2"), new SelectQuery("Win32_PingStatus", $"Address='{host}'"));
            return p[0]["ResponseTime"];
        }

        /// <summary>
        /// 域名解析为IP地址
        /// </summary>
        /// <param name="host">主机或IP</param>
        /// <returns>返回IP，如果未查询到则返回NULL</returns>
        public static string DomainToIP(string host)
        {
            var p = WMIHelper.WMIQueryHelper(new ManagementScope("\\root\\cimv2"), new SelectQuery("Win32_PingStatus", $"Address='{host}'"));
            return p[0]["ProtocolAddress"].IsNullOrWhiteSpace()?null: p[0]["ProtocolAddress"];
        }
    }
}
