using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace пара1
{
    public class People
    {
        private string name_;
        private int age_;
        public People (string name, int age) //конструктор
        {
            name_= name;
            age_= age;
        }         
    }
}
