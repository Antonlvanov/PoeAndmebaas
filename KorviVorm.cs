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
    public partial class KorviVorm : Form
    {
        private List<CartItem> cart = new List<CartItem>();

        static string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\"));
        static string db_name = "Database.mdf";
        static string db_path = Path.Combine(projectRoot, "Database.mdf");
        static string imageFolder = Path.Combine(projectRoot, @"Pildid");

        SqlConnection conn;
        public KorviVorm()
        {
            InitializeComponent();
            FindDB();
            LoadProducts();
        }

        public void FindDB()
        {
            if (File.Exists(db_path))
            {
                conn = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={db_path};Integrated Security=True");
            }
        }

        private void AddToCart(string productName, decimal price)
        {
            CartItem existingItem = cart.FirstOrDefault(item => item.Name == productName);

            if (existingItem != null)
            {
                existingItem.Quantity++;  
            }
            else
            {
                cart.Add(new CartItem(productName, price)); 
            }

            UpdateCartDisplay();  
        }

        private void LoadProducts()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Nimetus, Hind, Kogus, Pilt FROM Toode", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string productName = reader["Nimetus"].ToString();
                    decimal productPrice = Convert.ToDecimal(reader["Hind"]);
                    int productQuantity = Convert.ToInt32(reader["Kogus"]);
                    string productImagePath = Path.Combine(imageFolder, reader["Pilt"].ToString());

                    Panel productPanel = new Panel();
                    productPanel.Size = new Size(tootePanel.Width / 3 - 10, tootePanel.Height / 2 - 10);
                    productPanel.Margin = new Padding(5);
                    productPanel.BorderStyle = BorderStyle.FixedSingle;

                    Label nameLabel = new Label();
                    nameLabel.Text = productName;
                    nameLabel.Font = new Font("Arial", 12, FontStyle.Bold);
                    nameLabel.TextAlign = ContentAlignment.MiddleCenter;
                    nameLabel.Dock = DockStyle.Top;
                    nameLabel.Height = 30;

                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Image = Image.FromFile(productImagePath);
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox.Dock = DockStyle.Top;
                    pictureBox.Height = productPanel.Height - 110;

                    Panel infoPanel = new Panel();
                    infoPanel.Dock = DockStyle.Top;
                    infoPanel.Height = 40;

                    Label priceLabel = new Label();
                    priceLabel.Text = "Hind: " + productPrice.ToString() + " €";
                    priceLabel.Font = new Font("Arial", 10);
                    priceLabel.AutoSize = false; 
                    priceLabel.Size = new Size(infoPanel.Width / 2 - 10, 30); 
                    priceLabel.Location = new Point(5, 5); 

                    Label quantityLabel = new Label();
                    quantityLabel.Text = "Kogus: " + productQuantity.ToString();
                    quantityLabel.Font = new Font("Arial", 10);
                    quantityLabel.AutoSize = false;
                    quantityLabel.Size = new Size(infoPanel.Width / 2 - 10, 30); 
                    quantityLabel.Location = new Point(infoPanel.Width / 2, 5); 

                    infoPanel.Controls.Add(priceLabel);
                    infoPanel.Controls.Add(quantityLabel);

                    Button addButton = new Button();
                    addButton.Text = "Lisa ostukorvi";
                    addButton.Dock = DockStyle.Bottom;
                    addButton.Height = 30;

                    addButton.Click += (sender, e) => AddToCart(productName, productPrice);

                    productPanel.Controls.Add(addButton);
                    productPanel.Controls.Add(infoPanel);
                    productPanel.Controls.Add(pictureBox);
                    productPanel.Controls.Add(nameLabel);

                    tootePanel.Controls.Add(productPanel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga kauba laadimisel: {ex.Message}");
            }
            finally
            {
                conn.Close(); 
            }
        }


        private void UpdateCartDisplay()
        {
            cartListBox.Items.Clear(); 

            foreach (CartItem item in cart)
            {
                cartListBox.Items.Add($"{item.Name} - {item.Quantity} tk. - {item.Price * item.Quantity} €");
            }
        }
    }
}
