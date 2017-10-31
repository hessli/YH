namespace YH.MVC
{
    public enum TopExceptionType
    {
        成功 = 1,
        失败 = 0
    }
    /// <summary>
    /// 二级返回码
    /// </summary>
    public enum ExceptionType
    {
        HttpMethod错误 = -100000,
        InputStream为空 = -100001,
        反序列化数据失败 = -100003,

        认证失败 = -200000,
        Token非法 = -200001,
        Token缓存异常 = -200002,
        AccessToken过期 = -200003,
        RrefreshToken过期 = -200004,
    }
}
