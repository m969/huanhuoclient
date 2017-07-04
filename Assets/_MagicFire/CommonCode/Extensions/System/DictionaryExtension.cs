using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/*
 *	
 *  
 *
 *	by Xuanyi
 *
 */

namespace System.Collections.Generic
{
	public static class DictionaryExtension{

        /// <summary>
        /// 尝试将键和值添加到字典中：如果不存在，才添加；存在，不添加也不抛导常
        /// </summary>
        public static Dictionary<TKey, TValue> TryAddOrNothing<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key) == false) dict.Add(key, value);
            return dict;
        }
        /// <summary>
        /// 将键和值添加或替换到字典中：如果不存在，则添加；存在，则替换
        /// </summary>
        public static Dictionary<TKey, TValue> TryAddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            dict[key] = value;
            return dict;
        }

    }
}

namespace System
{
    public static class StringExtension
    {
        public static StringBuilder String2Unicode(this string s)
        {
            char[] charbuffers = s.ToCharArray();
            byte[] buffer;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charbuffers.Length; i++)
            {
                buffer = System.Text.Encoding.Unicode.GetBytes(charbuffers[i].ToString());
                sb.Append(System.String.Format("//u{0:X2}{1:X2}", buffer[1], buffer[0]));
            }
            return sb;
        }
    }

}