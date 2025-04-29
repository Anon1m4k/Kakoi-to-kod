using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace пара1
{
    public class Program
    {
        static void Main()
        {
            int a1 = 3;
            int a2 = 5;
            int i;
            string b1 = "абв";
            string b2 = "вба";  //переменные
            double d = 2;
            double m;
            float z = 3;
            char c;
            bool f;

            var j = 0;

            int[] x = { 1, 2, 3, 4, 5, 6 }; //массив
           
            if (d > z)
            {
                m = d + z;  //операторы
            }
            else
            {
                m = d * z;
            }

            switch (j)
            {
                case 1:
                    int h = a1 + a2;
                    break;
            }

            for (i = 0; i < a1; i++)
            {
                j += i;
            }

            while (i < 2)
            {
                Console.WriteLine(i); //циклы
            }

            foreach (int y in x)
            {
                Console.WriteLine(y);
            }

            string b = b1 + b2; //склеивание
            Console.WriteLine(b);

            String line; //чтение
            try
            {             
                StreamReader sr = new StreamReader("D:\\текст.txt");              
                line = sr.ReadLine();              
                while (line != null)
                {
                    Console.WriteLine(line);                 
                    line = sr.ReadLine();
                }             
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

            try //запись
            {        
                StreamWriter sw = new StreamWriter("D:\\текст.txt");
                sw.WriteLine("Hello World!!");          
                
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
           
           Console.ReadKey();
        }
    }
}
