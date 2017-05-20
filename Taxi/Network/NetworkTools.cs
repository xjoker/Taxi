using System;
using System.Collections.Generic;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using Taxi.StringHelper;
using Taxi.SystemHelper;

namespace Taxi.Network
{
    public class PingResponseType
    {
        private static int send = 0;
        private static int received = 0;
        private static int lost = send - received;
        public long Minimum { get; set; }
        public long Maximum { get; set; }
        public long Average { get; set; }
        public int Sent { get; set; }
        public int Received { get; set; }
        public int Lost { get; }
    }

    public class PortListType
    {

        public List<int> TcpPorts { get; set; }
        public List<int> UdpPorts { get; set; }
    }

    public class NetworkTools
    {
        /// <summary>
        /// PING方法
        /// </summary>
        /// <param name="host">主机</param>
        /// <param name="timeout">超时设定 默认3000</param>
        /// <param name="bufferSize">包尺寸 默认32</param>
        /// <returns></returns>
        public static PingReply Ping(string host, int timeout = 3000, int bufferSize = 32)
        {
            using (Ping p = new Ping())
            {
                PingOptions options = new PingOptions { DontFragment = true };
                byte[] buffer = Encoding.ASCII.GetBytes(new string('a', bufferSize));
                PingReply reply = p.Send(host, timeout, buffer, options);
                return reply;
            }
        }

        /// <summary>
        /// PING 检测主机是否连通
        /// </summary>
        /// <param name="host">主机</param>
        /// <returns></returns>
        public static bool PingCheck(string host)
        {
            var temp = Ping(host);
            if (temp.Status == IPStatus.Success)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// PING 检测主机延时
        /// 如主机无法连通会返回NULL
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static long? PingDelay(string host)
        {
            var temp = Ping(host);
            if (temp.Status == IPStatus.Success)
            {
                return temp.RoundtripTime;
            }
            return null;
        }

        /// <summary>
        /// PING 检测，可定义次数和间隔
        /// </summary>
        /// <param name="host"></param>
        /// <param name="count"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static PingResponseType PingCheckDetailed(string host, int count = 4, int interval = 1000)
        {
            PingResponseType prt = new PingResponseType();
            long delaySum = 0;
            for (int i = 0; i < count; i++)
            {
                prt.Sent++;
                var temp = Ping(host);
                if (temp.Status == IPStatus.Success)
                {
                    prt.Received++;
                    if (prt.Minimum == 0)
                    {
                        prt.Minimum = temp.RoundtripTime;
                    }
                    if (temp.RoundtripTime > prt.Maximum) prt.Maximum = temp.RoundtripTime;
                    if (temp.RoundtripTime < prt.Minimum) prt.Minimum = temp.RoundtripTime;

                    delaySum = delaySum + temp.RoundtripTime;
                }
                Thread.Sleep(interval);
            }
            prt.Average = delaySum / 4;

            return prt;
        }

        /// <summary>
        /// 域名解析为IP地址
        /// </summary>
        /// <param name="host">主机或IP</param>
        /// <returns>返回IP，如果未查询到则返回NULL</returns>
        public static string DomainToIP(string host)
        {
            var p = WMIHelper.WMIQueryHelper(new ManagementScope("\\root\\cimv2"), new SelectQuery("Win32_PingStatus", $"Address='{host}'"));
            return p[0]["ProtocolAddress"].IsNullOrWhiteSpace() ? null : p[0]["ProtocolAddress"];
        }

        /// <summary>
        /// 获取本机所有网卡的IP地址
        /// </summary>
        /// <returns></returns>
        public static List<string> GetLocalIPs()
        {
            IPHostEntry ips = Dns.GetHostEntry(Dns.GetHostName());
            List<string> list = new List<string>();
            if (ips.AddressList.Length > 0)
            {
                foreach (IPAddress address in ips.AddressList)
                {
                    if (address.AddressFamily.ToString().Equals("InterNetwork"))
                    {
                        list.Add(address.ToString());
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取本机所有网卡的MAC地址
        /// </summary>
        /// <returns></returns>
        public static List<string> GetLocalMacs()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            List<string> list = new List<string>();
            foreach (var o in mc.GetInstances())
            {
                var mo = (ManagementObject)o;
                if (System.Convert.ToBoolean(mo["IPEnabled"]))
                {
                    list.Add(mo["MacAddress"].ToString());
                }
            }
            mc.Dispose();
            return list;
        }

        /// <summary>
        /// 获取所有在用状态的TCP/UDP端口
        /// </summary>
        /// <returns></returns>
        public static PortListType GetAllUsePort()
        {
            List<int> tcpPorts = new List<int>();
            List<int> udpPorts = new List<int>();

            //获取本地计算机的网络连接和通信统计数据的信息 
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            //返回本地计算机上的所有Tcp监听程序 
            IPEndPoint[] ipsTcp = ipGlobalProperties.GetActiveTcpListeners();

            //返回本地计算机上的所有UDP监听程序 
            IPEndPoint[] ipsUdp = ipGlobalProperties.GetActiveUdpListeners();

            foreach (IPEndPoint ep in ipsTcp) tcpPorts.Add(ep.Port);
            foreach (IPEndPoint ep in ipsUdp) udpPorts.Add(ep.Port);

            return new PortListType() { TcpPorts = tcpPorts, UdpPorts = udpPorts };
        }

        /// <summary>
        /// 检测TCP端口是否已被使用
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool CheckTCPPortIsUse(int port)
        {
            if (port >= 65535 && 0>port)
            {
                throw new ArgumentException("Port must in 1~65535");
            }
            var portUsed = GetAllUsePort().TcpPorts;
            return portUsed.Contains(port);
        }


        /// <summary>
        /// 检测UDP端口是否已被使用
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool CheckUDPPortIsUse(int port)
        {
            if (port >= 65535 && 0 > port)
            {
                throw new ArgumentException("Port must in 1~65535");
            }
            var portUsed = GetAllUsePort().UdpPorts;
            return portUsed.Contains(port);
        }
    }
}
