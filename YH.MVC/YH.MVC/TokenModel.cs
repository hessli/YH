using System;

namespace YH.MVC
{
    [Serializable]
    public class TokenModel
    {
        /// <summary>
        /// 旧的授权令牌的key
        /// 保证AccessToken请求时有效，当AccessToken变化时，不影响当次请求
        /// </summary>
        public string OldAccessToken
        {
            get;
            set;
        }

        /// <summary>
        /// 授权令牌的key
        /// </summary>
        public string AccessToken
        {
            get;
            set;
        }

        /// <summary>
        /// 刷新授权令牌的key
        /// </summary>
        public string RefreshToken
        {
            get;
            set;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId
        {
            get;
            set;
        }

        /// <summary>
        /// 经销商Id
        /// </summary>
        public int DealerId
        {
            get;
            set;
        }

        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId
        {
            get;
            set;
        }
        public string SeniorId
        {
            get;
            set;
        }
        /// <summary>
        /// 剩余有效期(秒）
        /// </summary>
        public uint ExpiriesTime
        {
            get;
            set;
        }
        public string LoginName { get; set; }
    }
}
