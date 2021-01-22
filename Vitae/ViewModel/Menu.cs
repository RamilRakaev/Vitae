using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace Vitae
{
    public partial class MainWindow : Window
    {
        //List<ContextMenu> conMenuList = new List<ContextMenu>();
        List<string> images_paths = new List<string>();
        Image selectImg;
        private void Import_Image(object sender, RoutedEventArgs e)
        {
            if (TreeElements.SelectedElement != null)
            {
                //проверка на наличие дирректорий
                string path = Environment.CurrentDirectory + @"\Images";
                Directory.CreateDirectory(path);
                path += @"\" + TreeElements.SelectedElement.Interior;
                Directory.CreateDirectory(path);
                //Создание диалогового окна
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = true;
                fileDialog.Filter = ".jpg|*.jpg|.png|*.png|All Files|*.*";
                fileDialog.DefaultExt = ".jpg";
                Nullable<bool> dialogOk = fileDialog.ShowDialog();
                //Событие нажатия кнопки ОК
                if (dialogOk == true)
                {
                    IEnumerable<string> files = Directory.EnumerateFiles(path);

                    for (int i = 0; i < fileDialog.FileNames.Length; i++)
                    {
                        //Копирование указанных, пользователем кнопок 
                        File.Copy(fileDialog.FileNames[i], path + @"\" + i + files.Count() +
                            fileDialog.FileNames[i].Substring(fileDialog.FileNames[i].LastIndexOf(@".")));
                    }
                }
                Load_Img();
                click_button = "new_image";
            }
        }

        private void ImgMenu(Image sender, ContextMenu conMenu)
        {
            conMenu = new ContextMenu();
            var delete = new MenuItem();
            delete.Header = "Удалить картинку";
            conMenu.Items.Add(delete);
            sender.ContextMenu = conMenu;
            delete.Click += Delete_Img;
        }


        private void Delete_Img(Object sender, RoutedEventArgs e)
        {
            if (selectImg != null)
            {

                for (int i = 0; i < Element_Images.Children.Count; i++)
                {
                    if (Element_Images.Children[i] == selectImg)
                    {
                        File.Delete(images_paths[i]);
                    }
                }
                Load_Img();
            }

        }
        private void Select_Img(Object sender, RoutedEventArgs e)
        {
            selectImg = (Image)sender;
        }
        private void Open_Book_Click(object sender, RoutedEventArgs e)
        {
            tabControl.Visibility = Visibility.Collapsed;
            Pages_Grid.Visibility = Visibility.Visible;
            Open_Book.IsEnabled = false;
            Open_Classification.IsEnabled = true;
            Load_Pages();
        }
        private void Open_Classification_Click(object sender, RoutedEventArgs e)
        {
            Names_Pages.Clear();
            Table_Contets.Children.Clear();
            tabControl.Visibility = Visibility.Visible;
            Pages_Grid.Visibility = Visibility.Collapsed;
            Open_Book.IsEnabled = true;
            Open_Classification.IsEnabled = false;
        }
        private void Save_Book_Click(object sender, RoutedEventArgs e)
        {

            //if (rich_Book.Document.Blocks != null)
            //{
            //    TabItem item;
            //    item = (TabItem)Tab_Pages.SelectedItem;
            //    int ind = Tab_Pages.Items.IndexOf(item);
            //    text[ind].IsEnabled = false;
            //    Pages[ind] = text[ind];
            FileSave.SaveXamlPackage(Environment.CurrentDirectory + @"\Pages\" + Selected_Page+".xaml", rich_Book);
            rich_Book.IsReadOnly = true;
            FileSave.Stream_Save(Names_Pages, Environment.CurrentDirectory + @"\Pages\" + "Names_Pages.bin");
        }

        private void Statistics_Menu_Click(object sender, RoutedEventArgs e)
        {
            Statistics_Window dialog_Window = new Statistics_Window();
            if (dialog_Window.ShowDialog() == true)
            {

            }
        }
    }
}