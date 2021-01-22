using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitae
{
    [Serializable]
    public class TreeElements : INotifyPropertyChanged
    {
        #region Constructors
        public TreeElements() { }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_interior">Название элемента</param>
        /// <param name="Parent">Родительский элемент</param>
        /// <param name="_description">Описание элемента</param>
        /// <param name="_childs">Дочерние элементы</param>
        public TreeElements(string _interior, TreeElements Parent, string _description = null, ObservableCollection<TreeElements> _childs = null)
        {
            Interior = _interior;
            Description = _description;
            Parent_Id = Parent.Id;
            if (_childs == null)
                Child = new ObservableCollection<TreeElements>();
            else
                Child = _childs;
            Id = Increment++;
            Parent.Child.Add(this);

            Establish_Path();
        }

        /// <summary>
        /// Конструктор на случай, если родителей нету
        /// </summary>
        /// <param name="_interior">Название элемента</param>
        /// <param name="Collection">Коллекция, к которой присваивается переменная</param>
        /// <param name="_description">Описание элемента</param>
        /// <param name="_childs">Дочерние элементы</param>
        public TreeElements(string _interior, ObservableCollection<TreeElements> Collection, string _description = null, ObservableCollection<TreeElements> _childs = null)
        {
            Interior = _interior;
            Description = _description;
            if (_childs == null)
                Child = new ObservableCollection<TreeElements>();
            else
                Child = _childs;
            Id = Increment++;
            Collection.Add(this);

            Establish_Path();
        }

        /// <summary>
        /// Конструктор созданющий неупорядоченные экземпляры, для последующих преобразований
        /// </summary>
        /// <param name="_interior">Название элемента</param>
        /// <param name="_parent_number">Уникальный номер родителя</param>
        /// <param name="_unique_number">Уникальный номер</param>
        /// <param name="_description">Описание элемента</param>
        public TreeElements(string _interior, int _unique_number, int _parent_number = 0, string _description = null,
            bool _is_selected = false, bool _is_expanded = false, string _path = null)
        {
            Interior = _interior;
            Description = _description;
            Parent_Id = _parent_number;
            Id = _unique_number;
            Description = _description;
            Child = new ObservableCollection<TreeElements>();
            //Path = _path;
            IsSelected = _is_selected;
            IsExpanded = _is_expanded;
        }
        #endregion

        #region Fields
        private string interior;
        private string description;
        private bool is_selected;
        private bool is_expanded;
        public int parent_number = 0;
        public int unique_number;
        private static int increment;
        //private string path;
        public static TreeElements SelectedElement;
        #endregion

        #region Properties
        public string Interior
        {
            get
            {
                if (interior != null) return interior;
                else return "Organism";
            }
            set
            {
                if (value != null)
                    interior = value;
            }
        }

        public string Description
        {
            get
            {
                if (description != null) return description;
                else return "Is empty";
            }
            set
            {
                if (value != null)
                    description = value;
            }
        }

        public bool IsSelected
        {
            get { return is_selected; }
            set
            {
                if (value != is_selected)
                {
                    is_selected = value;
                    NotifyPropertyChandged("IsSelected");
                }
            }
        }

        public bool IsExpanded
        {
            get { return is_expanded; }
            set
            {
                 is_expanded = value;
                    NotifyPropertyChandged("IsExpanded");
                
            }
        }

        public int Parent_Id
        {
            get { return parent_number; }
            set
            {
                if (value != 0)
                    parent_number = value;
            }
        }
        public int Id
        {
            get { return unique_number; }
            set
            {
                if (value != 0)
                    unique_number = value;
            }
        }

        public static int Increment
        {
            get
            { return increment; }
            set
            {
                if (value != 0)
                    increment = value;
            }
        }
        public string Path
        {
            get
            {
                //if (path != null) return path;
                //else
                //{
                    //string new_path = Environment.CurrentDirectory + @"\Images\" + this.Interior;
                    return Environment.CurrentDirectory + @"\Images\" + this.Interior;
                //}
            }
            //set
            //{
            //    if (value != null)
            //        path = value;
            //}
        }

        #endregion

        #region Collections
        public ObservableCollection<TreeElements> Child { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Устанавливает уникальный номер элемента, по которому можно определить его положение в иерархии
        /// </summary>
        /// <param name="all_elem"></param>
        /// <param name="parent_number"></param>
        /// <returns></returns>
        static string Establish_Value(ObservableCollection<TreeElements> all_elem, string parent_number = null)
        {
            string count = Convert.ToString(all_elem.Count);
            if (parent_number == null)
                return count;
            else
            {
                return parent_number + "#" + count;
            }
        }

        public void Establish_Path()
        {
            if (!Directory.Exists(Environment.CurrentDirectory + @"\Images"))
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Images");

            if (!Directory.Exists(this.Path))
            {
                Directory.CreateDirectory(this.Path);
            }
            else
            {
                int number = 1;
                do
                {
                    number++;
                }
                while (Directory.Exists(this.Path + number));

                Interior = Interior + number;
                Directory.CreateDirectory(this.Path);
            }

        }

        public void Add_Image()
        {

        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChandged(string prop_name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop_name));
        }
        #endregion
    }
}
