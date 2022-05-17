using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListExemple
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Origin List Exemple");
            Console.WriteLine();

            List<ParameterOfList> originList = new List<ParameterOfList>();
            for (int i = 0; i < 10; i++)
            {
                ParameterOfList list = new ParameterOfList();
                list.FirstName = $"FirstName{i}";
                list.LastName = $"LastName{i}";
                originList.Add(list);
                Console.WriteLine(string.Format("{0}---{1}", originList[i].FirstName, originList[i].LastName));
            }

            Console.WriteLine("=============================");
            Console.WriteLine();
            //=============================//
            Console.WriteLine("My List Exemple");
            MyList<ParameterOfList> myList = new MyList<ParameterOfList>(5);
            for (int i = 0; i < 5; i++)
            {
                ParameterOfList list = new ParameterOfList();
                list.FirstName = $"FirstName{i}";
                list.LastName = $"LastName{i}";
                myList.Add(list);
                Console.WriteLine(string.Format("{0}---{1}", myList[i].FirstName, myList[i].LastName));
            }

            Console.ReadLine();
        }
    }
}
