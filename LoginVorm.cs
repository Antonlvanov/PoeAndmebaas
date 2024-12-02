using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoeAndmebaas
{
    public partial class LoginVorm : Form
    {
        static string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\"));
        static string db_name = "Database.mdf";
        static string db_path = Path.Combine(projectRoot, "Database.mdf");

        SqlCommand cmd;
        SqlConnection conn;

        public LoginVorm()
        {
            InitializeComponent();
            conn = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={db_path};Integrated Security=True");
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(login_input.Text) && !string.IsNullOrWhiteSpace(parool_input.Text))
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("SELECT * FROM LoginTable WHERE username=@username AND password=@password", conn);
                    cmd.Parameters.AddWithValue("@username", login_input.Text);
                    cmd.Parameters.AddWithValue("@password", parool_input.Text);

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        this.Hide();
                        AB_HaaldamineVorm AB_Vorm = new AB_HaaldamineVorm();
                        AB_Vorm.Show();
                    }
                    else
                    {
                        dr.Close();
                        MessageBox.Show("Selle kasutajanime ja parooliga pole ühtegi kontot saadaval.", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Viga: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Palun sisestage väärtused kõikidele väljadele.", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
