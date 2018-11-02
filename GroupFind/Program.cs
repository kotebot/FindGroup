using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupFind
{
    class Program
    {
        
        const string Path = @"E:\input.txt";
        const string Path_ = @"E:\output.txt";
        //random
        static Random random = new Random();

        static void Main(string[] args)
        {
            //max размер массива
            const int maxSize = 500000000;//500000000

            int ch = 0;
            Console.WriteLine("1-Добавить.\n2-считать");
            ch = Int32.Parse(Console.ReadLine());
            switch (ch)
            {
                case 1:
                    //инициализация исходного массива
                    int[] array = new int[random.Next(10, maxSize)];
                    //Console.WriteLine(array.Length);//вывод кол-во элементов массива
                    string Array = "";
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = random.Next(1, 10000);//10000
                        Array += array[i] + " ";
                    }
                    SaveFile(Array, false);
                    Array = "";


                    //инициализация массива для поиска
                    int[] arrayFind = new int[random.Next(2, 4)];
                    Console.WriteLine(arrayFind.Length);//вывод кол-во элементов массива
                    for (int i = 0; i < arrayFind.Length; i++)
                    {
                        arrayFind[i] = random.Next(1, 10000);//10000
                        Array += arrayFind[i] + " ";
                    }
                    SaveFile(Array, true);
                    break;
                case 2:
                    //максимальный размер группы
                    int sizeGroup = random.Next(1, 10);

                    SaveFile(Find(stringToArrayInt("1 5 5 5 5 5 5 5 5 5 8 1 1 1 1 5 1 5 5 5 5 5 1 1 2 2 2 5 2 2 2 8 6 6 6 6 6 5"), stringToArrayInt("5 5 8"),7));
                    break;
                default:
                    Console.WriteLine("Не правильно введено.");
                    break;
            }
            

            Console.ReadKey();
        }

        //запись в файл
        static void SaveFile(string text,bool plusText)
        {         
            using (StreamWriter sw = new StreamWriter(Path, plusText, Encoding.Default))
            {
                sw.WriteLine(text);
            }
        }

        static void SaveFile(Node[] nodes)
        {
            using (StreamWriter sw = new StreamWriter(Path_, false, Encoding.Default))
            {
                for(int i=0; i<nodes.Length;i++)
                    sw.WriteLine(nodes[i].Show());
            }
        }

        //cчитывание с файла
        static string LoadFile(int numberString)
        {
            using (StreamReader sr = new StreamReader(Path, Encoding.Default))
            {
                string line;
                int count=0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (count == numberString)
                        return line;
                    count++;
                }
            }
            return "";
        }

        //строка в массив int
        static int[] stringToArrayInt(string text)
        {
            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] array = new int[words.Length];
            for (int i = 0; i < array.Length; i++)
                array[i] = Int32.Parse(words[i]);
            return array;
        }
        
        class Node
        {
            public int data;
            public int index;

            public Node(int data, int index)
            {
                this.data = data;
                this.index = index;
            }

            public string Show()
            {
                return data + " " + index;
            }

        }

        //алгоритм посика
        static Node []Find(int[] sourceArray, int[] findArray, int sizeGroup)
        {
            List<Node> nodes = new List<Node>();//список записей
            List<int> find = new List<int>();
            bool[] checkArray = new bool[findArray.Length];//массив для проверки
            
            for(int i=0;i<sourceArray.Length; i++)//проходим по исхдном массив
            {
                
                for(int j=0;j<findArray.Length; j++)//по массив для поиска
                {
                    
                    if (sourceArray[i]==findArray[j])//сравниваем
                    {
                        find = findArray.ToList();
                        if (i == 5)
                        {
                            Console.Write("");
                        }
                        //если они равны то мы отбрасем за 8 элементом, с словием что это больше 0
                        int count = 0;
                        int temp = 0;
                        
                        int k = i - sizeGroup;
                        if (k < 0)
                            k = 0;
                        for(;k<i+sizeGroup && k<sourceArray.Length && k < temp + sizeGroup; k++)//идем по максимм
                        {
                            
                            bool start = true;
                            for (int l=0;l<find.Count;l++)//каждый элемент проверяем
                            {
                                if(sourceArray[k] == find[l])//если они равны то отмечаем это
                                {
                                    

                                    if(start)
                                    {
                                        temp = k;
                                        start = false;
                                    }
                                    if (checkArray.Length == count)
                                        break;
                                    checkArray[count] = true;
                                    find.RemoveAt(l);
                                    count++;
                                    
                                    
                                }
                                
                            }
                            if (allArray(checkArray))
                                break;
                        }
                        if (allArray(checkArray))//если весь массив truе, тогда добавляем его в список
                            nodes.Add(new Node(sourceArray[i], i));
                        arrayFalse(checkArray, false);//обнляем массив
                    }
                }
            }
            return nodes.ToArray();
        }

        //весь масив одним значением
        static bool[] arrayFalse(bool []array, bool TrueOrFalse)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = TrueOrFalse;
            return array;
        }

        static bool allArray(bool []array)
        {
            for (int i = 0; i < array.Length; i++)
                if (!array[i])
                    return false;
            return true;
        }
    }
}
