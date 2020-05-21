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
using Oracle.DataAccess.Client;
namespace Project_PCS
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        OracleConnection conn;
        public Window1(string ds)
        {
            InitializeComponent();
            conn = MainWindow.con;

        }

        private void admin_Click(object sender, RoutedEventArgs e)
        {
            admin a = new admin();
            a.Show();
            this.Close();
        }
    }
}
