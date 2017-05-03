using System.Collections;
using System.Text;

namespace Taxi.Array
{
    public static class ArrayHelper
    {
        /// <summary>
        /// 数组转为String类型
        /// </summary>
        /// <param name="arr">数组</param>
        /// <param name="separator">分隔符,默认为","</param>
        /// <returns></returns>
        public static string JoinToString(this IEnumerable arr, string separator=",")
        {
            StringBuilder stringBuilder = new StringBuilder();
            string value = string.Empty;
            foreach (object current in arr)
            {
                stringBuilder.Append(value);
                stringBuilder.Append(current.ToString());
                value = separator;
            }
            return stringBuilder.ToString();
        }
    }
}
