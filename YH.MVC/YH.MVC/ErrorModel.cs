namespace YH.MVC
{
    public class ErrorModel
    {
        public ErrorModel(int errcode, string msg)
        {
            this.errcode = errcode;
            this.msg = msg;
        }
        public int errcode
        {
            get;
            private set;
        }

        public string msg
        {
            get;
            private set;
        }
    }
}