using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitae
{
    public class TreeElementsControl
    {
        /// <summary>
        /// Находит родительский элемент, если он есть
        /// </summary>
        /// <param name="_element"></param>
        /// <param name="_parent_element"></param>
        /// <returns></returns>
        private static TreeElements Search_Parent(TreeElements _element, TreeElements _parent_element)
        {
            for (int i = 0; i < _parent_element.Child.Count; i++)
            {
                if (_parent_element.Child[i] == _element)
                    return _parent_element;
                else
                {
                    if (_parent_element.Child[i].Child.Count > 0)
                    {
                        TreeElements new_element = Search_Parent(_element, _parent_element.Child[i]);
                        if (new_element != null)
                            return new_element;
                    }

                }
            }
            return null;
        }

        /// <summary>
        /// Находит коллекцию, в которой хранится элемент
        /// </summary>
        /// <param name="_element"></param>
        /// <param name="_all_elements"></param>
        /// <returns></returns>
        private static bool Search_Collection(TreeElements _element, ObservableCollection<TreeElements> _all_elements)
        {
            for (int i = 0; i < _all_elements.Count; i++)
            {
                if (_all_elements[i] == _element)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Добавляет новый элемент в ту же коллекцию, в которой находится исходный элемент
        /// </summary>
        /// <param name="_element"></param>
        /// <param name="_all_elements"></param>
        /// <param name="_interior"></param>
        /// <param name="_description"></param>
        public static void Add_Element_in_Collection(TreeElements _element, ObservableCollection<TreeElements> _all_elements, string _interior, string _description = null)
        {
            if (Search_Collection(_element, _all_elements))
            {
                TreeElements new_element = new TreeElements(_interior, _all_elements, _description);
            }
            else
            {
                for (int i = 0; i < _all_elements.Count; i++)
                {
                    TreeElements elem = Search_Parent(_element, _all_elements[i]);
                    if (elem != null)
                    {
                        TreeElements new_element = new TreeElements(_interior, elem, _description);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Удаляет элемент и папку с картинками
        /// </summary>
        /// <param name="_element"></param>
        /// <param name="_all_elements"></param>
        public static void Remove(TreeElements _element, ObservableCollection<TreeElements> _all_elements)
        {
            if (Search_Collection(_element, _all_elements))
            {
                if (Directory.Exists(_element.Path))
                    Directory.Delete(_element.Path, true);
                _all_elements.RemoveAt(_all_elements.IndexOf(_element));

            }
            else
            {
                for (int i = 0; i < _all_elements.Count; i++)
                {
                    TreeElements parent_elem = Search_Parent(_element, _all_elements[i]);
                    if (parent_elem != null)
                    {
                        if (Directory.Exists(_element.Path))
                            Directory.Delete(_element.Path);
                        parent_elem.Child.RemoveAt(parent_elem.Child.IndexOf(_element));
                        break;
                    }
                }
            }
        }


        /// <summary>
        /// Проверяет есть ли в коллекции элелементы с похожим названием
        /// </summary>
        /// <param name="_interior"></param>
        /// <param name="_collection"></param>
        /// <returns></returns>
        public static bool Check_Interior(string _interior, ObservableCollection<TreeElements> _collection)
        {
            foreach (TreeElements element in _collection)
            {
                if (_interior == element.Interior)
                    return false;
            }
            return true;
        }
        ///// <summary>
        ///// Проверяет есть у элемента картинки в папке Images
        ///// </summary>
        ///// <param name="names"></param>
        //public static void Cleaning(ref List <string> names)
        //{
        //    int count = names.Count;
        //    for (int i = 0; i < count; i++)
        //    {
        //        if (Directory.EnumerateFiles(Environment.CurrentDirectory + @"\Images\" + names[i]).Count<string>() == 0)
        //        {
        //            count--;
        //            names.Remove(names[i]);
        //        }
        //    }
        //}
    }
}
