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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kazychanov_Autoservice
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();

            var currentServices = KazychanovAutoServiceEntities.GetContext().Service.ToList();

            ServiceListView.ItemsSource = currentServices;
        }



        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
