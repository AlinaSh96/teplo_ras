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
using System.Net;
using System.IO;
using HtmlAgilityPack;

using teplo_ras.ViewModel;


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
        private readonly ThermalCalculatorViewModel _thermalCalculatorViewModel;

        public MainWindow()
        {
            _thermalCalculatorViewModel = new ThermalCalculatorViewModel();
            InitializeComponent();
            Fillcombo();
            Fillcombo1();
            Fillcombo2();
            Fillcombo3();
            LoadData();
        
        }

        public ThermalCalculatorViewModel ThermalCalculator => _thermalCalculatorViewModel;
        void Fillcombo()
        {
           // string connStr = "server=localhost;user=root;database=123;password=12345;";
            string sql = "select * from pom ";
          //  MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader;
            try
            {      
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string result = reader.GetString("Помещения");
                    comboBox1.Items.Add(result);
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
           // string connStr = "server=localhost;user=root;database=123;password=12345;";
            string sql = "select * from construct ";
            // создаём объект для подключения к БД
           // MySqlConnection conn = new MySqlConnection(connStr);
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
           // string connStr = "server=localhost;user=root;database=123;password=12345;";
            string sql = "select * from podcat ";
            // создаём объект для подключения к БД
           // MySqlConnection conn = new MySqlConnection(connStr);
            // устанавливаем соединение с БД
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader;
            try
            {
                conn.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string result = reader.GetString("PodPodMat");
                    comboBox3.Items.Add(result);
                    //nn.ItemsSource = result;
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
           // string connStr = "server=localhost;user=root;database=123;password=12345;";

            string sql1 = "select * from gorod ";
            string sql = "select * from  gorod ,clim_param where name='" + comboBox4.Text + "' AND  gorod.id = clim_param.id";
            // создаём объект для подключения к БД
          //  MySqlConnection conn = new MySqlConnection(connStr);
            // устанавливаем соединение с БД
            MySqlCommand command1 = new MySqlCommand(sql1, conn);
            MySqlDataReader reader1;
            try
            {
                conn.Open();
                reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    string result = reader1.GetString("name");
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


           // string MyConString = "server=localhost;user=root;database=123;password=12345;";

            string sql = "SELECT * FROM har_mat";
            using (MySqlConnection connection = new MySqlConnection(connStr))
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
            //string connStr = "server=localhost;user=root;database=123;password=12345;";
            string sql = "select * from pom where Помещения='" + comboBox1.Text + "' ; ";
            // создаём объект для подключения к БД
           // MySqlConnection conn = new MySqlConnection(connStr);
            // устанавливаем соединение с БД
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader;
            try
            {
                conn.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string result1 = reader.GetInt32("Id помещения").ToString();
                    string result2 = reader.GetString("Влажность в помещени");;
                    textBox3.Text = result2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // string connStr = "server=localhost;user=root;database=123;password=12345;";
            string sql = "select * from construct where name='" + comboBox2.SelectedItem + "' ; ";
           // MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader;
            try
            {
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
          //  string MyConString = "server=localhost;user=root;database=123;password=12345;";         
            string sql = "SELECT  `PodPodMat`,`plotnost`,`udelnaya_teployemkodt`from podcat, har_mat WHERE  PodPodMat =  '" + comboBox3.SelectedItem + "' AND podcat.id = har_mat.id";

         //  string sql1 = "SstringELECT  `подподМат`,`плотность`,`улельная теплоемкость`from podcat, har_mat WHERE  подподМат =  '" + comboBox3.Text + "' AND podcat.id = har_mat.id";
            // string sql = "SELECT * FROM podcat where подподМат =  '" + comboBox3.Text + "' ; ";
           

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();
                using (MySqlCommand cmdSel = new MySqlCommand(sql, connection))
               // using (MySqlCommand cmdSel1 = new MySqlCommand(sql1, connection))
                {
                    


                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                 
                    da.Fill(dt);
  
                    try
                    {
                        

                      int index = 0;
                      dataGrid2.ItemsSource = dt.DefaultView;
                     
                        dt.AcceptChanges();
                     
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    } 
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            string sql = "select * from  gorod ,clim_param where name='" + comboBox4.SelectedItem + "' AND  gorod.id = clim_param.id";

            if (comboBox4.Text != string.Empty)
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(sql, conn);
                    MySqlDataReader reader;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        string result11 = reader.GetDouble("temp_cold5(0.92)").ToString();
                        string result1 = reader.GetDouble("prodol_otop_perioda").ToString();
                        string result2 = reader.GetDouble("cred_temp_vozd").ToString();
                        string result3 = reader.GetDouble("otnos_vlazh_vozd").ToString();
                      //  string result4 = reader.GetString("uslov_eksplut");


                        textBox8.Text = result11;
                        textBox9.Text = result1;
                        textBox10.Text = result2;
                        textBox11.Text = result3;
                     //   textBox12.Text = result4;
                   
                        var rasch_temp = 20;
                        //var d = 10;
                        textBox13.Text = ((rasch_temp - Convert.ToDouble(textBox10.Text)) * Convert.ToInt32(textBox9.Text)).ToString();




                    }

                }
          
        }

        private void TextBox10_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var response = WebService.GetResponse("http://docs.cntd.ru/document/1200095546");
            WebService.GetResponse("http://docs.cntd.ru/document/1200095546");
           HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            // Присваиваем текстовой переменной k html-код
            // Загружаем в класс (парсер) наш html
            doc.LoadHtml(response);
           
            HtmlNode bodyNode = doc.DocumentNode.SelectSingleNode(".//*[@id='P002A0060']");
            // richTextBox1.AppendText(bodyNode.InnerText);
            // Выводим на экран результат работы парсера
            MessageBox.Show(bodyNode.InnerText);
            //string c = bodyNode.InnerText;
            string connStr = "server=localhost;user=root;database=123;password=12345;";
            string sql = "update `clim_param` set `temp_cold5(0.92)` = '" + bodyNode.InnerText + "'  where  (`id` = '1') ";
            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(connStr);
            // устанавливаем соединение с БД
            MySqlCommand command = new MySqlCommand(sql, conn);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();

        }

        private void TextBox9_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox8_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string connStr = "server=localhost;user=root;database=123;password=12345;";
            string sql = "insert into gorod (`name`) values (@name)";
           string sql1 = "insert into clim_param (`temp_cold5(0.92)`,`prodol_otop_perioda`,`cred_temp_vozd`,`otnos_vlazh_vozd`) values (@t,@tt,@ttt,@tttt)";
            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(connStr);
            // устанавливаем соединение с БД
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlCommand command1 = new MySqlCommand(sql1, conn);
            try
            {
                conn.Open();
                command.Parameters.AddWithValue("@name", gorod.Text);
                command1.Parameters.AddWithValue("@t", l1.Text);
                command1.Parameters.AddWithValue("@tt", l2.Text);
                command1.Parameters.AddWithValue("@ttt", l3.Text);
                command1.Parameters.AddWithValue("@tttt", l3.Text);

                command.ExecuteNonQuery();
                command1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            MessageBox.Show("Данные добавлены");
        }
        
    }
    }
    
    




    