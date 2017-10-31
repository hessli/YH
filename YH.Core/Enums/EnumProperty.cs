using System;

namespace YH.Core.Enums
{
    public class EnumProperty
    {
        public EnumProperty(int value, string desc, Enum item)
        {
            _value = value;
            _desc = desc;
            _item = item;
        }
        private System.Enum _item;
        public System.Enum Item
        {
            get
            {
                return _item;
            }
        }
        private int _value;
        public int Value
        {
            get
            {
                return _value;
            }
        }

        private string _desc;
        public string Desc
        {
            get
            {
                return _desc;
            }
        }
    }
}
