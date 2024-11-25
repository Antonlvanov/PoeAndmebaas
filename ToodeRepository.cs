using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoeAndmebaas
{
    public class ToodeRepository
    {
        private readonly SqlConnection _connection;

        // Конструктор для инициализации подключения
        public ToodeRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        // Получение всех записей из таблицы Toode
        public List<Toode> GetAll()
        {
            List<Toode> tooded = new List<Toode>();
            string query = "SELECT * FROM Toode";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tooded.Add(new Toode
                    {
                        Id = (int)reader["Id"],
                        Nimetus = reader["Nimetus"].ToString(),
                        Kogus = Convert.ToInt32(reader["Kogus"]),
                        Hind = Convert.ToSingle(reader["Hind"]),
                        Pilt = reader["Pilt"].ToString(),
                        LaoID = reader["LaoID"] as int?
                    });
                }
                _connection.Close();
            }
            return tooded;
        }

        // Вставка новой записи в таблицу Toode
        public void Insert(Toode toode)
        {
            string query = "INSERT INTO Toode (Nimetus, Kogus, Hind, Pilt, LaoID) VALUES (@Nimetus, @Kogus, @Hind, @Pilt, @LaoID)";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@Nimetus", toode.Nimetus);
                cmd.Parameters.AddWithValue("@Kogus", toode.Kogus);
                cmd.Parameters.AddWithValue("@Hind", toode.Hind);
                cmd.Parameters.AddWithValue("@Pilt", toode.Pilt);
                cmd.Parameters.AddWithValue("@LaoID", toode.LaoID ?? (object)DBNull.Value);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        // Обновление записи в таблице Toode
        public void Update(Toode toode)
        {
            string query = "UPDATE Toode SET Nimetus = @Nimetus, Kogus = @Kogus, Hind = @Hind, Pilt = @Pilt, LaoID = @LaoID WHERE Id = @Id";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@Id", toode.Id);
                cmd.Parameters.AddWithValue("@Nimetus", toode.Nimetus);
                cmd.Parameters.AddWithValue("@Kogus", toode.Kogus);
                cmd.Parameters.AddWithValue("@Hind", toode.Hind);
                cmd.Parameters.AddWithValue("@Pilt", toode.Pilt);
                cmd.Parameters.AddWithValue("@LaoID", toode.LaoID ?? (object)DBNull.Value);

                _connection.Open();
                try { cmd.ExecuteNonQuery(); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                _connection.Close();
            }
        }

        // Удаление записи из таблицы Toode по ID
        public void Delete(int id)
        {
            string query = "DELETE FROM Toode WHERE Id = @Id";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }

}
