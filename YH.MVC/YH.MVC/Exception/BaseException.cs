using System;

namespace YH.MVC
{
   public class BaseException:Exception
    {
        #region Variants,Constructors

        private int _errorCode;
        private string _message;

        public BaseException(int exceptionCode)
        {
            this._message = null;
            this._errorCode = 0;
            this._errorCode = exceptionCode;
        }

        public BaseException(ExceptionType exceptionType)
            : this((int)exceptionType, Enum.GetName(typeof(ExceptionType), exceptionType))
        {
        }

        public BaseException(int exceptionCode, string errorMsg)
            : base(errorMsg)
        {
            this._message = null;
            this._errorCode = 0;
            this._errorCode = exceptionCode;
            this._message = errorMsg;
        }

        #endregion

        #region Properties

        public int ErrorCode
        {
            get
            {
                return this._errorCode;
            }
        }

        public override string Message
        {
            get
            {
                if (string.IsNullOrEmpty(this._message))
                {
                    return base.Message;
                }
                return this._message;
            }
        }

        #endregion
    }
}
