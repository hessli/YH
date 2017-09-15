using System.Diagnostics;

namespace YH.Core.Track
{
    public class Tracked
    {
        public string CallerMehodName { get;private  set; }

        public string CallerClassName { get; private  set; }

        public int LineNo { get; private set; }

        private StackTrace _stackTrace;

        public Tracked(StackTrace stacktrace)
        {
            _stackTrace = stacktrace;
            GetStackInfo();
        }

        private void GetStackInfo()
        {
            var stackTraces= _stackTrace.GetFrames();

            foreach (var item in stackTraces)
            {
                if (!item.Equals(".ctor"))
                {
                    var method= item.GetMethod();
                    this.CallerMehodName = method.Name;
                    this.CallerClassName = method.ReflectedType.Name;
                    this.LineNo = item.GetFileLineNumber();
                    break;
                }   
            }
        } 
    }
}
