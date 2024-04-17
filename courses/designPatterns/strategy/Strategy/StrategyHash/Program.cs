using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StrategyHash
{
    public abstract class SerializerObject
    {
        public abstract string Serialize(object source);
    }

    class UsualSerializerObject : SerializerObject
    {
        public override string Serialize(object source)
        {
            if(source ==null)
                return null;
            return source.ToString();
        }
    }

    class XMLSerializerObject : SerializerObject
    {
        public override string Serialize(object source)
        {
            var xs=new XmlSerializer(source.GetType());
            using (var textWriter = new StringWriter())
            {
                xs.Serialize(textWriter, source);
                return textWriter.ToString();
            }
            

        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Serialize()
        {
            return ToString();
        }

        public override string ToString()
        {
            return (FirstName + "--" + LastName);
        }

        public string SerializeWithStrategy(SerializerObject s )
        {
            return s.Serialize(this);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Person p=new Person();
            p.FirstName = "Andrei";
            p.LastName = "Ignat";
            Console.WriteLine(p.Serialize());


            ////what if we want to add xml, json, other?
            Console.WriteLine(p.SerializeWithStrategy(new UsualSerializerObject()));
            Console.WriteLine(p.SerializeWithStrategy(new XMLSerializerObject()));

        }
    }
}
