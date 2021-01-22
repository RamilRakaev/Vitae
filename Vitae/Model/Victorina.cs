using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitae
{
    public class Victorina
    {
        public int ffff { get; set; }
        /// <summary>
        /// Располагает в случайном порядке amount элементов коллекции All_Elements
        /// </summary>
        /// <param name="All_Elements"></param>
        /// <param name="Sample"></param>
        /// <param name="Number_Right_Ansver"></param>
        /// <param name="Str_Right_Description"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static List<string> Selection_By_Description(ObservableCollection<TreeElements> All_Elements,
           ref List<string> Sample, ref int Number_Right_Ansver, ref string Str_Right_Description, int amount)
        {
            if (All_Elements == null)
                return null;
            if (All_Elements.Count < amount)
                return null;
            //Преобразование данных из иерархической коллекции в коллекцию имен элементов, где есть Description
            Sorting.Cleaning_Description(ref All_Elements);
            List<string> All_Elements_Str = Sorting.Turning_in_Collection(All_Elements);

            if (Sample == null)
            {
                Sample = new List<string>();
            }
            if (Sample.Count == 0)
            {
                Sample.AddRange(All_Elements_Str);
            }
            Random rand = new Random();
            int int_right = rand.Next(0, Sample.Count - 1);
            Str_Right_Description = Sorting.Description_Search(All_Elements, Sample[int_right]);

            List<string> temporary_collection = new List<string>();
            temporary_collection.AddRange(Sample);
            //проверка
            Addition(All_Elements_Str, ref temporary_collection, amount);

            List<int> rand_index = Rand_int(Near(int_right, temporary_collection.Count, amount));

            List<string> output = new List<string>();
            for (int b = 0; b < amount; b++)
            {
                output.Add(temporary_collection[rand_index[b]]);
            }
            Number_Right_Ansver = output.IndexOf(Sample[int_right]);
            return output;
        }

        /// <summary>
        /// Располагает в случайном порядке amount элементов коллекции All_Elements
        /// </summary>
        /// <param name="Sample">выборка элементов из коллекции</param>
        /// <param name="Str_Right">верный вариант</param>
        /// <param name="All_Elements">исходная коллекция, из которой берутся все элементы</param>
        /// <param name="amount">нужное количество элементов в выборке</param>
        /// <returns>массив рандомных чисел</returns>
        public static List<string> Selection(List<string> All_Elements, List<string> Sample, ref int Int_Right, ref string Str_Right, int amount)
        {
            if (All_Elements == null)
            {
                return null;
            }
            else if (All_Elements.Count < amount)
                return null;

            if (Sample.Count == 0)
                Sample.AddRange(All_Elements);
            Random rand = new Random();
            int int_right = rand.Next(0, Sample.Count - 1);
            Str_Right = Sample[int_right];
            List<string> temporary_collection = new List<string>();
            temporary_collection.AddRange(Sample);
            //проверка
            Addition(All_Elements, ref temporary_collection, amount);

            List<int> rand_index = Rand_int(Near(int_right, temporary_collection.Count, amount));

            List<string> output = new List<string>();
            for (int b = 0; b < amount; b++)
            {
                output.Add(temporary_collection[rand_index[b]]);
            }
            Int_Right = output.IndexOf(Str_Right);
            return output;
        }
        /// <summary>
        /// Располагает числа коллекции в случайном порядке
        /// </summary>
        /// <param name="sample_index">коллекция чисел</param>
        /// <returns>коллекция рандомных чисел</returns>
        private static List<int> Rand_int(List<int> sample_index)
        {
            List<int> rand_index = new List<int>();
            Random rand = new Random();
            while (sample_index.Count > 0)
            {
                int num = rand.Next(0, sample_index.Count - 1);
                rand_index.Add(sample_index[num]);
                sample_index.RemoveAt(num);
            }
            return rand_index;
        }
        /// <summary>
        /// Собирает в массив amount чисел начиная с num, если num+amount>=length, то в массив также добавляются числа начиная с 0
        /// </summary>
        /// <param name="start">индекс первого числа в списке</param>
        /// <param name="length">сколько всего чисел</param>
        /// <param name="amount">сколько чисел должно быть в списке</param>
        /// <returns>коллекция чисел</returns>
        private static List<int> Near(int start, int length, int amount)
        {
            List<int> output = new List<int>();

            for (int i = 0; i < amount; i++)
                if (start < length)
                {
                    output.Add(start++);
                }
                else
                {
                    start = 0;
                    output.Add(start++);
                }
            return output;
        }
        /// <summary>
        /// Проверка: если в коллекции осталось меньше amount элементов, метод добавляет столько сколько необходимо из исходного массива
        /// </summary>
        /// <param name="sample">коллекция</param>
        /// <param name="All_elements">исходный массив</param>
        /// <param name="int_right">индекс верного элемента</param>
        /// <param name="amount">количество элементов в списке</param>
        /// <returns></returns>
        private static void Addition(List<string> All_elements, ref List<string> sample, int amount)
        {
            int ind = All_elements.IndexOf(sample[0]);

            while (sample.Count < amount)
            {
                if (!sample.Contains(All_elements[ind]))
                {
                    sample.Add(All_elements[ind]);
                }
                ind--;
                if (ind < 0)
                    ind = All_elements.Count - 1;
            }
        }
    }
}
