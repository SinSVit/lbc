using System.ComponentModel;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

namespace ConsoleApp1
{

    internal class Program
    {
        class SPfile
        {
            private string PIB, num;
            private int course;
            private double avg = 0;
            private double[] marks = new double[10];
            public SPfile()
            {
                PIB = "Іванов Іван Іванович";
                num = "19457";
                course = 1;
                avg = 0;
                for (int i = 0; i < 10; i++)
                {
                    marks[i] = 0;
                }
            }
            public SPfile(string PIB, string num, int course, double avg, double[] marks)
            {
                this.PIB = PIB;
                this.num = num; 
                this.course = course;
                this.avg = avg;
                this.marks = marks;
            }
            public void SetMark(int num, double mark)
            {
                marks[num] = mark;
                AverageMark();
            }
            public double GetMark(int num) => marks[num];
            public double[] Compare(SPfile b)
            {
                double[] temp = new double[10];
                for (int i = 0; i < 10; i++) 
                    temp[i] = this.GetMark(i)-b.GetMark(i);
                return temp;
            }
            public void Show()
            {
                Console.WriteLine("ПІБ: " + PIB.ToString() + "\nНомер залікової книжки: " + num + "\nКурс: " + course + "\nСередня оцінка: " + avg);
            }
            public void AverageMark()
            {
                for (int i = 0; i < 10; i++) avg += marks[i];
                avg /= 10;
            }
            public void SetName(string name)
            {
                PIB = name;
            }
            public string GetName()
            {
                return PIB;
            }
            public void SetNum(string num)
            {
                this.num = num;
            }
            public void SetCourse(int course)
            {
               this.course = course;
            }
            public void Add()
            {
                bool Ready = false;
                string input = "";
                Console.WriteLine("Додавання студента\n");
                while (!Ready)
                {
                    Console.WriteLine("Введіть ПІБ\n");
                    input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Ready = false;
                        Console.WriteLine("Ім'я не може бути пустим");
                        continue;
                    }
                        for (int i = 0; i < input.Length; i++)
                        {
                            if (!char.IsDigit(input[i])) Ready = true;
                            else { Ready = false; Console.WriteLine("Ім'я не може мати цифри"); break; }
                        }
                }
                SetName(input);
                Ready = false;
                while (!Ready)
                {
                    Console.WriteLine("Введіть номер залікової книжки\n");
                    input = Console.ReadLine();
                    for (int i = 0; i < input.Length; i++)
                    {
                        if (char.IsDigit(input[i])) Ready = true;
                        else { Ready = false; Console.WriteLine("Номер залікової книжки может мати тільки цифри"); continue; }
                    }
                }
                SetNum(input);
                Ready = false;
                int course = 0;
                while (!Ready)
                {
                    Console.WriteLine("Введіть курс\n");
                    input = Console.ReadLine();
                    if (int.TryParse(input, out course)) Ready = true;
                    else { Ready = false; Console.WriteLine("Введіть ціле число"); continue; }
                }
                SetCourse(course);
                Ready = false;
                Console.WriteLine("Введіть 10 оцінок\n");
                while (!Ready)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Ready = false;
                        while (!Ready)
                        {
                            double mark = 0.0;
                            input = Console.ReadLine();
                            if (double.TryParse(input, out mark))
                            {
                                if (mark >= 0 && mark <= 100)
                                {
                                    Ready = true; SetMark(i, mark);
                                }
                                else
                                {
                                    Ready = false; Console.WriteLine("Оцінка має бути від 0 до 100"); continue;
                                }
                            }
                            else { Ready = false; Console.WriteLine("Введіть число з крапкою"); continue; }
                        }
                    }
                    Ready = true;
                }
            }
        }


        static void Main(string[] args)
        {
            string a = " ";
            int n = 1;
            SPfile[] array = new SPfile[n];
            n = 0;
            while (a != "0")
            {

                
                Console.WriteLine("Профіль студенту університету\n\n1.\tДодати студента\n2.\tВидалити елемент\n3.\tСписок студентів\n4.\tПрофіль студента\n5.\tПорівняння студентів\n6.\tЗміна оцінки\n0.\tВихід\n\n");
                a = Console.ReadLine();
                switch (a)
                {
                    case "1": 
                        {
                            n++;
                            SPfile[] temp = new SPfile[n];
                            for (int i = 0; i < n; i++)
                                temp[i] = new SPfile();
                            
                            for (int i = 0; i < n - 1; i++)
                            {
                                temp[i] = array[i];
                            }
                            temp[n-1].Add();
                            Console.Clear();
                            temp[n - 1].Show();
                            array = temp;
                        } 
                        break;
                    case "2": 
                        {
                            if (n == 0)
                            {
                                Console.WriteLine("Немає жодного студента"); break;
                            }
                            SPfile[] temp = new SPfile[n-1];
                            Console.WriteLine("Введіть порядковий номер студента\n");
                            int num = Convert.ToInt32(Console.ReadLine());
                            int counter = 0;
                            for (int i = 0; i < n; i++)
                            {
                                if (i == num) continue;
                                else temp[counter] = array[i];
                                counter++;
                            }
                            array = temp;
                            n--;
                            Console.Clear();
                        }
                        break;
                    case "3":
                        {
                            if (n == 0) 
                            {
                                Console.WriteLine("Немає жодного студента"); break;
                            }
                            for (int i = 0; i < n; i++)
                            {
                                Console.WriteLine(array[i].GetName());
                            }
                        }
                        break;
                    case "4":
                        {
                            if (n == 0)
                            {
                                Console.WriteLine("Немає жодного студента"); break;
                            }
                            Console.WriteLine("Введіть порядковий номер студента\n");
                            int num = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                            array[num].Show();
                        }
                        break;
                    case "5":
                        {
                            if (n == 0)
                            {
                                Console.WriteLine("Немає достатньої кількості студентів"); break;
                            }
                            Console.WriteLine("Введіть порядковий номер студента\n");
                            int num = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введіть порядковий номер студента\n");
                            
                            int num1 = Convert.ToInt32(Console.ReadLine());
                            while (num == num1)
                            {
                                Console.WriteLine("Номер студентів повинні відрізнятись\n"); num1 = Convert.ToInt32(Console.ReadLine()); }
                            double [] compared = array[num].Compare(array[num1]);
                            for (int i = 0; i < compared.Length; i++)
                                Console.Write(compared[i].ToString() + ' ');
                            Console.WriteLine();
                        }
                        break;
                    case "6":
                        {
                            if (n == 0)
                            {
                                Console.WriteLine("Немає жодного студента"); break;
                            }
                            Console.WriteLine("Введіть порядковий номер студента\n");
                            int num = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                            for (int i = 0; i < 10; i++)
                                Console.Write(array[num].GetMark(i).ToString() + ' ');
                            Console.WriteLine("Введіть порядковий номер оцінки\n");
                            int marknum = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введіть оцінку\n");
                            int changed = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                            Console.WriteLine("До\n");
                            for (int i = 0; i < 10; i++)
                                Console.Write(array[num].GetMark(i).ToString() + ' ');
                            Console.WriteLine();
                            array[num].SetMark(marknum, changed);
                            Console.WriteLine("Після\n");
                            for (int i = 0; i < 10; i++)
                                Console.Write(array[num].GetMark(i).ToString() + ' ');
                            Console.WriteLine();
                        }
                        break;
                    case "0":
                        {
                            Environment.Exit(0);
                        }
                        break;

                    default: break;
                }
            }
        }


    }
}