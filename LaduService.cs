using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoeAndmebaas
{
    public class LaduService
    {
        private readonly DataAccess dataAccess;

        public LaduService(string connectionString)
        {
            dataAccess = new DataAccess(connectionString);
        }

        public List<Ladu> GetAllLaod()
        {
            DataTable table = dataAccess.ExecuteQuery("SELECT * FROM Ladu");
            return table.AsEnumerable().Select(row => new Ladu
            {
                Id = row.Field<int>("Id"),
                LaoNimetus = row.Field<string>("LaoNimetus"),
                Suurus = row.Field<string>("Suurus"),
                Kirjeldus = row.Field<string>("Kirjeldus")
            }).ToList();
        }

        public void AddLadu(Ladu ladu)
        {
            string query = "INSERT INTO Ladu (LaoNimetus, Suurus, Kirjeldus) VALUES (@LaoNimetus, @Suurus, @Kirjeldus)";
            var parameters = new Dictionary<string, object>
        {
            { "@LaoNimetus", ladu.LaoNimetus },
            { "@Suurus", ladu.Suurus },
            { "@Kirjeldus", ladu.Kirjeldus }
        };
            dataAccess.ExecuteNonQuery(query, parameters);
        }

        public void UpdateLadu(Ladu ladu)
        {
            string query = "UPDATE Ladu SET LaoNimetus = @LaoNimetus, Suurus = @Suurus, Kirjeldus = @Kirjeldus WHERE Id = @Id";
            var parameters = new Dictionary<string, object>
        {
            { "@LaoNimetus", ladu.LaoNimetus },
            { "@Suurus", ladu.Suurus },
            { "@Kirjeldus", ladu.Kirjeldus },
            { "@Id", ladu.Id }
        };
            dataAccess.ExecuteNonQuery(query, parameters);
        }

        public void DeleteLadu(int laduId)
        {
            string query = "DELETE FROM Ladu WHERE Id = @Id";
            var parameters = new Dictionary<string, object>
        {
            { "@Id", laduId }
        };
            dataAccess.ExecuteNonQuery(query, parameters);
        }
    }


}
