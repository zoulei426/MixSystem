using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mix.Windows.Core
{
    /// <summary>
    /// 系统信息
    /// </summary>
    public static class SystemInfo
    {
        /// <summary>
        /// 操作系统
        /// </summary>
        public static readonly OSPlatform OSPlatform = GetOSPlatform();

        /// <summary>
        /// 系统架构
        /// </summary>
        public static readonly Architecture OSArchitecture = RuntimeInformation.OSArchitecture;

        /// <summary>
        /// 系统名称
        /// </summary>
        public static readonly string OSDescription = RuntimeInformation.OSDescription;

        /// <summary>
        /// 版本
        /// </summary>
        public static readonly Version Version = Environment.Version;

        private static OSPlatform GetOSPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return OSPlatform.Linux;
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return OSPlatform.Windows;
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return OSPlatform.OSX;
            else throw new Exception("获取操作系统信息失败！");
        }

        /// <summary>
        /// 获取当前有活跃IP的网卡
        /// </summary>
        /// <param name="separator">-</param>
        /// <returns></returns>
        public static List<string> GetActiveMacAddress(string separator = "-")
        {
            var macAddress = new List<string>();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            if (nics == null || nics.Length < 1)
            {
                return macAddress;
            }

            foreach (NetworkInterface adapter in nics.Where(c =>
                c.NetworkInterfaceType != NetworkInterfaceType.Loopback && c.OperationalStatus == OperationalStatus.Up))
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();

                var unicastAddresses = properties.UnicastAddresses;
                if (unicastAddresses.Any(temp => temp.Address.AddressFamily == AddressFamily.InterNetwork))
                {
                    var address = adapter.GetPhysicalAddress();
                    if (string.IsNullOrEmpty(separator))
                    {
                        macAddress.Add(address.ToString());
                    }
                    else
                    {
                        macAddress.Add(string.Join(separator, address.GetAddressBytes()));
                    }
                }
            }
            return macAddress;
        }
    }
}