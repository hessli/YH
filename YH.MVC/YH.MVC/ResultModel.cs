namespace YH.MVC
{
    public class ResultModel
    {
        ResultModel(int isSuccess, object data)
        {
            this.issuccess = isSuccess;

            this.data = data;
        }
        ResultModel(int isSuccess,  object data, int code,string msg)
        {
            this.issuccess = isSuccess;

            this.data = data;

            this.code = code;

            this.msg = msg;

           
        }
        public int issuccess
        {
            get;
            private set;
        }

        public int code { get; set; }

        public object data
        {

            get;
            private set;
        }
        public string msg { get; private set; }

        public static ResultModel Success()
        {
            var model = new ResultModel(1, null);

            return model;
        }


        public static ResultModel Success(string msg)
        {
            var model = new ResultModel(1,null,1,msg);

            return model;
        }


        public static ResultModel CreateResult(object data,int issuccess,int code,string msg)
        {
            var model = new ResultModel(issuccess,data,code,msg);

            return model;
        }

        public static ResultModel CreateResult( int issuccess, int code, string msg)
        {
            var model = new ResultModel(issuccess,null, code, msg);

            return model;
        }


        public static ResultModel Success(object data)
        {
            var model = new ResultModel(1, data);

            return model;
        }

        public static ResultModel Error(string msg)
        {

            var model = new ResultModel(0,null,0,msg);

            return model;
        }

        public static ResultModel Error(int code,string msg)
        {
            var model = new ResultModel(0, null, code, msg);

            return model;
        }
      
        public static ResultModel Error(object data, int code, string msg)
        {
            var model = new ResultModel(0, data, code, msg);

            return model;
          
        }
    }
}