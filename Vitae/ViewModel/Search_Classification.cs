using System.Windows;

namespace Vitae
{
    public partial class MainWindow : Window
    {
        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            Sorting.Search_Element(tree, Search_Element.Text);
            treeView.ItemsSource = tree;
        }
    }
}
