using System.Linq;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;

namespace Vitae
{
    class Sorting
    {
        //List<TreeElements> Turn(List<TreeElements> items)
        //{

        //}
        /// <summary>
        /// Восстанавливает иерархию по уникальным номерам
        /// </summary>
        /// <param name="items">Исходная коллекция элементов</param>
        /// <param name="result">Упорядоченная по уникальным номерам иерархия элементов</param>
        /// <returns></returns>
        public static void Normalize(ObservableCollection<TreeElements> items, ObservableCollection<TreeElements> result)
        {
            for (int i = 0; i < items.Count;)
            {
                if (items[i].Parent_Id == 0)
                {
                    result.Add(items[i]);
                    items.Remove(items[i]);

                }
                else
                    i++;
            }
            Normalize_in_Depth(items, result);
        }

        static void Normalize_in_Depth(ObservableCollection<TreeElements> items, ObservableCollection<TreeElements> result)
        {
            for (int j = 0; j < result.Count; j++)
                for (int i = 0; i < items.Count;)
                {
                    if (items[i].Parent_Id == result[j].Id)
                    {
                        result[j].Child.Add(items[i]);
                        items.Remove(items[i]);
                    }
                    else
                        i++;
                }
            if (items.Count != 0)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    Normalize_in_Depth(items, result[i].Child);
                }
            }
        }
        /// <summary>
        /// Собирает названия всех вложенных объектов
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static List<string> Turning_in_Ierarchy(ObservableCollection<TreeElements> elements = null, TreeElements parent = null)
        {
            List<string> output = new List<string>();
            if (elements != null)
            {
                foreach (TreeElements x in elements)
                {
                    if (x.Child.Count > 0)
                    {
                        output.AddRange(Turning_in_Ierarchy(x.Child));
                    }
                    output.Add(x.Interior);
                }
                return output;
            }
            if (parent != null)
            {
                output.Add(parent.Interior);
                if (parent.Child != null)
                    output.AddRange(Turning_in_Ierarchy(parent.Child));
                return output;
            }
            return null;
        }

        /// <summary>
        /// Собирает названия всех объектов в списке
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static List<string> Turning_in_Collection(ObservableCollection<TreeElements> elements = null)
        {
            List<string> output = new List<string>();
            if (elements != null)
            {
                foreach (TreeElements x in elements)
                {
                    output.Add(x.Interior);
                }
                return output;
            }
            return null;
        }

        /// <summary>
        /// Возвращает имена коллекции элементов
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static List<string> Get_Names(ObservableCollection<TreeElements> elements)
        {

            List<string> Names = new List<string>();
            foreach(TreeElements tree in elements)
            {
                Names.Add(tree.Interior);
            }
            return Names;
        }
        /// <summary>
        /// Очищает список названий элементов от тех, которые не имеют картинок в папке Image
        /// </summary>
        /// <param name="names"></param>
        public static void Cleaning(ref List<string> names)
        {
            List<string> new_names = new List<string>();
            new_names.AddRange(names);
            foreach (string name in new_names)
            {
                if (!Picture_Avialability(name))
                {
                    names.Remove(name);
                }
            }
        }
        /// <summary>
        /// Если у элемента с указанным именем есть картинка выводит true
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool Picture_Avialability(string name)
        {
            Directory.CreateDirectory(Environment.CurrentDirectory + @"\Images");
            Directory.CreateDirectory(Environment.CurrentDirectory + @"\Images\" + name);
            IEnumerable <string> _new= Directory.EnumerateFiles(Environment.CurrentDirectory + @"\Images\" + name);
            int num=Convert.ToInt32(_new.Count<string>());
            if (num == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Очищает коллекцию от элементов, где отсутсвует описание
        /// </summary>
        /// <param name="names"></param>
        public static void Cleaning_Description(ref ObservableCollection<TreeElements> names)
        {
            foreach (TreeElements name in names)
            {
                if (name.Description == null)
                {
                    if (name.Description == "")
                        names.Remove(name);
                }
            }
        }
        /// <summary>
        /// Ищет нужный элемент в иерархии элементов по имени и выдает описание элемента
        /// </summary>
        /// <param name="Elements">иерархия элементов</param>
        /// <param name="Iterior">имя</param>
        /// <returns>описание</returns>
        public static string Description_Search(ObservableCollection<TreeElements> Elements, string Iterior)
        {

            if (Elements != null)
            {
                foreach (TreeElements x in Elements)
                {
                    if (x.Child.Count > 0)
                    {
                        string search = Description_Search(x.Child, Iterior);
                        if (search != "")
                            return search;
                    }
                    if (x.Interior == Iterior)
                        if (x.Description != null)
                            return x.Description;
                        else return "";
                }
            }
            return "";
        }
        /// <summary>
        /// Осуществляет поиск элемента в иерархической коллекции, по указанному имени
        /// </summary>
        /// <param name="Elements">Коллекция</param>
        /// <param name="Iterior">Имя</param>
        /// <returns></returns>
        public static void Search_Element(ObservableCollection<TreeElements> Elements, string Interior)
        {
            if (Elements != null)
            {
                foreach (TreeElements x in Elements)
                {
                    if (x.Child.Count > 0)
                    {
                        Search_Element(x.Child, Interior);

                    }
                    if (x.Interior == Interior)
                    {
                        x.IsSelected = true;
                        x.IsExpanded = true;
                    }
                }
            }
        }

        /// <summary>
        /// Собирает все вложенные объекты в один список
        /// </summary>
        /// <param name="Elements"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static void Collection_Objects(ref ObservableCollection<TreeElements> output, ObservableCollection<TreeElements> Elements = null,
            TreeElements parent = null)
        {
            if (Elements != null)
            {
                foreach (TreeElements x in Elements)
                {
                    if (x.Child.Count > 0)
                    {
                        Collection_Objects(ref output, x.Child);
                    }
                    output.Add(x);
                }
            }
            if (parent != null)
            {
                output.Add(parent);
                if (parent.Child != null)
                    if (parent.Child.Count > 0)
                        Collection_Objects(ref output, parent.Child);
            }
        }
        public static void Get_Names(ref ObservableCollection<TreeElements> output, ObservableCollection<TreeElements> Elements = null)
        {
            output = new ObservableCollection<TreeElements>();
            if (Elements != null)
            {
                foreach (TreeElements x in Elements)
                {
                    output.Add(x);
                }
            }
            
        }
        //Проверить
    }
}
