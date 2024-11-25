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
    public partial class Form1 : Form
    {
        static string filePath;
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        OpenFileDialog open;
        SaveFileDialog save;
        Form popupForm;
        DataTable laotable;
        string extension;
        private byte[] imageData;
        public Form1()
        {
            InitializeComponent();
            FindDB();
            if (conn != null)
            {
                NaitaAndmed();
                NaitaLaod();
            }
        }

        public void FindDB()
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database.mdf");

            if (File.Exists(filePath))
            {
                conn = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={filePath};Integrated Security=True");
            }
            else
            {
                MessageBox.Show("База данных не найдена. Подключите новую базу данных.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void NaitaLaod()
        {
            conn.Open();
            cmd = new SqlCommand("SELECT Id, LaoNimetus FROM Ladu", conn);
            adapter = new SqlDataAdapter(cmd);
            laotable = new DataTable();
            adapter.Fill(laotable);
            foreach (DataRow item in laotable.Rows)
            {
                Ladu_cb.Items.Add(item["LaoNimetus"]);
            }
            conn.Close();
        }

        public void NaitaAndmed()
        {
            if (conn == null)
            {
                MessageBox.Show("Подключение к базе данных не настроено.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                conn.Open();
                cmd = new SqlCommand("SELECT * FROM Toode", conn);
                adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void Lisa_btn_Click(object sender, EventArgs e)
        {
            if (Nimetus_txt.Text.Trim() != string.Empty && Kogus_txt.Text.Trim() != string.Empty && Hind_txt.Text.Trim() != string.Empty)
            {
                try
                {
                    conn.Open();

                    cmd = new SqlCommand("SELECT Id FROM Ladu WHERE LaoNimetus=@ladu", conn);
                    cmd.Parameters.AddWithValue("@ladu", Ladu_cb.Text);
                    cmd.ExecuteNonQuery();
                    ID = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd = new SqlCommand("Insert into Toode(Nimetus, Kogus, Hind, Pilt) Values (@toode,@kogus,@hind,@pilt)", conn);
                    cmd.Parameters.AddWithValue("@toode", Nimetus_txt.Text);
                    cmd.Parameters.AddWithValue("@kogus", Kogus_txt.Text);
                    cmd.Parameters.AddWithValue("@hind", Hind_txt.Text);
                    cmd.Parameters.AddWithValue("@pilt", Nimetus_txt.Text + extension);

                    //imageData = File.ReadAllBytes(open.FileName);
                    cmd.Parameters.AddWithValue("@ladu", ID);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                    NaitaAndmed();
                }
                catch (Exception)
                {
                    MessageBox.Show("Andmebaasiga viga");
                }
            }
            else
            {
                MessageBox.Show("Sisesta andmeid");
            }
        }

        private void Kustuta_btn_Click(object sender, EventArgs e)
        {
            try
            {
                ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                if (ID != 0)
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM Toode WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    // Удаляем файл
                    Kustuta_fail(dataGridView1.SelectedRows[0].Cells["Pilt"].Value.ToString());

                    Emaldamine();
                    NaitaAndmed();

                    MessageBox.Show("Запись успешно удалена", "Удаление");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении записи: {ex.Message}");
            }
        }

        private void Kustuta_fail(string file)
        {
            try
            {
                // Полный путь к файлу
                string filePath = Path.Combine(Path.GetFullPath(@"..\..\Pildid"), file);

                // Проверяем, существует ли файл
                if (File.Exists(filePath))
                {
                    // Сбрасываем картинку в PictureBox
                    pictureBox1.Image?.Dispose();
                    pictureBox1.Image = null;

                    // Удаляем файл
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении файла: {ex.Message}");
            }
        }

        private void Uuenda_btn_Click(object sender, EventArgs e)
        {
            if (Nimetus_txt.Text.Trim() != string.Empty && Kogus_txt.Text.Trim() != string.Empty && Hind_txt.Text.Trim() != string.Empty)
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("Update Toode SET Nimetus=@toode, Kogus=@kogus, Hind=@hind, LaoID=@laoid WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Parameters.AddWithValue("@toode", Nimetus_txt.Text);
                    cmd.Parameters.AddWithValue("@kogus", Kogus_txt.Text);
                    cmd.Parameters.AddWithValue("@hind", Hind_txt.Text);
                    cmd.Parameters.AddWithValue("@pilt", Nimetus_txt.Text + extension);
                    cmd.Parameters.AddWithValue("@laoid", Ladu_cb);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                    NaitaAndmed();
                    Emaldamine();
                    MessageBox.Show("Andmed elukalt uuendatud", "Uuendamine");
                }
                catch (Exception)
                {
                    MessageBox.Show("Andmebaasiga viga");
                }
            }
            else
            {
                MessageBox.Show("Sisesta andmeid");
            }
        }

        private void Emaldamine()
        {
            Nimetus_txt.Text = "";
            Kogus_txt.Text = "";
            Hind_txt.Text = "";
            pictureBox1.Image = Image.FromFile(Path.Combine(Path.GetFullPath(@"..\..\Pildid"), "pilt.jpg"));
        }

        int ID = 0;
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = (int)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;
            Nimetus_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["Nimetus"].Value.ToString();
            Kogus_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["Kogus"].Value.ToString();
            Hind_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["Hind"].Value.ToString();
            try
            {
                pictureBox1.Image = Image.FromFile(Path.Combine(Path.GetFullPath(@"..\..\Pildid"),
                    dataGridView1.Rows[e.RowIndex].Cells["Pilt"].Value.ToString()));
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception)
            {
                pictureBox1.Image = Image.FromFile(Path.Combine(Path.GetFullPath(@"..\..\Pildid"), "pilt.jpg"));
            }
        }

        private void Otsipilt_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            open.InitialDirectory = @"C:\Users\opilane\Pictures\";
            open.Multiselect = false;
            open.Filter = "Images Files (*.jpeg;*.png;*.bmp;*.jpg;|*.jpeg;*.png;*.bmp;*.jpg";
            FileInfo openfile = new FileInfo(@"C:\Users\opilane\Pictures\" + open.FileName);
            if (open.ShowDialog() == DialogResult.OK && Nimetus_txt != null)
            {
                save = new SaveFileDialog();
                save.InitialDirectory = Path.GetFullPath(@"..\..\..\Pildid");
                string extension = Path.GetExtension(open.FileName);

                save.FileName = Nimetus_txt.Text + extension;
                save.Filter = "Images" + Path.GetExtension(open.FileName) + "|" + Path.GetExtension(open.FileName);
                if (save.ShowDialog() == DialogResult.OK && Nimetus_txt != null)
                {
                    File.Copy(open.FileName, save.FileName);
                    pictureBox1.Image = Image.FromFile(save.FileName);
                }
            }
            else
            {
                MessageBox.Show("Puudub toode nimetus või ole cancel vajatud");
            }
        }

        private void ConnectDB_btn_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                Multiselect = false,
                Filter = "Database Files (*.mdf)|*.mdf"
            };

            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string projectRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\");
                    string databasesFolder = Path.Combine(projectRoot, "Databases");
                    Directory.CreateDirectory(databasesFolder);

                    // Новый путь для базы данных
                    string newFilePath = Path.Combine(databasesFolder, Path.GetFileName(open.FileName));

                    // Копируем файл базы данных
                    File.Copy(open.FileName, newFilePath, overwrite: true);

                    // Обновляем путь для подключения
                    filePath = newFilePath;
                    conn.ConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={filePath};Integrated Security=True";

                    // Проверяем подключение
                    conn.Open();
                    conn.Close();

                    MessageBox.Show("База данных успешно подключена.");
                    NaitaAndmed(); // Обновляем данные в интерфейсе
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка подключения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    //public partial class Form1 : Form
    //{
    //    private ToodeRepository _toodeRepo;
    //    private LaduService _laduService;
    //    private int _selectedToodeId = 0;
    //    private int _selectedLaduId = 0;

    //    public Form1()
    //    {
    //        InitializeComponent();

    //        // Инициализация соединения и репозиториев
    //        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
    //        _toodeRepo = new ToodeRepository(new SqlConnection(connectionString));
    //        _laduService = new LaduService(connectionString);

    //        // Загрузка данных
    //        LoadTooded();
    //        LoadLaod();
    //    }

    //    private void LoadTooded()
    //    {
    //        dataGridView1.DataSource = _toodeRepo.GetAll();
    //    }

    //    private void LoadLaod()
    //    {
    //        var laod = _laduService.GetAllLaod();
    //        Ladu_cb.Items.Clear();
    //        foreach (var ladu in laod)
    //        {
    //            Ladu_cb.Items.Add(new { ladu.Id, ladu.LaoNimetus });
    //        }
    //        Ladu_cb.DisplayMember = "LaoNimetus";
    //        Ladu_cb.ValueMember = "Id";
    //    }

    //    private void Lisa_btn_Click(object sender, EventArgs e)
    //    {
    //        if (ValidateInputs())
    //        {
    //            var toode = new Toode
    //            {
    //                Nimetus = Nimetus_txt.Text,
    //                Kogus = int.Parse(Kogus_txt.Text),
    //                Hind = float.Parse(Hind_txt.Text),
    //                Pilt = SaveImageAndGetPath(),
    //                LaoID = Ladu_cb.SelectedItem != null ? (int?)((dynamic)Ladu_cb.SelectedItem).Id : null
    //            };

    //            _toodeRepo.Insert(toode);
    //            LoadTooded();
    //            ClearInputs();
    //            MessageBox.Show("Товар успешно добавлен!");
    //        }
    //    }

    //    private void Kustuta_btn_Click(object sender, EventArgs e)
    //    {
    //        if (_selectedToodeId > 0)
    //        {
    //            _toodeRepo.Delete(_selectedToodeId);
    //            LoadTooded();
    //            ClearInputs();
    //            MessageBox.Show("Товар успешно удалён!");
    //        }
    //        else
    //        {
    //            MessageBox.Show("Выберите товар для удаления.");
    //        }
    //    }

    //    private void Uuenda_btn_Click(object sender, EventArgs e)
    //    {
    //        if (_selectedToodeId > 0 && ValidateInputs())
    //        {
    //            var toode = new Toode
    //            {
    //                Id = _selectedToodeId,
    //                Nimetus = Nimetus_txt.Text,
    //                Kogus = int.Parse(Kogus_txt.Text),
    //                Hind = float.Parse(Hind_txt.Text),
    //                Pilt = SaveImageAndGetPath(),
    //                LaoID = Ladu_cb.SelectedItem != null ? (int?)((dynamic)Ladu_cb.SelectedItem).Id : null
    //            };

    //            _toodeRepo.Update(toode);
    //            LoadTooded();
    //            ClearInputs();
    //        }
    //    }

    //    private void Otsipilt_Click(object sender, EventArgs e)
    //    {
    //        using (OpenFileDialog openFileDialog = new OpenFileDialog())
    //        {
    //            openFileDialog.Filter = "Изображения (*.jpg;*.png)|*.jpg;*.png|Все файлы (*.*)|*.*";
    //            if (openFileDialog.ShowDialog() == DialogResult.OK)
    //            {
    //                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
    //            }
    //        }
    //    }


    //    private bool ValidateInputs()
    //    {
    //        if (string.IsNullOrWhiteSpace(Nimetus_txt.Text) ||
    //            string.IsNullOrWhiteSpace(Kogus_txt.Text) ||
    //            string.IsNullOrWhiteSpace(Hind_txt.Text))
    //        {
    //            MessageBox.Show("Заполните все поля.");
    //            return false;
    //        }

    //        if (!int.TryParse(Kogus_txt.Text, out _) || !float.TryParse(Hind_txt.Text, out _))
    //        {
    //            MessageBox.Show("Некорректные данные в поле количества или цены.");
    //            return false;
    //        }

    //        return true;
    //    }

    //    private string SaveImageAndGetPath()
    //    {
    //        if (pictureBox1.Image != null)
    //        {
    //            string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
    //            Directory.CreateDirectory(directory);

    //            string fileName = $"{Guid.NewGuid()}.jpg";
    //            string filePath = Path.Combine(directory, fileName);

    //            pictureBox1.Image.Save(filePath);
    //            return fileName;
    //        }
    //        return "pilt.jpg"; // Дефолтное изображение
    //    }

    //    private void ClearInputs()
    //    {
    //        _selectedToodeId = 0;
    //        Nimetus_txt.Clear();
    //        Kogus_txt.Clear();
    //        Hind_txt.Clear();
    //        Ladu_cb.SelectedIndex = -1;
    //        pictureBox1.Image = null;
    //    }

    //    private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    //    {
    //        _selectedToodeId = (int)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;
    //        Nimetus_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["Nimetus"].Value.ToString();
    //        Kogus_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["Kogus"].Value.ToString();
    //        Hind_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["Hind"].Value.ToString();

    //        int? laduId = dataGridView1.Rows[e.RowIndex].Cells["LaoID"].Value as int?;
    //        if (laduId.HasValue)
    //        {
    //            Ladu_cb.SelectedItem = Ladu_cb.Items.Cast<dynamic>().FirstOrDefault(x => x.Id == laduId.Value);
    //        }

    //        string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", dataGridView1.Rows[e.RowIndex].Cells["Pilt"].Value.ToString());
    //        if (File.Exists(imagePath))
    //        {
    //            pictureBox1.Image = Image.FromFile(imagePath);
    //        }
    //    }
    //}
}
