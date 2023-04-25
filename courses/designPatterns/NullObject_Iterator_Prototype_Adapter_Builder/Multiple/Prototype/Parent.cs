using System;

namespace Prototype
{
    partial class Program
    {
        class Parent :ICloneable
        {
            public int Age { get; set; }
            public Child MyChild { get; set; }

            public object Clone()
            {
                //TODO: serialize + unserialize 
                //with Newtonsoft json

                var p = ShallowClone();
                //TODO: clone the child 
                var c = new Child();
                c.Age = this.MyChild.Age;

                p.MyChild = c;
                return p;
            }

            public Parent ShallowClone()
            {
                return this.MemberwiseClone() as Parent;
            }
        }
    }
}
