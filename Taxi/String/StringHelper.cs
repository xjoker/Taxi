﻿using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Linq;
using System;

namespace Taxi.StringHelper
{
    public static class StringHelper
    {
        /// <summary>
        /// 转换为指定编码
        /// </summary>
        /// <param name="data">元数据</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string Decode(this byte[] data, Encoding encoding)
        {
            return encoding.GetString(data);
        }

        /// <summary>
        /// 封装 IsNullOrEmpty为Bool
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 封装 IsNullOrWhiteSpace为Bool
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// 判断是否为int类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsInt(this string s)
        {
            return int.TryParse(s, out int i);
        }

        /// <summary>
        /// 转换为int类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(this string s)
        {
            return int.Parse(s);
        }

        /// <summary>
        /// 鉴定是否为JSON格式
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static bool IsValidJson(this string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) ||
                (strInput.StartsWith("[") && strInput.EndsWith("]")))
            {
                Encoding encoding = Encoding.UTF8;
                using (var reader = JsonReaderWriterFactory.CreateJsonReader(encoding.GetBytes(strInput), XmlDictionaryReaderQuotas.Max))
                {
                    try
                    {
                        var b = XElement.Load(reader).Attribute("type").Value;
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 字符串相等判断
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool IsEqualsString(this string s1,string s2,bool IgnoreCase=false)
        {
            if (IgnoreCase)
            {
                return string.Equals(s1, s2);
            }
            else
            {
                return string.Equals(s1.ToLower(), s2.ToLower());
            }
        }
        
        /// <summary>
        /// 将文本快速写入文件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="FilePath">写入路径</param>
        /// <param name="replaceExisted">是否覆盖已经存在的文件</param>
        /// <returns></returns>
        public static bool WriteToFile(this string s,string FilePath, bool replaceExisted=false)
        {
            try
            {
                if (!File.Exists(FilePath)|| replaceExisted)
                {
                    string directory = Path.GetDirectoryName(FilePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    File.WriteAllText(FilePath, s);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
            
        }

    }
}
