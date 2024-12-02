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
            // Проверяем, есть ли товар уже в корзине
            CartItem existingItem = cart.FirstOrDefault(item => item.Name == productName);

            if (existingItem != null)
            {
                existingItem.Quantity++;  // Увеличиваем количество, если товар уже в корзине
            }
            else
            {
                cart.Add(new CartItem(productName, price));  // Добавляем новый товар в корзину
            }

            UpdateCartDisplay();  // Обновляем отображение корзины
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
                    Panel productPanel = new Panel();
                    productPanel.Size = new Size(tootePanel.Width/3, tootePanel.Height/2);

                    // Картинка 
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Image = Image.FromFile(Path.Combine(imageFolder, reader["Pilt"].ToString()));
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox.Size = new Size(productPanel.Width, productPanel.Height-productPanel.Height/4);

                    // Название
                    Label nameLabel = new Label();
                    nameLabel.Text = reader["Nimetus"].ToString();
                    nameLabel.Dock = DockStyle.Top;

                    // Цена
                    Label priceLabel = new Label();
                    priceLabel.Text = "Цена: " + reader["Hind"].ToString() + " €";
                    priceLabel.Dock = DockStyle.Top;

                    // Количество
                    Label quantityLabel = new Label();
                    quantityLabel.Text = "Количество: " + reader["Kogus"].ToString();
                    quantityLabel.Dock = DockStyle.Top;

                    // Кнопка добавления
                    Button addButton = new Button();
                    addButton.Text = "Добавить в корзину";
                    addButton.Dock = DockStyle.Bottom;
                    addButton.Click += (sender, e) => AddToCart(reader["Nimetus"].ToString(), Convert.ToDecimal(reader["Hind"]));

                    // Добавляем компоненты на панель
                    productPanel.Controls.Add(nameLabel);
                    productPanel.Controls.Add(pictureBox);
                    productPanel.Controls.Add(priceLabel);
                    productPanel.Controls.Add(quantityLabel);
                    productPanel.Controls.Add(addButton);

                    // Добавляем панель товара в FlowLayoutPanel
                    tootePanel.Controls.Add(productPanel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки товаров: {ex.Message}");
            }
        }
        private void UpdateCartDisplay()
        {
            cartListBox.Items.Clear();  // Очищаем текущий список корзины

            foreach (CartItem item in cart)
            {
                cartListBox.Items.Add($"{item.Name} - {item.Quantity} шт. - {item.Price * item.Quantity} €");
            }
        }
    }
}
