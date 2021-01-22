using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Vitae.Model;

namespace Vitae
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static ObservableCollection<TreeElements> tree;
        static ObservableCollection<TreeElements> Favorites;
        string click_button;
        Arrangement arrangement;
        public MainWindow()
        {
            InitializeComponent();
            arrangement = new Arrangement();
            answer2.Text = "11111111111111111111111111111111111111111111111111111111111111111111111111111";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FileSave.Stream_Open(ref tree, Environment.CurrentDirectory, "text.txt");
            treeView.ItemsSource = tree;
            int Increment = 0;
            FileSave.Stream_Open(ref Increment, Environment.CurrentDirectory + "\\Index.bin");
            TreeElements.Increment = Increment;
            //FileSave.Stream_Open(ref Sample_List1, Environment.CurrentDirectory,  "Sample1.bin");
            //FileSave.Stream_Open(ref Sample_List2, Environment.CurrentDirectory, "Sample2.bin");
            //FileSave.Stream_Open(ref Sample_List3, Environment.CurrentDirectory, "Sample3.bin");
            FileSave.Stream_Open(ref Favorites, Environment.CurrentDirectory, "Favorites.bin");
            favorites_listBox.ItemsSource = Favorites;
            

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FileSave.Stream_Save(tree, Environment.CurrentDirectory + "\\text.txt");
            FileSave.Stream_Save(TreeElements.Increment, Environment.CurrentDirectory + "\\Index.bin");
            //FileSave.Stream_Save(Sample_List1, Environment.CurrentDirectory + "\\Sample1.bin");
            //FileSave.Stream_Save(Sample_List2, Environment.CurrentDirectory + "\\Sample2.bin");
            //FileSave.Stream_Save(Sample_List3, Environment.CurrentDirectory + "\\Sample3.bin");
            FileSave.Stream_Save(Favorites, Environment.CurrentDirectory + "\\Favorites.bin");

        }

        private void add_child_Click(object sender, RoutedEventArgs e)
        {
            if (TreeElements.SelectedElement != null)
            {
                click_button = "add_child";
                alerts.Content = "Добавление элемента";
                Interior_Box.IsEnabled = true;
                Description_Box.IsEnabled = true;
                Ok.IsEnabled = true;
            }
        }

        private void add_in_list_Click(object sender, RoutedEventArgs e)
        {
            click_button = "add_in_list";
            alerts.Content = "Добавление элемента";
            Interior_Box.IsEnabled = true;
            Description_Box.IsEnabled = true;
            Ok.IsEnabled = true;
        }

        private void add_to_favorites_Click(object sender, RoutedEventArgs e)
        {
            if (TreeElements.SelectedElement != null)
            {
                TreeElements elem = new TreeElements(TreeElements.SelectedElement.Interior, 
                    TreeElements.SelectedElement.Id,
                    TreeElements.SelectedElement.Parent_Id,
                    TreeElements.SelectedElement.Description);

                FilterElements.FilterFavorites(Favorites, elem);
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            Cleaning_Samples(TreeElements.SelectedElement, ref Sample_List1, ref Sample_List2, ref Sample_List3);
            if (TreeElements.SelectedElement != null)
            {
                Cleaning_Favorites(TreeElements.SelectedElement.Interior);
                TreeElementsControl.Remove(TreeElements.SelectedElement, tree);

            }

        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            if (TreeElements.SelectedElement != null)
            {
                click_button = "edit";
                alerts.Content = "Редактирование";
                Interior_Box.IsEnabled = true;
                Description_Box.IsEnabled = true;
                Ok.IsEnabled = true;
            }
        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            click_button = "";
            alerts.Content = "";
            Description_Box.IsEnabled = false;
            Interior_Box.IsEnabled = false;
            Ok.IsEnabled = false;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (TreeElements.SelectedElement != null)
                switch (click_button)
            {
                case "edit":
                    {
                            string new_name = TreeElements.SelectedElement.Path.Substring(0, TreeElements.SelectedElement.Path.LastIndexOf(@"\") + 1) + Interior_Box.Text;
                            if (!Directory.Exists(new_name))
                                Directory.Move(TreeElements.SelectedElement.Path, new_name);
                            TreeElements.SelectedElement.Description = Description_Box.Text;
                        TreeElements.SelectedElement.Interior = Interior_Box.Text;
                            
                        }
                    break;
                case "add_child":
                    {
                        //if (TreeElements.SelectedElement == null)
                        //{
                        //    if (TreeElementsControl.Check_Interior(Interior_Box.Text, tree))
                        //    {
                        //        TreeElements new_tree = new TreeElements(Interior_Box.Text, tree, Description_Box.Text);
                        //    }
                        //    else
                        //    {
                        //        alerts.Content = "Элемент с таким именем уже существует!";
                        //    }
                        //}
                        
                            if (TreeElementsControl.Check_Interior(Interior_Box.Text, TreeElements.SelectedElement.Child))
                            {
                                TreeElements new_tree = new TreeElements(Interior_Box.Text, TreeElements.SelectedElement, Description_Box.Text);
                            }
                            else
                            {
                                alerts.Content = "Элемент с таким именем уже существует!";
                            }
                        
                    }
                    break;
                case "add_in_list":
                    {
                        
                            if (TreeElements.SelectedElement.Parent_Id == 0)
                                if (TreeElementsControl.Check_Interior(Interior_Box.Text, TreeElements.SelectedElement.Child))
                                {
                                    TreeElementsControl.Add_Element_in_Collection(TreeElements.SelectedElement, tree, Interior_Box.Text, Description_Box.Text);
                                }
                                else
                                {
                                    alerts.Content = "Элемент с таким именем уже существует!";
                                }
                            else
                            {
                                if (TreeElementsControl.Check_Interior(Interior_Box.Text, tree))
                                {
                                    TreeElementsControl.Add_Element_in_Collection(TreeElements.SelectedElement, tree, Interior_Box.Text, Description_Box.Text);
                                }
                                else
                                {
                                    alerts.Content = "Элемент с таким именем уже существует!";
                                }
                            }
                        
                    }
                    break;
            }
            click_button = "";
            Description_Box.IsEnabled = false;
            Interior_Box.IsEnabled = false;
            Ok.IsEnabled = false;
            alerts.Content = "";
            SaveTreeView();
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeElements select = treeView.SelectedItem as TreeElements;
            if (select != null)
            {
                Description_Box.Text = select.Description;
                Interior_Box.Text = select.Interior;
                TreeElements.SelectedElement = select;
                Element_Images.Children.Clear();
                if (Sorting.Picture_Avialability(TreeElements.SelectedElement.Interior))
                    Load_Img();
            }
        }

        /// <summary>
        /// Прогрузка картинок элемента
        /// </summary>
        private void Load_Img()
        {
            Element_Images.Children.Clear();
            images_paths.Clear();
            DirectoryInfo info = new DirectoryInfo(TreeElements.SelectedElement.Path);
            IEnumerable<FileInfo> directories = info.EnumerateFiles();
            Thickness margin = new Thickness(5);

            foreach (FileInfo file_path in directories)
            {
                images_paths.Add(file_path.FullName);
                using (var file = new FileStream(file_path.FullName, FileMode.Open, FileAccess.Read, FileShare.Inheritable))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = file;

                    arrangement.Image_Height = Classification_Grid.RowDefinitions[2].ActualHeight + Classification_Grid.RowDefinitions[3].ActualHeight + Classification_Grid.RowDefinitions[4].ActualHeight;
                    Image image = new Image()
                    {
                        Margin = margin,
                        Source = bitmap,
                        Height= arrangement.Image_Height
                    };
                    //image.SetBinding(DependencyProperty.Register("Height", typeof(double), typeof(Grid)), bind);
                    Element_Images.Children.Add(image);
                    bitmap.EndInit();
                    bitmap.Freeze();
                    
                }

            }

            foreach (Image img in Element_Images.Children)
            {
                ContextMenu menu = new ContextMenu();
                //conMenuList.Add(menu);
                ImgMenu(img, menu);
                img.MouseDown += Select_Img;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveTreeView();
        }

        void SaveTreeView()
        {
            FileSave.Stream_Save(tree, Environment.CurrentDirectory + "\\text.txt");
            FileSave.Stream_Open(ref tree, Environment.CurrentDirectory , "text.txt");
            treeView.ItemsSource = tree;
        }

    }



}