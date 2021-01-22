using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitae.Model
{
    class FilterElements
    {
        static public void FilterFavorites(ObservableCollection<TreeElements> _favorites, TreeElements newElement)
        {
            foreach (TreeElements tree in _favorites)
            {
                if (tree.Id == newElement.Id)
                { 
                    goto exit;
                }
            }
                _favorites.Add(newElement);
            
        exit:;
        }
    }
}
