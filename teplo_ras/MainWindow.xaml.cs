using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
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
using MySql.Data.MySqlClient;

namespace teplo_ras
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string connStr = "server=localhost;user=root;database=123;password=12345;";
        // создаём объект для подключения к БД
        MySqlConnection conn = new MySqlConnection(connStr);
        // устанавливаем соединение с БД

        public MainWindow()
        {
            InitializeComponent();
            Fillcombo();
            Fillcombo1();
            Fillcombo2();
            Fillcombo3();
            LoadData();
             dataGrid2.SelectionChanged += ComboBox2_SelectionChanged;
        }


        void Fillcombo()
        {
            string connStr = "server=localhost;user=root;database=123;password=12345;";
            string sql = "select * from pom ";
            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(connStr);
            // устанавливаем соединение с БД
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader;
            try
            {
                conn.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string result = reader.GetString("Помещения");
                    comboBox1.Items.Add(result);
                    // conn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        void Fillcombo1()
        {
            string connStr = "server=localhost;user=root;database=123;password=12345;";
            string sql = "select * from construct ";
            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(connStr);
            // устанавливаем соединение с БД
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader;
            try
            {
                conn.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string result = reader.GetString("name");
                    comboBox2.Items.Add(result);
                    // conn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        void Fillcombo2()
        {
            string connStr = "server=localhost;user=root;database=123;password=12345;";
            string sql = "select * from podcat ";
            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(connStr);
            // устанавливаем соединение с БД
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader;
            try
            {
                conn.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string result = reader.GetString("подподМат");
                    comboBox3.Items.Add(result);
                    // conn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        void Fillcombo3()
        {
            string connStr = "server=localhost;user=root;database=123;password=12345;";
            string sql = "select * from gorod ";
            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(connStr);
            // устанавливаем соединение с БД
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader;
            try
            {
                conn.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string result = reader.GetString("name");
                    comboBox4.Items.Add(result);
                    // conn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        private void LoadData()
        {


            string MyConString = "server=localhost;user=root;database=123;password=12345;";

            string sql = "SELECT `плотность`,`улельная теплоемкость`,`коэфф теплопроводеости`,`паропроницаемость А` FROM har_mat";
            using (MySqlConnection connection = new MySqlConnection(MyConString))
            {
                connection.Open();
                using (MySqlCommand cmdSel = new MySqlCommand(sql, connection))
                {
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                    da.Fill(dt);
                    dataGrid1.ItemsSource = dt.DefaultView;

                }
                connection.Close();
            }
        }


        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string connStr = "server=localhost;user=root;database=123;password=12345;";
            string sql = "select * from pom where Помещения='" + comboBox1.Text + "' ; ";
            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(connStr);
            // устанавливаем соединение с БД
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader;
            try
            {
                conn.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //string result = reader.GetString("Помещения");
                    string result1 = reader.GetInt32("Id помещения").ToString();
                    string result2 = reader.GetString("Влажность в помещени");

                    //textBox1.Text = result;
                    // textBox2.Text = result1;
                    textBox3.Text = result2;




                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string connStr = "server=localhost;user=root;database=123;password=12345;";
            string sql = "select * from construct where name='" + comboBox2.Text + "' ; ";
            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(connStr);
            // устанавливаем соединение с БД
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader;
            try
            {
                conn.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string result = reader.GetDouble("koffi_zav").ToString();
                    string result1 = reader.GetDouble("koffi_teplo_vntr").ToString();
                    string result2 = reader.GetInt32("koffi_teplo_nar").ToString();
                    string result3 = reader.GetInt32("temp_per").ToString();

                    textBox4.Text = result;
                    textBox5.Text = result1;
                    textBox6.Text = result2;
                    textBox7.Text = result3;




                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();

        }

        private void TextBox5_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string MyConString = "server=localhost;user=root;database=123;password=12345;";         
            string sql = "SELECT  `подподМат`,`плотность`,`улельная теплоемкость`from podcat, har_mat WHERE  подподМат =  '" + comboBox3.Text + "' AND podcat.id = har_mat.id";

         //  string sql1 = "SstringELECT  `подподМат`,`плотность`,`улельная теплоемкость`from podcat, har_mat WHERE  подподМат =  '" + comboBox3.Text + "' AND podcat.id = har_mat.id";
            // string sql = "SELECT * FROM podcat where подподМат =  '" + comboBox3.Text + "' ; ";
           

            using (MySqlConnection connection = new MySqlConnection(MyConString))
            {
                connection.Open();
                using (MySqlCommand cmdSel = new MySqlCommand(sql, connection))
               // using (MySqlCommand cmdSel1 = new MySqlCommand(sql1, connection))
                {
                    


                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                  //  MySqlDataAdapter da1 = new MySqlDataAdapter(cmdSel1);
                    da.Fill(dt);
                    //  da1.Fill(dt);
                    // while (dataGrid2.Items.Count>0) {

                    // for (dataGrid2.Items.Count >; i < dataGrid2.Items.Count; i++)
                    try
                    {
                        
                        // if (dt.Rows.Count>0) { 
                       //  dt.Rows.Add(dt);

                        // dataGrid2.Items.Add;
                      int index = 0;
                     //   dataGrid2.ItemsSource = dt.DefaultView;
                        dt.Rows.Add("mjfjj0");

                     //   for (int i = 0; i < 5; i++)
                      //  {
                            ///   if (da=null)
                        //    dataGrid2.DataContext = dt.Rows[i];
                        //}
                        //   dt.Rows[index + 1];
                        dataGrid2.ItemsSource = dt.DefaultView;
                    //    dataGrid2.SelectionChanged += ComboBox2_SelectionChanged;
                        dt.AcceptChanges();
                       

                     ///   dt.Rows.Add(dt.DefaultView.ToString);
                        // }
                        //   //  dataGrid2.ItemsSource = dt.DefaultView;
                          // dataGrid2.DataContext = dt.Rows.Add(dt);

                        //dataGrid2.ItemsSource = dt.DefaultView;

                        // dataGrid2.Items.Refresh();
                        // dt.AcceptChanges();
                        // }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    //  }
                    // connection.Close();
                }
                //connection.Close();
     
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string connStr = "server=localhost;user=root;database=123;password=12345;";
            string sql = "select * from  gorod ,clim_param where name='" + comboBox4.Text + "' AND  gorod.id = clim_param.id";
            // создаём объект для подключения к БД
            //SELECT* from gorod, clim_param WHERE  gorod.id = clim_param.id;
            MySqlConnection conn = new MySqlConnection(connStr);
            // устанавливаем соединение с БД
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader;
            try
            {
                conn.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string result = reader.GetDouble("temp_cold5(0.92)").ToString();
                    string result1 = reader.GetDouble("prodol_otop_perioda").ToString();
                   string result2 = reader.GetDouble("cred_temp_vozd").ToString();
                   string result3 = reader.GetDouble("otnos_vlazh_vozd").ToString();
                   string result4 = reader.GetString("uslov_eksplut");


                    textBox8.Text = result;
                    textBox9.Text = result1;
                    textBox10.Text = result2;
                    textBox11.Text = result3;
                    textBox12.Text = result4;
                    var rasch_temp = 20;
                    var d = 10;
                    textBox13.Text =( (rasch_temp - Convert.ToDouble(textBox10.Text))*Convert.ToInt32(textBox9.Text)).ToString() ;




                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }

        private void TextBox10_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
    
}



    