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
        static string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\"));
        static string db_name = "Database.mdf";
        static string db_path = Path.Combine(projectRoot, "Database.mdf");
        static string imageFolder = Path.Combine(projectRoot, @"Pildid");

        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;

        OpenFileDialog open;
        SaveFileDialog save;
        Form popupForm;
        DataTable laotable;
        string extension;
        private byte[] imageData;
        int ID = 0;
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
            if (File.Exists(db_path))
            {
                conn = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={db_path};Integrated Security=True");
            }
        }

        public void NaitaAndmed()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                cmd = new SqlCommand("SELECT * FROM Toode", conn);
                adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga andmete laadimisel: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void NaitaLaod()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                cmd = new SqlCommand("SELECT Id, LaoNimetus FROM Ladu", conn);
                adapter = new SqlDataAdapter(cmd);
                laotable = new DataTable();
                adapter.Fill(laotable);
                foreach (DataRow item in laotable.Rows)
                {
                    Ladu_cb.Items.Add(item["LaoNimetus"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga andmete laadimisel: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Закрытие подключения, если оно открыто
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void Lisa_btn_Click(object sender, EventArgs e)
        {
            if (FieldsNotNull())
            {
                try
                {
                    conn.Open();
                    // ladu
                    cmd = new SqlCommand("SELECT Id FROM Ladu WHERE LaoNimetus = @ladu", conn);
                    cmd.Parameters.AddWithValue("@ladu", Ladu_cb.Text);
                    int laduID = Convert.ToInt32(cmd.ExecuteScalar());
                    // pilt
                    string imageFileName = string.Empty;
                    if (open != null && !string.IsNullOrWhiteSpace(open.FileName))
                    {
                        extension = Path.GetExtension(open.FileName); 
                        imageFileName = Nimetus_txt.Text + extension;
                        SavePicture();
                    }
                    // andmete valmistamine
                    cmd = new SqlCommand("INSERT INTO Toode (Nimetus, Kogus, Hind, Pilt, LaoID) VALUES (@toode, @kogus, @hind, @pilt, @ladu)", conn);
                    cmd.Parameters.AddWithValue("@toode", Nimetus_txt.Text);
                    cmd.Parameters.AddWithValue("@kogus", Kogus_txt.Text);
                    cmd.Parameters.AddWithValue("@hind", Hind_txt.Text);
                    cmd.Parameters.AddWithValue("@pilt", imageFileName);
                    cmd.Parameters.AddWithValue("@ladu", laduID);
                    // käski käivitamine
                    cmd.ExecuteNonQuery();
                    ClearFields();
                    NaitaAndmed();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Andmebaasi viga: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Palun täitke kõik väljad.", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearPictureBox()
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
                GC.Collect(); 
                GC.WaitForPendingFinalizers(); 
            }
        }

        private void Otsipilt_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            open.InitialDirectory = projectRoot; //Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            open.Multiselect = false;
            open.Filter = "Images Files (*.jpeg;*.png;*.bmp;*.jpg;|*.jpeg;*.png;*.bmp;*.jpg";

            if (open.ShowDialog() == DialogResult.OK && Nimetus_txt != null)
            {
                Pildi_tee.Text = open.FileName;

                pictureBox1.Image = Image.FromFile(open.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                MessageBox.Show("Puudub toode nimetus või ole cancel vajatud");
            }
        }

        private void SavePicture()
        {
            string extension = Path.GetExtension(open.FileName);
            string newFile = Path.Combine(imageFolder, Nimetus_txt.Text + extension);
            try
            {
                ClearPictureBox();
                File.Copy(open.FileName, newFile, overwrite: true);
                open.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga faili salvestamisel: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Delete_picture(string picName)
        {
            try
            {
                ClearPictureBox();
                string picPath = Path.Combine(imageFolder, picName);
                if (File.Exists(picPath))
                {
                    File.Delete(picPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga faili kustutamisel: {ex.Message}");
            }
        }
        private void Pildi_uuendamine(string newImageName, string oldImageName)
        { 
            if (oldImageName != newImageName && open == null) // if rename image
            {
                ClearPictureBox();
                string oldPath = Path.Combine(imageFolder, oldImageName);
                string newPath = Path.Combine(imageFolder, newImageName);
                if (File.Exists(oldPath))
                {
                    File.Move(oldPath, newPath);
                }
            }
            if (open != null && !string.IsNullOrWhiteSpace(open.FileName) && File.Exists(Pildi_tee.Text.ToString())) // if image being replaced
            {
                SavePicture();
                Delete_picture(oldImageName);
            }
        }
        private void Uuenda_btn_Click(object sender, EventArgs e)
        {
            if (FieldsNotNull())
            {
                try
                {
                    string oldImageName = dataGridView1.SelectedRows[0].Cells["Pilt"].Value.ToString();
                    string newImageName = Nimetus_txt.Text + (open != null ? Path.GetExtension(open.FileName) : Path.GetExtension(oldImageName));
                    Pildi_uuendamine(newImageName, oldImageName);

                    conn.Open();
                    cmd = new SqlCommand("Update Toode SET Nimetus=@toode, Kogus=@kogus, Hind=@hind, Pilt=@pilt, LaoID=@laoid WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Parameters.AddWithValue("@toode", Nimetus_txt.Text);
                    cmd.Parameters.AddWithValue("@kogus", Kogus_txt.Text);
                    cmd.Parameters.AddWithValue("@hind", Hind_txt.Text);
                    cmd.Parameters.AddWithValue("@pilt", newImageName);
                    cmd.Parameters.AddWithValue("@laoid", Ladu_cb.SelectedIndex + 1);

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    NaitaAndmed();
                    ClearFields();
                    ClearPictureBox();
                    MessageBox.Show("Andmed elukalt uuendatud", "Uuendamine");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Andmebaasiga viga: {ex.Message}");
                }
                finally
                {
                    conn.Close();
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

                    ClearFields();
                    Delete_picture(dataGridView1.SelectedRows[0].Cells["Pilt"].Value.ToString());

                    NaitaAndmed();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga kirje kustutamisel: {ex.Message}");
            }
        }

        private void ClearFields()
        {
            Nimetus_txt.Text = null;
            Kogus_txt.Text = null;
            Hind_txt.Text = null;
            Ladu_cb.Text = null;
            Pildi_tee.Text = null;
            pictureBox1.Image?.Dispose();
        }

        private bool FieldsNotNull()
        {
            if (!string.IsNullOrWhiteSpace(Nimetus_txt.Text) &&
                !string.IsNullOrWhiteSpace(Kogus_txt.Text) &&
                !string.IsNullOrWhiteSpace(Hind_txt.Text) &&
                !string.IsNullOrWhiteSpace(Ladu_cb.Text))
            {
                return true;
            }
            return false;
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = (int)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;
            Nimetus_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["Nimetus"].Value.ToString();
            Kogus_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["Kogus"].Value.ToString();
            Hind_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["Hind"].Value.ToString();

            Ladu_cb.SelectedIndex = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["LaoID"].Value) - 1;


            if (dataGridView1.Rows[e.RowIndex].Cells["Pilt"].Value.ToString() != "")
            {
                try
                {
                    pictureBox1.Image = Image.FromFile(Path.Combine(imageFolder,
                        dataGridView1.Rows[e.RowIndex].Cells["Pilt"].Value.ToString()));
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Viga pildi näitamisega: {ex.Message}");
                }
            }
            else
            {
                pictureBox1.Image = Image.FromFile(Path.Combine(imageFolder, "pilt.jpg"));
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
