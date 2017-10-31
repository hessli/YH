using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace YH.Core.Enums
{
    public  class EnumManage
    {
        private System.Enum _enumItem = null;

        private Type _enumType = null;

        public EnumManage(System.Enum enumItem)
        {
            _enumItem = enumItem;

            _enumType = enumItem.GetType();
        }

        public EnumManage(Type enumType)
        {
            _enumType = enumType;
        }

        public IList<EnumProperty> GetAllItem()
        {
            IList<EnumProperty> properties = new List<EnumProperty>();

            var enumItems = System.Enum.GetValues(_enumType);

            foreach (var item in enumItems)
            {
                var desc = GetDescription(item);

                properties.Add(new EnumProperty(Convert.ToInt32(item), desc, (System.Enum)item));
            }
            return properties;
        }

        public IList<EnumProperty> GetFlagItem()
        {
            IList<EnumProperty> properties = new List<EnumProperty>();

            var value = Convert.ToInt32(_enumItem);

            var allItems = GetAllItem();

            foreach (var item in allItems)
            {
                if ((item.Value & value) > 0)
                {
                    properties.Add(item);
                }
            }
            return properties;
        }
        private string GetDescription(object value)
        {
            FieldInfo fieldinfo = _enumType.GetField(value.ToString());
            if (fieldinfo != null)
            {
                object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs.Length == 0)
                {
                    return _enumItem.ToString();
                }
                else
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    return da.Description;
                }
            }
            else return string.Empty;
        }
    }
}
