//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace YH.Core.Notification.Filter
//{

//    public class Condition
//    {
//         public int Number { get; set; }

//        ISpecification<Condition> _has;

//        public Condition()
//        {
//            _has = new A();
//        }
//        public bool IsTrue()
//        {

//            return  _has.IsStatifiedBy(this);
//        }
//    }

//    public interface ISpecification<T>
//    {
//        bool IsStatifiedBy(T condtion);

//    }


//    public class AndSpeci<T> : AbCondition<T>
//    {

//        public AndSpeci()
//        {

//        }
//        public override bool IsStatifiedBy(T c)
//        {
//            throw new NotImplementedException();
//        }
//    }


//    public abstract class AbCondition <T>: ISpecification<T>
//    {


//        public abstract bool IsStatifiedBy(T c);

//        public ISpecification<T> And(ISpecification<T> other)
//        {


//        }
//    }



//    public class A : ISpecification<Condition>
//    {
//        public bool IsStatifiedBy(Condition condition)
//        {
           
//            return  condition.Number > 0;
//        }
//    }


//    public class B : ISpecification<Condition>
//    {
//        public bool IsStatifiedBy(Condition condtion)
//        {
//            return true;
//        }
//    }
//}
