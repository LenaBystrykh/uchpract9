using System;

namespace UP9
{
    class Program
    {
        static void Main(string[] args)
        {
            int N;
            bool ok;
            Console.WriteLine("Введите N - количество элементов в списке");
            do
            {
                ok = int.TryParse(Console.ReadLine(), out N); //проверка ввода, должно быть введено целое положительное число
                if (!ok || N <= 0) Console.WriteLine("Ошибка ввода. Попробуйте снова"); //если введено не целое положительное число, вывод ошибки
            } while (!ok || N <= 0);

            DoublyLinkedList<int> list = new DoublyLinkedList<int>();
            list.Beg = list.MakeList(N);

            int choice = -1;
            while(choice !=4)
            {
                Console.WriteLine("1. Печать");
                Console.WriteLine("2. Поиск элемента");
                Console.WriteLine("3. Удаление элемента");
                Console.WriteLine("4. Выход");

                ok = int.TryParse(Console.ReadLine(), out choice);
                if (!ok || choice < 0 || choice > 4)
                {
                    do
                    {
                        Console.WriteLine("Проверьте правильность ввода");
                        ok = int.TryParse(Console.ReadLine(), out choice);
                    } while (!ok || choice < 0 || choice > 4);
                }
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        list.ShowList(list.Beg);
                        break;
                    case 2:
                        Console.WriteLine("Введите целое число - элемент, который необходимо найти");
                        int number;
                        do
                        {
                            ok = int.TryParse(Console.ReadLine(), out number);
                            if (!ok) Console.WriteLine("Ошибка ввода. Попробуйте снова");
                        } while (!ok);
                        list.Search(list.Beg, number);
                        break;
                    case 3:
                        Console.WriteLine("Введите целое число - элемент, который необходимо удалить");
                        int del;
                        do
                        {
                            ok = int.TryParse(Console.ReadLine(), out del);
                            if (!ok) Console.WriteLine("Ошибка ввода. Попробуйте снова");
                        } while (!ok) ;
                        list.Beg = list.Delete(list.Beg, del);
                        break;
                    case 4:
                        choice = 4;
                        Console.Clear();
                        break;
                }
            }
        }
    }
    public class Point<T>
    {
        public T Data { get; set; }
        public Point<T> Next { get; set; }
        public Point<T> Pred { get; set; }

        public Point()
        {
            Next = null;
            Pred = null;
            Data = default(T);
        }

        public Point(T data)
        {
            Data = data;
            Next = null;
            Pred = null;
        }

        public override string ToString()
        {
            return Data.ToString() + " ";
        }
    }

    public class DoublyLinkedList<T>
    {
        public Point<T> Beg { get; set; }

        public DoublyLinkedList()
        {
            Beg = null;
        }

        public DoublyLinkedList(Point<T> data)
        {
            Beg = data;
        }

        public DoublyLinkedList(DoublyLinkedList<T> list)
        {
            Beg = list.Beg;
        }

        public Point<int> MakePoint(int number)
        {
            Point<int> p = new Point<int>(number);
            return p;
        }
        //формирование двунаправленного списка
        public Point<int> MakeList(int size) //добавление в начало
        {
            int first = 1;
            int others;
            others = first + 1;
            Point<int> beg = MakePoint(first);
            Console.WriteLine("The element \"{0}\" is adding...", first);

            Point<int> tHelp = beg;

            for (int i = 1; i < size; i++)
            {
                Point<int> p = MakePoint(others);
                Console.WriteLine("The element \"{0}\" is adding...", others);
                p.Next = tHelp;
                tHelp.Pred = p;
                tHelp = p;
                others++;
            }
            return beg;
        }
        public void ShowList(Point<int> beg)
        {
            Console.Clear();
            //проверка наличия элементов в списке
            if (beg == null)
            {
                Console.WriteLine("The List is empty");
                return;
            }
            Point<int> p = beg;
            while (p != null)
            {
                Console.WriteLine(p);
                p = p.Pred;//переход к следующему элементу назад
            }
            Console.WriteLine();
        }
        public Point<int> Delete(Point<int> beg, int deleteThis)
        {
            int count = 0;
            //проверка наличия элементов в списке
            if (beg == null)
            {
                Console.WriteLine("The List is empty");
                return beg;
            }
            Point<int> p = beg;
            while (p != null)
            {
                if (p.Data == deleteThis)
                {
                    count++;
                    if (p.Pred == null) //последний элемент?
                    {
                        Point<int> tHelp = p.Next;
                        tHelp.Pred = null;
                        p.Next.Pred = null;
                        return beg;
                    }
                    else
                    {
                        if (p.Next == null) //первый элемент?
                        {
                            Point<int> tHelp = p.Pred;
                            tHelp.Next = null;
                            p.Pred.Next = null;
                            p.Next = null;
                            beg = p.Pred;
                            p = p.Pred;
                        }
                        else
                        {
                            Point<int> tHelp1 = p.Pred;
                            Point<int> tHelp2 = p.Next;
                            tHelp1.Next = p.Next;
                            tHelp2.Pred = p.Pred;
                            p.Pred.Next = p.Next;
                            p.Next.Pred = p.Pred;
                            p = p.Pred;
                        }
                    }
                }
                else
                {
                    p = p.Pred;//переход к следующему элементу
                }
            }
            if (count == 0) Console.WriteLine("Элемент не был найден");
            else Console.WriteLine("Элемент удалён");
            return beg;
        }
        public void Search(Point<int> beg, int findThis)
        {
            Console.Clear();
            bool found = false;
            //проверка наличия элементов в списке
            if (beg == null)
            {
                Console.WriteLine("The List is empty");
                return;
            }
            Point<int> p = beg;
            while (p != null)
            {
                
                if (p.Data == findThis)
                {
                    found = true;
                }
                p = p.Pred;//переход к следующему элементу назад
            }
            if (found == true) Console.WriteLine($"Элемент {findThis} найден");
            else Console.WriteLine($"Элемент {findThis} не найден");
        }
    }
}
