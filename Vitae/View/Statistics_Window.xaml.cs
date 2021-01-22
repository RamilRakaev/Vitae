using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Vitae
{
    /// <summary>
    /// Логика взаимодействия для Statistics_Window.xaml
    /// </summary>
    public partial class Statistics_Window : Window
    {
        public Statistics_Window()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> game = new List<string>();
            game.Add("Угадай по описанию");
            game.Add("Картинки");
            game.Add("Слова");
            List<string> level = new List<string>();
            level.Add("Всё сразу");
            level.Add("Избранные");
            Statistics statistics = new Statistics(game, level);
            int ind = 0;
            foreach (string i in statistics.Result)
            {
                Console.WriteLine(statistics.Games_Names[ind]);
                TextBlock text = new TextBlock() { Text = i, 
                    FontFamily=new System.Windows.Media.FontFamily("Book Antiqua"), 
                    Foreground=System.Windows.Media.Brushes.White,
                    Padding=new Thickness(5),
                    FontWeight=FontWeights.Normal
                };

                TabItem item = new TabItem()
                {
                    Style=(Style)FindResource("Left_Menu"),
                    Header = statistics.Games_Names[ind],
                    Content = text
                };

                TC_Statistics.Items.Add(item);
                ind++;
                Console.WriteLine(i);
            }
        }
    }
}
