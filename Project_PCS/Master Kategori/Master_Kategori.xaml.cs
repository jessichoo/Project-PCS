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
using System.Data;
namespace Project_SDP.Master_Kategori
{
    /// <summary>
    /// Interaction logic for Master_Kategori.xaml
    /// </summary>
    public partial class Master_Kategori : UserControl
    {
        OracleConnection con;
        DataSet ds;
        int ctr = 0;
        public Master_Kategori()
        {
            InitializeComponent();
            con = App.Connection;
            CallKategori();


        }
        private void CallKategori()
        {
            using (OracleDataAdapter adap = new OracleDataAdapter("SELECT * from kategori", con))
            {
                ds = new DataSet();
                adap.Fill(ds);
                viewer.ItemsSource = ds.Tables[0].DefaultView;
            }
        }

       
    }
}
