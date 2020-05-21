using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Project_PCS
{
    /// <summary>
    /// Interaction logic for cust.xaml
    /// </summary>
    public partial class cust : Window
    {
        OracleConnection con;
        DataSet db = new DataSet();
        public cust()
        {
            InitializeComponent();
            con = MainWindow.con;
            show();
            tblCust.IsReadOnly = true;
            tblCust.CanUserAddRows = false;
        }
        public void show()
        {
            try
            {
                string query = "SELECT * from customer";
                OracleCommand cmd = new OracleCommand(query, con);
                cmd.ExecuteReader();
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                db = new DataSet();
                adapter.Fill(db);
                tblCust.ItemsSource = db.Tables[0].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                //Console.WriteLine(ex.StackTrace);
                //MessageBox.Show("EROR");
                //MessageBox.Show("Gagal karena " + ex.Message);
            }
        }

        private void tblCust_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //btnup.IsEnabled = true;
            if (tblCust.SelectedIndex != -1)
            {
                DataRow dr = db.Tables[0].Rows[tblCust.SelectedIndex];
                tbID.Text = dr["ID_CUSTOMER"].ToString();
                tbNama1.Text = dr["NAMA_CUSTOMER"].ToString();
                tbAlamat.Text = dr["ALAMAT_CUSTOMER"].ToString();
                tbPoin.Text = dr["POIN"].ToString();
                string stat = dr["AKTIF"].ToString();
                if (stat == "0")
                {
                    cbaktif.IsChecked = false;
                }
                else
                {
                    cbaktif.IsChecked = true;
                }

                string jenis = dr["JK"].ToString();

                if (jenis == "L")
                {
                    rbL.IsChecked = true;
                }
                else if (jenis == "P")
                {
                    rbP.IsChecked = true;
                }
            }
        }
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            con.Open();

            string id = tbID.Text;
            string jenis = "";
            if (rbL.IsChecked == true)
            {
                jenis = "L";
            }
            else if (rbP.IsChecked == true)
            {
                jenis = "P";
            }
            string nama = tbNama1.Text;
            string alamat = tbAlamat.Text;
            int poin = Convert.ToInt32(tbPoin.Text);
            int stat = 0;
            if (cbaktif.IsChecked == false)
            {
                stat = 0;
            }
            else if(cbaktif.IsChecked == true)
            {
                stat = 1;
            }
            string q = $"insert into customer (ID_CUSTOMER,NAMA_CUSTOMER,JK," +
                         $"ALAMAT_CUSTOMER,POIN,AKTIF) values" +
                        $"('{id}','{jenis}','{nama}','{jenis}','{alamat}',{poin},{stat})";
            OracleCommand cmd = new OracleCommand(q, con);
            cmd.ExecuteNonQuery();
            show();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                string id = tbID.Text;
                string jenis = "";
                if (rbL.IsChecked == true)
                {
                    jenis = "L";
                }
                else if (rbP.IsChecked == true)
                {
                    jenis = "P";
                }
                string nama = tbNama1.Text;
                string alamat = tbAlamat.Text;
                int poin = Convert.ToInt32(tbPoin.Text);
                int stat = 0;
                if (cbaktif.IsChecked == false)
                {
                    stat = 0;
                }
                else if (cbaktif.IsChecked == true)
                {
                    stat = 1;
                }
                string update = $"UPDATE customer SET NAMA_CUSTOMER = '{nama}'" +
                $", ALAMAT_CUSTOMER ='{alamat}' , JK = '{jenis}', POIN = {poin}, AKTIF = {stat} where id_customer = '{id}'";

                OracleCommand cmd = new OracleCommand(update, con);
                cmd.ExecuteNonQuery();


                con.Close();
                MessageBox.Show("Berhasil Update");
            }
            catch (Exception ex)
            {
                con.Close();
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show("Gagal");
            }
            show();
        }

    }
}
