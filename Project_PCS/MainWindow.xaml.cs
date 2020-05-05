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
namespace Project_SDP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OracleConnection con;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string data = tb_datasource.Text;
            string user = tb_username.Text;
            string pass = tb_pass.Text;
            con = new OracleConnection($"Data Source={data};User Id={user}; Password={pass}");
            try
            {
                App.Connection = con;
                con.Open();
                Window1 w = new Window1();
                w.Show();
                this.Hide();
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal Login");
            }
        }
    }
}
