namespace System
{
    public static class ConvertExtend
    {

        #region 枚举
        public static int ToInt(this System.Enum value)
        {
            var result = (int)System.Enum.Parse(value.GetType(), value.ToString());
            return result;
        }
        public static short ToShort(this System.Enum value)
        {
            return (short)value.ToInt();
        }
        #endregion
    }
}
