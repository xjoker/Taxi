using System;
using System.Management;
using Taxi.StringHelper;
using Taxi.System;

namespace Taxi.FileHelper
{
    public static class WindowsShareFolder
    {
        private static ManagementClass managementClass = new ManagementClass("Win32_Share");

        /// <summary>
        /// 文件夹共享
        /// </summary>
        /// <param name="FolderPath">共享文件夹的路径</param>
        /// <param name="ShareName">共享名</param>
        /// <param name="Description">备注</param>
        public static void ShareFolder(string FolderPath, string ShareName, string Description)
        {
            if (FolderPath.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("FolderPath");
            }

            if (ShareName.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("ShareName");
            }

            if (Description.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("Description");
            }

            if (!FileHelper.DirectoryExists(FolderPath))
            {
                throw new Exception("Directory not exists!");
            }


            if (CheckRunAs.IsRunAsAdmin())
            {
                try
                {
                    // Create ManagementBaseObjects for in and out parameters
                    ManagementBaseObject inParams = managementClass.GetMethodParameters("Create");
                    ManagementBaseObject outParams;
                    // Set the input parameters
                    inParams["Description"] = Description;
                    inParams["Name"] = ShareName;
                    inParams["Path"] = FolderPath;
                    inParams["Type"] = Win32ShareType.ShareType.DiskDrive;
                    outParams = managementClass.InvokeMethod("Create", inParams, null);
                    // Check to see if the method invocation was successful
                    if ((uint)(outParams.Properties["ReturnValue"].Value) != 0)
                    {
                        throw new Exception("Unable to share directory.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("Must run as Administrator");
            }
        }

        /// <summary>
        /// 删除共享
        /// </summary>
        /// <returns></returns>
        public static Win32ShareType.MethodStatus Delete(string ShareName)
        {
            if (ShareName.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("ShareName");
            }

            string ShareQuery = $"SELECT * FROM Win32_Share WHERE name = '{ShareName}'";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(ShareQuery);
            foreach (ManagementObject share in searcher.Get())
            {
                string name = share["Name"].ToString();
                if (name.IsEqualsString(ShareName))
                {
                    share.Delete();
                    return Win32ShareType.MethodStatus.Success;
                }
            }
            return Win32ShareType.MethodStatus.UnknownFailure;
        }

        /// <summary>
        /// 获取共享名称对应路径
        /// </summary>
        /// <param name="shareName"></param>
        /// <returns></returns>
        public static string GetLocalPathForShare(string ShareName)
        {
            if (ShareName.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("ShareName");
            }

            using (var search = new ManagementObjectSearcher(new WqlObjectQuery("select Name, Path from Win32_Share")))
            {
                foreach (var share in search.Get())
                {
                    if (share["Name"].ToString().IsEqualsString(ShareName))
                    {
                        return share["Path"].ToString();
                    }
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Windows 共享类
    /// </summary>
    public class Win32ShareType
    {
        public enum MethodStatus : uint
        {
            Success = 0, 	//Success
            AccessDenied = 2, 	//Access denied
            UnknownFailure = 8, 	//Unknown failure
            InvalidName = 9, 	//Invalid name
            InvalidLevel = 10, 	//Invalid level
            InvalidParameter = 21, 	//Invalid parameter
            DuplicateShare = 22, 	//Duplicate share
            RedirectedPath = 23, 	//Redirected path
            UnknownDevice = 24, 	//Unknown device or directory
            NetNameNotFound = 25 	//Net name not found
        }

        public enum ShareType : uint
        {
            DiskDrive = 0x0, 	//Disk Drive
            PrintQueue = 0x1, 	//Print Queue
            Device = 0x2, 	//Device
            IPC = 0x3, 	//IPC
            DiskDriveAdmin = 0x80000000, 	//Disk Drive Admin
            PrintQueueAdmin = 0x80000001, 	//Print Queue Admin
            DeviceAdmin = 0x80000002, 	//Device Admin
            IpcAdmin = 0x80000003 	//IPC Admin
        }


        private ManagementObject mWinShareObject;


        public uint AccessMask
        {
            get { return Convert.ToUInt32(mWinShareObject["AccessMask"]); }
        }

        public bool AllowMaximum
        {
            get { return Convert.ToBoolean(mWinShareObject["AllowMaximum"]); }
        }

        public string Caption
        {
            get { return Convert.ToString(mWinShareObject["Caption"]); }
        }

        public string Description
        {
            get { return Convert.ToString(mWinShareObject["Description"]); }
        }

        public DateTime InstallDate
        {
            get { return Convert.ToDateTime(mWinShareObject["InstallDate"]); }
        }

        public uint MaximumAllowed
        {
            get { return Convert.ToUInt32(mWinShareObject["MaximumAllowed"]); }
        }

        public string Name
        {
            get { return Convert.ToString(mWinShareObject["Name"]); }
        }

        public string Path
        {
            get { return Convert.ToString(mWinShareObject["Path"]); }
        }

        public string Status
        {
            get { return Convert.ToString(mWinShareObject["Status"]); }
        }

        public ShareType Type
        {
            get { return (ShareType)Convert.ToUInt32(mWinShareObject["Type"]); }
        }
    }
}
