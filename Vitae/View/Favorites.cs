using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace Vitae
{
    public partial class MainWindow : Window
    {
        private string Str_Right_Description = "";
        ObservableCollection<TreeElements> All_Elem3;
        List<string> Sample_List3;
        Dictionary<TextBlock, Button> answers;
        List<ulong> Descriptions_Statistics;

        #region Обработчики событий
        private void Favorites_listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TreeElements select = favorites_listBox.SelectedItem as TreeElements;
            if (select != null)
            {
                Description_Box_Favorites.Text =  select.Description;
                Interior_Box_Favorites.Text = select.Interior;
                TreeElements.SelectedElement = select;
                FileSave.Stream_Save(Favorites, Environment.CurrentDirectory + "\\Favorites.bin");
                FileSave.Stream_Open(ref Favorites, Environment.CurrentDirectory, "Favorites.bin");
                favorites_listBox.ItemsSource = Favorites;
            }
        }

        /// <summary>
        /// Вызов странички с викториной
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gamebutton2_Click(object sender, RoutedEventArgs e)
        {
            Descriptions_Statistics = Statistics.Load_Collection("Descriptions", Choice);
            gameMenu.Visibility = Visibility.Collapsed;
            game3.Visibility = Visibility.Visible;
            Description_Victorina();
        }
        private void Back3_Click(object sender, RoutedEventArgs e)
        {
            Statistics.Save_Collection(Descriptions_Statistics);
            gameMenu.Visibility = Visibility.Visible;
            game3.Visibility = Visibility.Collapsed;
        }

        private void delete_Favorite_Click(object sender, RoutedEventArgs e)
        {
            Cleaning_Favorites(Interior_Box_Favorites.Text);
            Interior_Box_Favorites.Text = "";
            Description_Box_Favorites.Text = "";
        }

        private void Description_Victorina_Click(object sender, RoutedEventArgs e)
        {
            switch (Int_Right)
            {
                case 0:
                    Win_Description(answer1But, sender);
                    break;
                case 1:
                    Win_Description(answer2But, sender);
                    break;
                case 2:
                    Win_Description(answer3But, sender);
                    break;
                case 3:
                    Win_Description(answer4But, sender);
                    break;
                case 4:
                    Win_Description(answer5But, sender);
                    break;
                case 5:
                    Win_Description(answer6But, sender);
                    break;
            }
        }
        #endregion

        #region Методы
        private void collection_answers()
        {
            answers = new Dictionary<TextBlock,Button>();
            answers.Add(answer1, answer1But);
            answers.Add(answer2, answer2But);
            answers.Add(answer3, answer3But);
            answers.Add(answer4, answer4But);
            answers.Add(answer5, answer5But);
            answers.Add(answer6, answer6But);
        }
        

        private void Filter(ref List<string> Sample_List, ref ObservableCollection<TreeElements> All_Elem, string Sample_Path, string All_Elem_Path)
        {

            switch (Choice)
            {
                case "All_RB":
                    {
                        All_Elem3 = new ObservableCollection<TreeElements>();
                        Sorting.Collection_Objects(ref All_Elem, tree);
                    }
                    break;
                case "Favorites_RB":
                    {
                        Sorting.Get_Names(ref All_Elem, Favorites);
                    }
                    break;
            }
            FileSave.Stream_Open(ref Sample_List, Environment.CurrentDirectory + @"\" + Choice, Sample_Path + ".bin");
        }

        private void Win_Description(Button but, object sender)
        {
            if (but == sender)
            {
                Sample_List3.Remove(Str_Right_Description);
                Save_Samples("Sample3",Sample_List3);
                if (Sample_List3.Count == 0)
                {
                    Descriptions_Statistics[2]++;
                    Message3.Content = "Проверка пройдена!";
                    Left3.Content = "Осталось 0";
                }
                else
                {
                    Descriptions_Statistics[0]++;
                    Message3.Content = "Верно";
                    Description_Victorina();
                }
            }
            else
            {
                Descriptions_Statistics[1]++;
                Message3.Content = "Неверно";
                Description_Victorina();
            }
        }

        private void Description_Victorina()
        {
            //All_Elem3 = new ObservableCollection<TreeElements>();

            //Sorting.Collection_Objects(ref All_Elem3, tree);
            Filter(ref Sample_List3, ref All_Elem3, "Sample3", "All3");
            List<string> temporary_collection;
            temporary_collection = Victorina.Selection_By_Description(All_Elem3, ref Sample_List3, ref Int_Right, ref Str_Right_Description, 6);
            if (All_Elem3.Count >= 6 && temporary_collection != null)
            {

                if (temporary_collection.Count >= 6)
                {
                    collection_answers();
                    int i = 0;
                    foreach (TextBlock text in answers.Keys)
                    {
                        text.Text = temporary_collection[i++];
                    }
                    Description_TextBlock.Text = Str_Right_Description;
                    Str_Right_Description = temporary_collection[Int_Right];
                    Left3.Content = "Осталось " + Sample_List3.Count;
                }
            }
            else Message3.Content = "Не хватает элементов";
        }
        /// <summary>
        /// Очищает коллекции строк от названия, указанного элемента
        /// </summary>
        /// <param name="_element"></param>
        /// <param name="Sample"></param>
        private static void Cleaning_Samples(TreeElements _element, ref List<string> _sample1, ref List<string> _sample2, ref List<string> _sample3)
        {
            if (_sample1 != null)
                if (_sample1.Contains(_element.Interior))
                {
                    _sample1.Remove(_element.Interior);
                }

            if (_sample2 != null)
                if (_sample2.Contains(_element.Interior))
                {
                    _sample2.Remove(_element.Interior);
                }

            if (_sample3 != null)
                if (_sample3.Contains(_element.Interior))
                {
                    _sample3.Remove(_element.Interior);
                }
        }

        /// <summary>
        /// Удаляет элемент  из избранных
        /// </summary>
        /// <param name="_interior">имя элемента</param>
        private static void Cleaning_Favorites(string _interior)
        {
            if (Favorites != null)
            {
                int count = Favorites.Count;
                for (int i = 0; i < count; i++)
                {
                    if (Favorites[i].Interior == _interior)
                    {
                        Favorites.Remove(Favorites[i]);
                        break;
                    }
                }
            }
        }
        #endregion
    }
}