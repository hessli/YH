using System;
using System.Data;
using System.Text;

namespace YH.Core.DataProvider
{
    public class DataAccessFieldHelper
    {
        /// <summary>
        /// 获取指定的字段值是否为DBNull
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool IsDBNull(IDataReader reader, string fieldName)
        {
            int col = reader.GetOrdinal(fieldName);
            if (reader.IsDBNull(col))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 从reader的中得到一个字段的bool值
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetBoolean(IDataReader reader, string fieldName, bool defaultValue)
        {
            int col = reader.GetOrdinal(fieldName);
            if (reader.IsDBNull(col))
            {
                return defaultValue;
            }
            return reader.GetBoolean(col);

        }

        /// <summary>
        /// 从reader的中得到一个字段的byte值
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static byte GetByte(IDataReader reader, string fieldName)
        {
            int col = reader.GetOrdinal(fieldName);
            if (reader.IsDBNull(col))
            {
                return 0;
            }
            return reader.GetByte(col);
        }

        /// <summary>
        /// 从reader的中得到一个字段的char值。
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static char GetChar(IDataReader reader, string fieldName, char defaultValue)
        {
            int col = reader.GetOrdinal(fieldName);
            if (reader.IsDBNull(col))
            {
                return defaultValue;
            }
            return reader.GetChar(col);
        }

        /// <summary>
        /// 从reader的中得到一个字段的string值。
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string GetString(IDataReader reader, string fieldName)
        {
            int col = reader.GetOrdinal(fieldName);
            if (reader.IsDBNull(col))
            {
                return null;
            }
            return reader.GetString(col);
        }

        /// <summary>
        /// 从reader的中得到一个字段的string值。
        /// </summary>
        /// <param name="reader">用于读取字段值的IDataReader对象</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetString(IDataReader reader, string fieldName, string defaultValue)
        {
            int col = reader.GetOrdinal(fieldName);
            if (reader.IsDBNull(col))
            {
                return defaultValue;
            }
            return reader.GetString(col);
        }

        /// <summary>
        /// 从reader的中得到一个字段的datetime值。
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(IDataReader reader, string fieldName, DateTime defaultValue)
        {
            int col = reader.GetOrdinal(fieldName);
            if (reader.IsDBNull(col))
            {
                return defaultValue;
            }
            return reader.GetDateTime(col);
        }

        /// <summary>
        /// 从reader的中得到一个字段的datetime值
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(IDataReader reader, string fieldName)
        {
            return GetDateTime(reader, fieldName, DateTime.Now);
        }

        /// <summary>
        /// 从reader的中得到一个字段的Int16值。
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static short GetInt16(IDataReader reader, string fieldName)
        {
            int col = reader.GetOrdinal(fieldName);
            if (reader.IsDBNull(col))
            {
                return 0;
            }
            return reader.GetInt16(col);

        }

        /// <summary>
        /// 从reader的中得到一个字段的Int32值。
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static int GetInt32(IDataReader reader, string fieldName)
        {
            int col = reader.GetOrdinal(fieldName);
            if (reader.IsDBNull(col))
            {
                return 0;
            }
            return reader.GetInt32(col);
        }

        /// <summary>
        /// 从reader的中得到一个字段的Int64值。
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static long GetInt64(IDataReader reader, string fieldName)
        {
            int col = reader.GetOrdinal(fieldName);
            if (reader.IsDBNull(col))
            {
                return 0;
            }
            return reader.GetInt64(col);
        }

        /// <summary>
        /// 从reader的中得到一个字段的float值。
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static float GetFloat(IDataReader reader, string fieldName)
        {
            int col = reader.GetOrdinal(fieldName);
            if (reader.IsDBNull(col))
            {
                return 0;
            }
            return reader.GetFloat(col);
        }

        /// <summary>
        /// 从reader的中得到一个字段的double值。
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static double GetDouble(IDataReader reader, string fieldName)
        {
            int col = reader.GetOrdinal(fieldName);
            if (reader.IsDBNull(col))
            {
                return 0;
            }
            return reader.GetDouble(col);

        }

        /// <summary>
        /// 从reader的中得到一个字段的decimal值。
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static decimal GetDecimal(IDataReader reader, string fieldName)
        {
            int col = reader.GetOrdinal(fieldName);
            if (reader.IsDBNull(col))
            {
                return 0;
            }
            return reader.GetDecimal(col);

        }

        /// <summary>
        /// 将应用程序的值转换为数据库值字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetBooleanValue(bool temp)
        {
            return temp ? "1" : "0";
        }

        /// <summary>
        /// 获取写入到存储过程中的字符串值,将会为其增加''标志
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetStringValue(string str)
        {
            if (str == null)
                return "''";
            if (str.IndexOf("'") != -1)
            {
                StringBuilder temp = new StringBuilder(str);
                temp.Replace("'", "''");
                return "'" + temp.ToString() + "'";
            }
            else
                return "'" + str + "'";


        }

        /// <summary>
        /// 获取null值
        /// </summary>
        /// <returns></returns>
        public static string GetNullValue()
        {
            return "NULL";
        }

        public static string GetDateTimeVlaue(DateTime time)
        {
            return "'" + time.ToString("yyyy-MM-dd HH:mm:ss") + "'";

        }

        public static string GetNumberValue(byte temp)
        {
            return temp.ToString();
        }

        public static string GetNumberValue(short temp)
        {
            return temp.ToString();
        }

        public static string GetNumberValue(int temp)
        {
            return temp.ToString();
        }

        public static string GetNumberValue(long temp)
        {
            return temp.ToString();
        }

        public static string GetNumberValue(bool temp)
        {
            return temp ? "1" : "0";
        }

        public static string GetNumberValue(float temp)
        {
            return temp.ToString();
        }

        public static string GetNumberValue(double temp)
        {
            return temp.ToString();
        }

        public static string GetNullValue(int temp)
        {
            return "NULL";
        }

    }
}
