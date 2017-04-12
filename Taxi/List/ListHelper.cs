using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.List
{
    public static class ListHelper
    {
        /// <summary>
        /// 获取List内最后一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T GetLast<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }

        /// <summary>
        /// List 搜索功能
        /// </summary>
        /// <param name="t"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public static List<string> Search(this List<string> t,string word)
        {
            return t.Where(e => e.ToLower().Contains(word.ToLower())).ToList();
        }
    }
}
