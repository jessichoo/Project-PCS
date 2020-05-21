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
    /// Interaction logic for peg.xaml
    /// </summary>
    public partial class peg : Window
    {
        OracleConnection con;
        DataSet db = new DataSet();

        public peg()
        {
            InitializeComponent();
            con = MainWindow.con;
            show();
            tblPeg.IsReadOnly = true;
            tblPeg.CanUserAddRows = false;
        }
        public void show()
        {
            try
            {
                string query = "SELECT * from pegawai";
                OracleCommand cmd = new OracleCommand(query, con);
                cmd.ExecuteReader();
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                db = new DataSet();
                adapter.Fill(db);
                tblPeg.ItemsSource = db.Tables[0].DefaultView;

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

        private void tblpeg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //btnup.IsEnabled = true;
            if (tblPeg.SelectedIndex != -1)
            {
                DataRow dr = db.Tables[0].Rows[tblPeg.SelectedIndex];
                tbID.Text = dr["ID_PEGAWAI"].ToString();
                tbNama1.Text = dr["NAMA_PEGAWAI"].ToString();
                string shift = dr["SHIFT"].ToString();
                if (shift == "Pagi")
                {
                    rbPagi.IsChecked = true;
                }
                else if (shift == "Siang")
                {
                    rbSiang.IsChecked = true;
                }
                else if (shift == "Malam")
                {
                    rbMalam.IsChecked = true;
                }
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            con.Open();

            string id = tbID.Text;
            string nama = tbNama1.Text;
            string shift = "";
            if (rbPagi.IsChecked == true)
            {
                shift = "Pagi";
            }
            else if (rbSiang.IsChecked == true)
            {
                shift = "Siang";
            }
            else if (rbMalam.IsChecked == true)
            {
                shift = "Malam";
            }
           
            string q = $"insert into pegawai (ID_PEGAWAI,NAMA_PEGAWAI,SHIFT" +
                         $") values" +
                        $"('{id}','{nama}','{shift}')";
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
                string nama = tbNama1.Text;
                string shift = "";
                if (rbPagi.IsChecked == true)
                {
                    shift = "Pagi";
                }
                else if (rbSiang.IsChecked == true)
                {
                    shift = "Siang";
                }
                else if (rbMalam.IsChecked == true)
                {
                    shift = "Malam";
                }
                string update = $"UPDATE PEGAWAI SET NAMA_PEGAWAI = '{nama}'" +
                $", SHIFT ='{shift}' where ID_PEGAWAI = '{id}'";

                OracleCommand cmd = new OracleCommand(update, con);
                cmd.ExecuteNonQuery();
                con.Close();
                show();
                MessageBox.Show("Berhasil Update");
            }
            catch (Exception ex)
            {
                con.Close();
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show("Gagal");
            }
            //show();
        }

    }
}
