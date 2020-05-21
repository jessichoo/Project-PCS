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

using Oracle.DataAccess.Client;
namespace Project_PCS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static OracleConnection con;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string data = "orcl2";
            string user = "system";
            string pass = "Jessi889";
            con = new OracleConnection($"Data Source={data};User Id={user}; Password={pass}");
            try
            {
              
                con.Open();
                Window1 w = new Window1($"Data Source={data};User Id={user}; Password={pass}");
                w.Show();
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal Login");
            }
        }
    }
}
