using System;
using System.Collections.Generic;
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

namespace HashtilCagesMap
{
    /// <summary>
    /// Interaction logic for SelectBox.xaml
    /// </summary>
    public partial class SelectBox : Window
    {
        public SelectBox()
        {
            InitializeComponent();
        }

       

        public void btnOK(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public void btnCANCEL(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
