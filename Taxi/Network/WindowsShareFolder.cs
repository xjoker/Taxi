using System;
using System.Management;
using System.Security.Principal;
using Taxi.StringHelper;
using Taxi.SystemHelper;

namespace Taxi.Network
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
        public static void ShareFolder(string FolderPath, string ShareName, string Description="", ManagementObject Acl = null)
        {
            if (FolderPath.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("FolderPath");
            }

            if (ShareName.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("ShareName");
            }

            if (!FileHelper.FileHelper.DirectoryExists(FolderPath))
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
                    if (Acl != null)
                    {
                        inParams["Access"] = Acl;
                    }
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


    public class Win32_Ace
    {
        /// <summary>
        /// 共享文件夹权限设定
        /// 这个类内含检测传入用户名对应的SID功能
        /// 要求必须在本机上才可获取，如果不在本机内需要传入
        /// SecurityIdentifier 类型
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="s"></param>
        /// <param name="ACL"></param>
        /// <param name="AceFlags"></param>
        /// <param name="AceType"></param>
        /// <returns></returns>
        public ManagementObject SecurityDescriptor(
            string Username,
            SecurityIdentifier s = null,
            Win32ShareType.ShareAccessMask ACL = Win32ShareType.ShareAccessMask.Read,
            int AceFlags = 3,
            Win32ShareType.ShareAceType AceType = 0
            )
        {
            if (s == null)
            {
                try
                {
                    NTAccount f = new NTAccount(Username);
                    s = (SecurityIdentifier)f.Translate(typeof(SecurityIdentifier));
                }
                catch
                {
                    throw new WindowsShareFolderException("Get User SID error!");
                }
            }

            byte[] sidArray = new byte[s.BinaryLength];
            s.GetBinaryForm(sidArray, 0);
            ManagementObject Trustee = new ManagementClass(new ManagementPath("Win32_Trustee"), null);
            Trustee["Name"] = Username;
            Trustee["SID"] = sidArray;
            ManagementObject ACE = new ManagementClass(new ManagementPath("Win32_Ace"), null);
            ACE["AccessMask"] = ACL;
            ACE["AceFlags"] = AceFlags;
            ACE["AceType"] = AceType;
            ACE["Trustee"] = Trustee;
            ManagementObject SecDesc = new ManagementClass(new ManagementPath("Win32_SecurityDescriptor"), null);
            SecDesc["ControlFlags"] = 4;
            SecDesc["DACL"] = new object[] { ACE };
            return SecDesc;
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

        /*
        AccessMask

        fullcontrol = 2032127
        change = 1245631
        read = 1179785

        */
        public enum ShareAccessMask : uint
        {
            FullControl = 2032127,
            ReadAndWrite = 1245631,
            Read = 1179785
        }

        /*
        AceFlags

        */

        public enum ShareAceFlags : int
        {
            OBJECT_INHERIT_ACE = 1,
            CONTAINER_INHERIT_ACE = 2,
            NO_PROPAGATE_INHERIT_ACE = 4,
            INHERIT_ONLY_ACE = 8,
            INHERITED_ACE = 16,
            SUCCESSFUL_ACCESS_ACE_FLAG = 64,
            FAILED_ACCESS_ACE_FLAG = 128
        }


        public enum ShareAceType : int
        {
            Allow = 0,
            Deny = 1,
            SystemAudit = 2
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

    class WindowsShareFolderException : ApplicationException
    {
        public WindowsShareFolderException(string message) : base(message) { }

        public override string Message
        {
            get
            {
                return base.Message;
            }
        }
    }
}
