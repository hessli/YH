using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace YH.Core.Table
{
    public static class ConvertToDataTable
    {
        /// <summary>
        /// 用来得到结果集，支持往下一层数据,但只有一层，下一层所有方法中是类属性都排除掉。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sachmaEquelDBTable">转换结构是否和数据库表相同，如果相同这去掉导航属性的信息。</param>
        /// <returns></returns>
        //[Obsolete("该方法只限于高级用途，支持特殊功能的需要，日常使用应该避免")]
        public static DataTable ToTable<T>(this IEnumerable<T> source, bool sachmaEquelDBTable = false)
        {

            DataTable dt = new DataTable("dataResult");


            //ArrayList al = new ArrayList(source.ToArray());

            //var al = source.ToList();
            var al = source.AsQueryable().ToList();

            getTableColomns<T>(ref dt, sachmaEquelDBTable);
            if (al.Count > 0)
            {
                getTableRows(al, ref dt, sachmaEquelDBTable);
            }
            return dt;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> source, bool sachmaEquelDBTable = false)
        {
            DataTable dt = new DataTable("dataResult");
            var al = source.AsQueryable().ToList();
            if (source.Count() > 0)
            {
                var entity = source.First();
                GetTableColomns(entity, ref dt, sachmaEquelDBTable);
                if (al.Count > 0)
                {
                    getTableRows(al, ref dt, sachmaEquelDBTable);
                }
            }
            else
            {
                getTableColomns<T>(ref dt, sachmaEquelDBTable);
                if (al.Count > 0)
                {
                    getTableRows(al, ref dt, sachmaEquelDBTable);
                }
            }
            return dt;
        }

        private static void GetTableColomns<T>(T entity, ref DataTable dt, bool sachmaEquelDBTable = false)
        {
            Type t = entity.GetType();
            if (t.IsClass)
            {
                System.Reflection.PropertyInfo[] properties = t.GetProperties(System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.Public).Where(x => !typeof(ICollection).IsAssignableFrom(x.GetType())).ToArray();

                foreach (PropertyInfo item in properties)
                {

                    if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                    {
                        if (!dt.Columns.Contains(item.Name))
                        {
                            dt.Columns.Add(item.Name, getOjectTypeOrDefault(item));
                        }
                    }
                    else
                    {
                        if (sachmaEquelDBTable)
                        {
                            continue;
                        }
                        Type t1 = item.PropertyType;
                        System.Reflection.PropertyInfo[] Itemproperties = t1.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                        foreach (PropertyInfo detid in Itemproperties)
                        {
                            if (detid is ICollection)
                            {
                                continue;
                            }
                            if (detid.PropertyType.IsValueType || detid.PropertyType.Name.StartsWith("String"))
                            {
                                if (!dt.Columns.Contains(detid.Name))
                                {
                                    dt.Columns.Add(detid.Name, getOjectTypeOrDefault(detid));
                                }

                            }
                        }

                    }

                }
            }
        }

        private static void getTableColomns<T>(ref DataTable dt, bool sachmaEquelDBTable = false)
        {
            Type t = typeof(T);
            if (t.IsClass)
            {
                System.Reflection.PropertyInfo[] properties = t.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                foreach (PropertyInfo item in properties)
                {
                    if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                    {
                        if (!dt.Columns.Contains(item.Name))
                        {
                            dt.Columns.Add(item.Name, getOjectTypeOrDefault(item));
                        }
                    }
                    else
                    {
                        if (sachmaEquelDBTable)
                        {
                            continue;
                        }
                        Type t1 = item.PropertyType;
                        System.Reflection.PropertyInfo[] Itemproperties = t1.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                        foreach (PropertyInfo detid in Itemproperties)
                        {
                            if (detid is ICollection)
                            {
                                continue;
                            }
                            if (detid.PropertyType.IsValueType || detid.PropertyType.Name.StartsWith("String"))
                            {
                                if (!dt.Columns.Contains(detid.Name))
                                {
                                    dt.Columns.Add(detid.Name, getOjectTypeOrDefault(detid));
                                }

                            }
                        }

                    }

                }
            }
        }

        private static void getTableRows<T>(List<T> Sources, ref DataTable dt, bool sachmaEquelDBTable = false)
        {
            foreach (object source in Sources)
            {
                Type t = source.GetType();
                DataRow cdr = dt.NewRow();
                if (t.IsClass)
                {
                    foreach (PropertyInfo item in
                        t.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {
                        if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                        {
                            cdr[item.Name] = getOjectValueOrDefault(source, item);
                        }
                        else
                        {
                            if (sachmaEquelDBTable)
                            {
                                continue;
                            }
                            object objEnd = item.GetValue(source, null);
                            if (objEnd == null)
                            {
                                continue;
                            }
                            Type t1 = objEnd.GetType();

                            foreach (PropertyInfo detid in t1.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                            {
                                if (detid.PropertyType.IsValueType || detid.PropertyType.Name.StartsWith("String"))
                                {
                                    if (dt.Columns.Contains(detid.Name))
                                    {
                                        cdr[detid.Name] = getOjectValueOrDefault(objEnd, detid);
                                    }
                                }
                            }
                        }
                    }

                    dt.Rows.Add(cdr);
                }
            }
        }

        public static DataTable ConvertToTable<T>(IEnumerable<T> source, string TableName = "dataResult", bool sachmaEquelDBTable = false)
        {

            DataTable dt = new DataTable(TableName);
            var al = source.AsQueryable().ToList();

            getTableColomns<T>(ref dt, sachmaEquelDBTable);

            if (al.Count > 0)
            {
                getTableRows(al, ref dt, sachmaEquelDBTable);
            }
            return dt;
        }

        public static IDictionary<Type, string> _SqlDbTypeConvertor = new Dictionary<Type, string>();
        public static IDictionary<Type, string> SqlDbTypeConvertor
        {
            get
            {
                if (_SqlDbTypeConvertor.Count == 0)
                {
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(Int32), "int"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(Nullable<Int32>), "int"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(Int64), "bigint"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(Nullable<Int64>), "bigint"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(Boolean), "bit"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(Nullable<Boolean>), "bit"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(string), "nvarchar(max)"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(DateTime), "datetime2(7)"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(Nullable<DateTime>), "datetime2(7)"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(DateTimeOffset), "datetimeoffset(7)"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(Nullable<DateTimeOffset>), "datetimeoffset(7)"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(decimal), "decimal(23,2)"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(Nullable<decimal>), "decimal(23,2)"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(Double), "float"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(Nullable<Double>), "float"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(Int16), "smallint"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(Nullable<Int16>), "smallint"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(Byte), "tinyint"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(Nullable<Byte>), "tinyint"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(Guid), "uniqueidentifier"));
                    _SqlDbTypeConvertor.Add(new KeyValuePair<Type, string>(typeof(Nullable<Guid>), "uniqueidentifier"));
                }

                return _SqlDbTypeConvertor;
            }
        }

        private static Type getOjectTypeOrDefault(PropertyInfo pinfo)
        {
            if (!pinfo.PropertyType.Name.StartsWith("Nullable"))
            {
                return pinfo.PropertyType;
            }
            else
            {
                Type t = pinfo.PropertyType;
                Type rT = typeof(Nullable);

                System.Reflection.PropertyInfo Itempropertie = t.GetProperty("Value");

                rT = Itempropertie.PropertyType;

                return rT;
            }
        }


        private static object getOjectValueOrDefault(object oparent, PropertyInfo pinfo)
        {
            object result = new object();
            if (!pinfo.PropertyType.Name.StartsWith("Nullable"))
            {
                result = pinfo.GetValue(oparent, null);
            }
            else
            {

                System.Reflection.PropertyInfo Itempropertie = pinfo.PropertyType.GetProperty("Value");
                object item = pinfo.GetValue(oparent, null);
                if (item == null)
                {
                    result = DBNull.Value;
                }
                else
                {
                    object ip = Itempropertie.GetValue(item, null);

                    result = ip == null ? DBNull.Value : ip;
                }
            }

            return result;
        }

        /// <summary>
        /// 将数据行转换成实体
        /// </summary>
        /// <param name="row">要转换的数据行</param>
        /// <returns>实体</returns>
        public static TEntity GetEntityByDataRow<TEntity>(System.Data.DataRow row)
        {
            int i;
            TEntity entity;
            Type entityType = typeof(TEntity);
            System.Reflection.PropertyInfo[] properties = entityType.GetProperties();

            entity = (TEntity)Assembly.GetAssembly(entityType).CreateInstance(entityType.FullName, false);

            try
            {
                for (i = 0; i < properties.Length; i++)
                {
                    if (properties[i].PropertyType.IsPrimitive || properties[i].PropertyType.IsValueType || properties[i].PropertyType.Name.StartsWith("String"))
                    {
                        if ((row[properties[i].Name] ?? properties[i].PropertyType.Assembly.CreateInstance(properties[i].PropertyType.FullName)) == null)
                        {
                            continue;
                        }

                        if (row[properties[i].Name] == DBNull.Value)
                        {
                            if (properties[i].PropertyType.Name.StartsWith("String"))
                            {
                                continue;
                            }
                            properties[i].SetValue(
                                entity, properties[i].PropertyType.Assembly.CreateInstance(properties[i].PropertyType.FullName), null);
                        }
                        else
                        {
                            properties[i].SetValue(
                                entity, row[properties[i].Name], null);
                        }
                    }
                }

                return entity;
            }
            catch
            {
                return default(TEntity);
            }
        }
    }
}
