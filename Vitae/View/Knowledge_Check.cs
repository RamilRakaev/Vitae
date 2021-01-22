using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace Vitae
{
    public partial class MainWindow : Window
    {
        //Общие аргументы
        private string Str_Right = "";
        private int Int_Right;
        private List<string> Names_Samples;
        private string Choice;

        //для первой игры
        List<string> All_elem1;
        List<string> Sample_List1;
        List<Image> variants;
        List<ulong> Image_Statistics;

        //для второй игры
        List<string> All_elem2;
        List<string> Sample_List2;
        Dictionary<TextBlock, Button> words;
        List<ulong> Words_Statistics;

        #region Обработка информации
        /// <summary>
        /// Заполнение коллекции картинками
        /// </summary>
        private void collection_images()
        {
            variants = new List<Image>();

            variants.Add(variant1);
            variants.Add(variant2);
            variants.Add(variant3);
            variants.Add(variant4);
            variants.Add(variant5);
            variants.Add(variant6);
        }

        /// <summary>
        /// Заполнение коллекции кнопками
        /// </summary>
        private void collection_words()
        {
            words = new Dictionary<TextBlock, Button>();

            words.Add(word1, word1But);
            words.Add(word2, word2But);
            words.Add(word3, word3But);
            words.Add(word4, word4But);
            words.Add(word5, word5But);
            words.Add(word6, word6But);
        }

        /// <summary>
        /// Определяет диапазон выборки значений
        /// </summary>
        /// <param name="Sample_List"></param>
        /// <param name="All_Elem"></param>
        /// <param name="Sample_Path"></param>
        /// <param name="All_Elem_Path"></param>
        private void Filter(ref List<string> Sample_List, ref List<string> All_Elem, string Sample_Path, string All_Elem_Path)
        {
            
                switch (Choice)
                {
                    case "All_RB":
                        {
                            All_Elem = Sorting.Turning_in_Ierarchy(tree);
                        }
                        break;
                    case "Favorites_RB":
                        {
                            All_Elem = Sorting.Get_Names(Favorites);
                        }
                        break;
                } 
            
            FileSave.Stream_Open(ref Sample_List, Environment.CurrentDirectory + @"\" + Choice, Sample_Path + ".bin");
        }

        
        /// <summary>
        /// Сохранение выборки
        /// </summary>
        /// <param name="game"></param>
        private void Save_Samples(string game,List <string> Sample_List)
        {
            FileSave.Stream_Save(Sample_List, Environment.CurrentDirectory +@"\"+ Choice + @"\" + game + ".bin");
        }
        #endregion

        #region Обработчики событий 
        /// <summary>
        /// Выбрать диапазон значений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void All_RB_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            Choice = (string)radio.Name;
            
        }

        /// <summary>
        /// Вызов странички с викториной по картинкам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gamebutton_Click(object sender, RoutedEventArgs e)
        {
            gameMenu.Visibility = Visibility.Collapsed;
            game1.Visibility = Visibility.Visible;

            Image_Statistics = Statistics.Load_Collection("Image",Choice);
            Image_Victorina();

        }
        /// <summary>
        /// Вызов странички с проверкой по словам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gamebutton1_Click(object sender, RoutedEventArgs e)
        {
            gameMenu.Visibility = Visibility.Collapsed;
            game2.Visibility = Visibility.Visible;

            Words_Statistics = Statistics.Load_Collection("Words", Choice);
            Word_Victorina();

        }

        private void Back1_Click(object sender, RoutedEventArgs e)
        {
            Statistics.Save_Collection(Image_Statistics);
            gameMenu.Visibility = Visibility.Visible;
            game1.Visibility = Visibility.Collapsed;
        }

        private void Back2_Click(object sender, RoutedEventArgs e)
        {
            Statistics.Save_Collection(Words_Statistics);
            gameMenu.Visibility = Visibility.Visible;
            game2.Visibility = Visibility.Collapsed;
        }
        
        /// <summary>
        /// Нажатие на одну из кнопок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_Victorina_MouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (Int_Right)
            {
                case 0:
                    Win_image(variant1, sender);
                    break;
                case 1:
                    Win_image(variant2, sender);
                    break;
                case 2:
                    Win_image(variant3, sender);
                    break;
                case 3:
                    Win_image(variant4, sender);
                    break;
                case 4:
                    Win_image(variant5, sender);
                    break;
                case 5:
                    Win_image(variant6, sender);
                    break;
            }

        }

        /// <summary>
        /// Событие нажатия на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Word_Victorina_Click(object sender, RoutedEventArgs e)
        {
            switch (Int_Right)
            {
                case 0:
                    Win_word(word1But, sender);
                    break;
                case 1:
                    Win_word(word2But, sender);
                    break;
                case 2:
                    Win_word(word3But, sender);
                    break;
                case 3:
                    Win_word(word4But, sender);
                    break;
                case 4:
                    Win_word(word5But, sender);
                    break;
                case 5:
                    Win_word(word6But, sender);
                    break;
            }
        }

        private void gamebutton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button but =(Button) sender;
            but.BorderThickness = new Thickness(4);
        }
        #endregion

        #region Вывод информации на экран
        /// <summary>
        /// Проверка на верность
        /// </summary>
        /// <param name="but"></param>
        /// <param name="sender"></param>
        private void Win_image(Image img, object sender)
        {
            if (img == sender)
            {
                Sample_List1.Remove(Str_Right);
                Save_Samples("Sample1", Sample_List1);
                if (Sample_List1.Count == 0)
                {
                    Image_Statistics[2]++;
                    Message1.Content = "Проверка пройдена!";
                    Left1.Content = "Осталось 0";

                }
                else
                {
                    Image_Statistics[0]++;
                    Message1.Content = "Верно";
                    Image_Victorina();
                }

            }
            else
            {
                Image_Statistics[1]++;
                Message1.Content = "Неверно";
                Image_Victorina();

            }
        }

        private void Image_Victorina()
        {
            Filter(ref Sample_List1, ref All_elem1, "Sample1", "All1");
            Sorting.Cleaning(ref All_elem1);
            if (All_elem1.Count >= 6)
            {
                List<string> temporary_collection;
                temporary_collection = Victorina.Selection(All_elem1, Sample_List1, ref Int_Right, ref Str_Right, 6);
                if (temporary_collection.Count != 0)
                {
                    collection_images();
                    for (int i = 0; i < variants.Count; i++)
                    {
                        Load_Img(variants[i], temporary_collection[i]);
                        Left1.Content = "Осталось " + Sample_List1.Count;
                    }
                    Right_label.Content = Str_Right;
                }
            }
            else 
            {
                Left1.Content = "Осталось 0";
                Message1.Content = "Не хватает картинок"; 

            }
        }


        private void Word_Victorina()
        {
            All_elem2 = Sorting.Turning_in_Ierarchy(tree);
            Filter(ref Sample_List2, ref All_elem2, "Sample2", "All2");
            Sorting.Cleaning(ref All_elem2);
            if (All_elem2.Count >= 6)
            {
                List<string> temporary_collection;
                temporary_collection = Victorina.Selection(All_elem2, Sample_List2, ref Int_Right, ref Str_Right, 6);
                if (temporary_collection.Count != 0)
                {
                    collection_words();
                    int i = 0;
                    foreach (TextBlock text in words.Keys)
                    {
                        text.Text = temporary_collection[i++];
                    }
                    Load_Img(being,Str_Right);
                    Left2.Content = "Осталось " + Sample_List2.Count;
                }
            }
            else Message2.Content = "Нет картинок";
        }

        /// <summary>
        /// Проверка на верность
        /// </summary>
        /// <param name="but"></param>
        /// <param name="sender"></param>
        private void Win_word(System.Windows.Controls.Button but, object sender)
        {
            if (but == sender)
            {
                Sample_List2.Remove(Str_Right);
                Save_Samples("Sample2", Sample_List2);
                if (Sample_List2.Count == 0)
                {
                    Words_Statistics[2]++;
                    Message2.Content = "Проверка пройдена!";
                    Left2.Content = "Осталось 0";
                }
                else
                {
                    Words_Statistics[0]++;
                    Message2.Content = "Верно";
                    Word_Victorina();
                }
            }
            else
            {
                Words_Statistics[1]++;
                Message2.Content = "Неверно";
                Word_Victorina();
            }
        }

        private void Load_Img(Image being,string file_name)
        {
            string path = (Directory.EnumerateFiles(Environment.CurrentDirectory + @"\Images\" + file_name)).First<string>();
            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Inheritable))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = file;
                bitmap.EndInit();
                being.Source = bitmap;

            }
        }
        #endregion
    }
}
