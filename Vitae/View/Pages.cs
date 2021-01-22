using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
namespace Vitae
{
    /// <summary>
    /// Книга
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Переменные и коллекции
        List<string> Names_Pages;
        static string selected_Page;
        static Button selected_Button;
        #endregion

        #region Свойства
        static string Selected_Page { get
            {
                if (selected_Page == null)
                    return "";
                else return selected_Page;
            }
            set
            {
                selected_Page = value;
            }
        }
        
        static Button Selected_Button
        {
            get
            {
                if (selected_Button == null)
                    return new Button();
                else return selected_Button;
            }
            set
            {
                selected_Button = value;
            }
        }
        #endregion

        #region Кнопки
        /// <summary>
        /// Выполняется при нажатии кнопки и выделении страницы
        /// </summary>
        /// <param name="but"></param>
        private void Select (Button but)
        {
            
            Selected_Page = but.Content.ToString();
            Selected_Button = but;
            FileSave.LoadXamlPackage(Environment.CurrentDirectory + @"\Pages\" + Selected_Page + ".xaml", rich_Book);
        }

        /// <summary>
        /// Загрузка кнопки
        /// </summary>
        /// <param name="name">Имя страницы</param>
        /// <param name="selected">Стоит ли выделять кнопку</param>
        private void Load_Button(string name, bool selected = false)
        {
            LinearGradientBrush linear = new LinearGradientBrush();
            linear.StartPoint = new Point(0.5, 0);
            linear.EndPoint = new Point(0.5, 1);
            linear.GradientStops.Add(new GradientStop(Color.FromRgb(139, 85, 188), 0.0));
            linear.GradientStops.Add(new GradientStop(Color.FromRgb(105, 62, 185), 1.0));

            Button but = new Button() { Content = name, Background = linear, Style = Save_Page.Style, Width = 180 };
            but.Click += Open_Page_Click;
            but.MouseEnter += Edit_Text_MouseEnter;
            but.MouseLeave += Edit_Text_MouseLeave;
            Table_Contets.Children.Add(but);
            if (selected)
            {
                Select(but);
            }
        }
        #endregion

        #region Форма с текстом
        /// <summary>
        /// Загрузка всех страниц
        /// </summary>
        private void Load_Pages()
        {
            FileSave.Stream_Open(ref Names_Pages, Environment.CurrentDirectory + @"\Pages\", "Names_Pages.bin");

            foreach (string name in Names_Pages)
            {
                Load_Button(name);
            }
            if (Names_Pages.Count > 0) 
            { 
            FileSave.LoadXamlPackage(Environment.CurrentDirectory + @"\" + Names_Pages[0], rich_Book);
                Selected_Page = Names_Pages[0];
            }

        }

        /// <summary>
        /// Открытие страницы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Page_Click(object sender, RoutedEventArgs e)
        {
            Button but = (Button)sender;
            Select(but);

        }

        #endregion

        #region Верхнее меню
        /// <summary>
        /// Добавить страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Page_Click(object sender, RoutedEventArgs e)
        {
            Dialog_Window dialog_Window = new Dialog_Window();
            if (dialog_Window.ShowDialog() == true)
            {
                Names_Pages.Add(dialog_Window.Name_Page.Text);
                Load_Button(dialog_Window.Name_Page.Text,true);

                rich_Book.Document.Blocks.Clear();

                FileSave.Stream_Save(Names_Pages, Environment.CurrentDirectory + @"\Pages\" + "Names_Pages.bin");
                FileSave.SaveXamlPackage(Environment.CurrentDirectory + @"\Pages\" + Selected_Page + ".xaml", rich_Book);
                FileSave.LoadXamlPackage(Environment.CurrentDirectory + @"\Pages\" + Selected_Page + ".xaml", rich_Book);
            }
        }
        
            /// <summary>
            /// Кнопка редактирования текста
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void Edit_Text_Click(object sender, RoutedEventArgs e)
            {
                rich_Book.IsReadOnly = false;
            }
        /// <summary>
        /// Кнопка удаления страницы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Page_Click(object sender, RoutedEventArgs e)
        {
            if (Selected_Page != null) 
            {
                Table_Contets.Children.RemoveAt(Names_Pages.IndexOf(Selected_Page));
            Names_Pages.Remove(Selected_Page);
            FileSave.Stream_Save(Names_Pages, Environment.CurrentDirectory + @"\Pages\" + "Names_Pages.bin");
            File.Delete(Environment.CurrentDirectory + @"\Pages\" + Selected_Page + ".xaml");
            if (Names_Pages.Count > 0)
                {
                FileSave.LoadXamlPackage(Environment.CurrentDirectory + @"\" + Names_Pages[0], rich_Book);
                Selected_Page = Names_Pages[0];
                }
                
            }
        }
        #endregion

        #region Стилизация кнопок
        /// <summary>
        /// Действие при наведении курсора на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Text_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button but = (Button)sender;
            but.Background = new SolidColorBrush( Color.FromRgb(188, 171, 237));


        }

        /// <summary>
        /// Действие при отводе курсора от кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Text_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button but = (Button)sender;
            LinearGradientBrush linear = new LinearGradientBrush();
            linear.StartPoint = new Point(0.5, 0);
            linear.EndPoint = new Point(0.5, 1);
            linear.GradientStops.Add(new GradientStop(Color.FromRgb(139, 85, 188), 0.0));
            linear.GradientStops.Add(new GradientStop(Color.FromRgb(105, 62, 185), 1.0));
            but.Background = linear;
        }
        #endregion
    }
}

