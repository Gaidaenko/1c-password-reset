using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1c_password_reset
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string cestart = @"C:\Program Files\1cv8\common\1cestart.exe";
        public bool CheckProcess()
        {
            Process[] processNme = Process.GetProcesses();
            foreach (Process process in processNme)
            {
                if (process.ProcessName == "1cv8")
                return true;
            }
            return false;
        }
        public void start1cConf()
        {
            if (File.Exists(cestart))
            {
                Process.Start(cestart);
            }
            else if (!File.Exists(cestart))
            {
                Color colorWarning = Color.Red;
                label1.ForeColor = colorWarning;
                label1.Text = "Файл для запуска 1с не существует.\n Попробуйсте запустить 1с вручную!";
            }
        }
        public void SqlConnectionStep1()
        {
            string dbName = richTextBox1.Text;
            string sqlLogin = richTextBox3.Text;
            string sqlPass = richTextBox4.Text;
            
            string connectionString = @"Initial Catalog=" + dbName + ";Persist Security Info=True;User ID=" + sqlLogin + ";Password=" + sqlPass;
            SqlConnection connect = new SqlConnection(connectionString);
            
            string sqlUsersRename = @"EXEC sp_rename 'v8users','v8users_tmp'";
            string sqlParamsRename = @"UPDATE Params SET FileName = 'users.usr_tmp' WHERE FileName = 'users.usr'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();

                    SqlCommand usersRename = new SqlCommand(sqlUsersRename, connect);
                    int number1 = usersRename.ExecuteNonQuery();
                                        
                    SqlCommand paramsRename = new SqlCommand(sqlParamsRename, connect);
                    int number2 = paramsRename.ExecuteNonQuery();

                    Color color = Color.Blue;
                    label3.ForeColor = color;
                    label3.Text = "Пользователи 1c отключены!\n\nЗайдите в конфигуратор 1с\nи НЕ ЗАКРЫВАЯ его нажмите Шаг 2!";

                    start1cConf();
                }
                catch (SqlException e)
                {
                    Color colorWarning = Color.Red;
                    label3.ForeColor = colorWarning;
                    label3.Text = "Не удалось подключится к SQL!\n\nПроверьте правильность написания имени\nсервера, или базы SQL!";                
                }
                finally
                {
                    connect.Close();                           
                }                
            }        
        }
        public void SqlConnectionStep2()
        {
            string dbName = richTextBox1.Text;
            string sqlLogin = richTextBox3.Text;
            string sqlPass = richTextBox4.Text;
            // string connectionString = @"Data Source = " + serverName + ";Initial Catalog=" + dbName + ";Integrated Security=True";
            string connectionString = @"Initial Catalog=" + dbName + ";Persist Security Info=True;User ID=" + sqlLogin + ";Password=" + sqlPass;
            
            SqlConnection connect = new SqlConnection(connectionString);

            string sqlDropTable = @"DROP TABLE v8users";
            string sqlReturnUsers = @"EXEC sp_rename 'v8users_tmp','v8users'";
            string sqlReuturnParams = @"UPDATE Params SET FileName = 'users.usr' WHERE FileName = 'users.usr_tmp'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    SqlCommand usersDrop = new SqlCommand(sqlDropTable, connect);
                    int number1 = usersDrop.ExecuteNonQuery();

                    SqlCommand usersReturn = new SqlCommand(sqlReturnUsers, connect);
                    int number2 = usersReturn.ExecuteNonQuery();

                    SqlCommand paramsReturn = new SqlCommand(sqlReuturnParams, connect);
                    int number3 = paramsReturn.ExecuteNonQuery();

                    Color color = Color.Blue;
                    label3.ForeColor = color;
                    label3.Text = "Пользователи 1c загруженны!\n\nСоздайте своего пользователя,\nили измените пароль к существующему.";
                }
                catch (SqlException e)
                {
                    Color colorWarning = Color.Red;
                    label3.ForeColor = colorWarning;
                    label3.Text = "Подключение у SQL серверу не удалось!\n\nПроверьте правильность написания имени\n сервера, или базы SQL!";
                }
                finally
                {
                    connect.Close();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnectionStep1();  
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (CheckProcess() == true)
            {
                CheckProcess();
            }
            else
            {
                Color colorWarning = Color.Red;
                label3.ForeColor = colorWarning;
                label3.Text = "Сначало выполните ШАГ 1!";
                return;
            }            
            
            SqlConnectionStep2();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }    
        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
 
        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
