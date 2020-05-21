using Oracle.DataAccess.Client;
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
using System.Data;


namespace Project_PCS
{
    /// <summary>
    /// Interaction logic for promo.xaml
    /// </summary>
    public partial class promo : Window
    {
        OracleConnection con;
        DataSet db = new DataSet();

        public promo()
        {
            InitializeComponent();
            con = MainWindow.con;
            show();
            tblPromo.IsReadOnly = true;
            tblPromo.CanUserAddRows = false;
        }
        public void show()
        {
            try
            {
                //con.Open();
                string query = "SELECT * from promo";
                OracleCommand cmd = new OracleCommand(query, con);
                cmd.ExecuteReader();
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                db = new DataSet();
                adapter.Fill(db);
                tblPromo.ItemsSource = db.Tables[0].DefaultView;

                cbBarang.ItemsSource = null;
                cbBarang.Items.Clear();
                query = "SELECT * from barang";
                cmd = new OracleCommand(query, con);
                cmd.ExecuteReader();
                OracleDataAdapter adp = new OracleDataAdapter(cmd);
                DataSet barang = new DataSet();
                adp.Fill(barang);

                cbBarang.ItemsSource = barang.Tables[0].DefaultView;
                cbBarang.DisplayMemberPath = barang.Tables[0].Columns["id_barang"].ToString();
                cbBarang.SelectedValuePath = barang.Tables[0].Columns["id_barang"].ToString();

                con.Close();
                //MessageBox.Show(barang.Tables[0].Columns["nama_barang"].ToString());
            }
            catch (Exception ex)
            {
                con.Close();
                //Console.WriteLine(ex.StackTrace);
                //MessageBox.Show("EROR");
                //MessageBox.Show("Gagal karena " + ex.Message);


            }
        }

        private void cbBarang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            con.Open();
            string query = "SELECT nama_barang from barang where id_barang = '"+cbBarang.SelectedValue+"'";
            OracleCommand cmd = new OracleCommand(query, con);
            //namaBarang.Content= cmd.ExecuteScalar().ToString();
            con.Close();

        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            con.Open();

            string id = tbID.Text;
            string jenis="";
            if (diskon.IsChecked == true)
            {
                jenis = "DISKON";
            }
            if(promo1.IsChecked == true)
            {
                jenis = "PROMO";
            }
            string barang = cbBarang.Text;
            int potongan = Convert.ToInt32(tbdisc.Text);
            string awal = dpawal.SelectedDate.Value.Date.ToShortDateString();
            string akhir = dpakhir.SelectedDate.Value.Date.ToShortDateString();

            string q = $"insert into promo (ID_PROMO,NAMA_PROMO,ID_BARANG," +
                         $"POTONGAN_HARGA,TANGGAL_PROMO,AKHIR_PROMO) values" + 
                        $"('{id}','{jenis}','{barang}',{potongan},TO_DATE('{awal}','DD-MM-YYYY hh24:mi:ss'),TO_DATE('{akhir}','DD-MM-YYYY hh24:mi:ss'))";
            OracleCommand cmd = new OracleCommand(q, con);
            cmd.ExecuteNonQuery();
            //con.Close();
            show();
        }

        private void diskon_Checked(object sender, RoutedEventArgs e)
        {
            ket.Content = "% (persentase diskon)";
        }

        private void promo1_Checked(object sender, RoutedEventArgs e)
        {
            ket.Content = "ribu rupiah";
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                string jenis = "";

                if (diskon.IsChecked == true)
                {
                    jenis = "DISKON";
                }
                if (promo1.IsChecked == true)
                {
                    jenis = "PROMO";
                }
                string id = tbID.Text;
                string barang = cbBarang.Text;
                int potongan = Convert.ToInt32(tbdisc.Text);
                string awal = dpawal.SelectedDate.Value.Date.ToShortDateString();
                string akhir = dpakhir.SelectedDate.Value.Date.ToShortDateString();
                string update = $"UPDATE promo SET NAMA_PROMO = '{jenis}'" +
                $", POTONGAN_HARGA ={potongan}, TANGGAL_PROMO = TO_DATE('{awal}','DD-MM-YYYY hh24:mi:ss'), AKHIR_PROMO = TO_DATE('{akhir}','DD-MM-YYYY hh24:mi:ss') where id_promo = '{id}'";

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


        private void tblPromo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //btnup.IsEnabled = true;
            if (tblPromo.SelectedIndex != -1)
            {
                DataRow dr = db.Tables[0].Rows[tblPromo.SelectedIndex];
                tbID.Text = dr["ID_PROMO"].ToString();
                cbBarang.SelectedValue = dr["ID_BARANG"].ToString();
                tbdisc.Text = dr["POTONGAN_HARGA"].ToString();
                dpawal.SelectedDate =  Convert.ToDateTime(dr["TANGGAL_PROMO"].ToString());
                dpakhir.SelectedDate = Convert.ToDateTime(dr["AKHIR_PROMO"].ToString()) ;
                string jenis = dr["NAMA_PROMO"].ToString();

                if (jenis == "DISKON")
                {
                    diskon.IsChecked = true;
                }
                else if (jenis == "PROMO")
                {
                    promo1.IsChecked = true;
                }
            }
        }
    } 
}
