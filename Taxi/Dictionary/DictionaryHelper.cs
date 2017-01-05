using System.Collections.Generic;

namespace Taxi.Dictionary
{
    public static class DictionaryHelper
    {
        /// <summary>
        /// 字典合并拓展
        /// 如果不存在key才添加，存在则不报错直接忽略
        /// </summary>
        /// <typeparam name="TKey">字典的Key类型</typeparam>
        /// <typeparam name="TValue">字典的Value类型</typeparam>
        /// <param name="first">合并的字典1</param>
        /// <param name="second">合并的字典2</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> MergeDictionaryAdd<TKey, TValue>(this Dictionary<TKey, TValue> first, Dictionary<TKey, TValue> second)
        {
            if (first == null) first = new Dictionary<TKey, TValue>();
            if (second == null) return first;

            foreach (var key in second.Keys)
            {
                if (!first.ContainsKey(key))
                    first.Add(key, second[key]);
            }
            return first;
        }

        /// <summary>
        /// 字典合并拓展
        /// 如果不存在key则添加，存在则替换,替换法则为second替换first
        /// </summary>
        /// <typeparam name="TKey">字典的Key类型</typeparam>
        /// <typeparam name="TValue">字典的Value类型</typeparam>
        /// <param name="first">合并的字典1</param>
        /// <param name="second">合并的字典2</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> MergeDictionaryReplace<TKey, TValue>(this Dictionary<TKey, TValue> first, Dictionary<TKey, TValue> second)
        {
            if (first == null) first = new Dictionary<TKey, TValue>();
            if (second == null) return first;

            foreach (var key in second.Keys)
            {
                if (first.ContainsKey(key))
                {
                    first[key] = second[key];
                }
                else
                {
                    first.Add(key, second[key]);
                }

            }
            return first;
        }

        /// <summary>
        /// 获取字典key的值，如果没有结果则返回空或者指定的值
        /// </summary>
        /// <typeparam name="TKey">字典的Key类型</typeparam>
        /// <typeparam name="TValue">字典的Value类型</typeparam>
        /// <param name="dict">查询的字典</param>
        /// <param name="key">key名称</param>
        /// <param name="defaultValue">未找到返回的值</param>
        /// <returns></returns>
        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            return dict.ContainsKey(key) ? dict[key] : defaultValue;
        }

        /// <summary>
        /// 向字典中批量添加键值对
        /// </summary>
        /// <param name="replaceExisted">如果已存在，是否替换</param>
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> values, bool replaceExisted)
        {
            foreach (var item in values)
            {
                if (dict.ContainsKey(item.Key) == false || replaceExisted)
                    dict[item.Key] = item.Value;
            }
            return dict;
        }
    }
}
