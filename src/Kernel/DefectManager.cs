using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADLib_Plugin_Kernel
{
    public class DefectManager : IDefectManager
    {
        private readonly string _connectionString;

        public DefectManager(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public DataTable GetDefectsByObject(int idObject)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Defects WHERE idObject = @idObject";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idObject", idObject);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            }
        }

        public Defect GetDefectById(int defectId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Defects WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", defectId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Defect
                            {
                                Id = (int)reader["Id"],
                                IdObject = (int)reader["idObject"],
                                DefectNumber = (int)reader["DefectNumber"],
                                Location = reader["Location"].ToString(),
                                Description = reader["Description"].ToString(),
                                DangerCategory = reader["DangerCategory"].ToString(),
                                Document = reader["Document"] != DBNull.Value ? (byte[])reader["Document"] : null,
                                Photo = reader["Photo"] != DBNull.Value ? (byte[])reader["Photo"] : null,
                                Recommendation = reader["Recommendation"].ToString(),
                                InspectionId = reader["InspectionId"] != DBNull.Value ? (int?)reader["InspectionId"] : null
                            };
                        }
                        throw new Exception($"Дефект с Id {defectId} не найден.");
                    }
                }
            }
        }

        public void AddDefect(Defect defect)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    INSERT INTO Defects (idObject, DefectNumber, Location, Description, DangerCategory, Document, Photo, Recommendation)
                    VALUES (@IdObject, 0, @Location, @Description, @DangerCategory, @Document, @Photo, @Recommendation)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdObject", defect.IdObject);
                    command.Parameters.AddWithValue("@Location", defect.Location);
                    command.Parameters.AddWithValue("@Description", (object)defect.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DangerCategory", defect.DangerCategory);
                    command.Parameters.Add("@Document", SqlDbType.VarBinary).Value = (object)defect.Document ?? DBNull.Value;
                    command.Parameters.Add("@Photo", SqlDbType.VarBinary).Value = (object)defect.Photo ?? DBNull.Value;
                    command.Parameters.AddWithValue("@Recommendation", (object)defect.Recommendation ?? DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateDefect(Defect defect)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    UPDATE Defects
                    SET Location = @Location,
                        Description = @Description,
                        DangerCategory = @DangerCategory,
                        Document = @Document,
                        Photo = @Photo,
                        Recommendation = @Recommendation
                    WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", defect.Id);
                    command.Parameters.AddWithValue("@Location", defect.Location);
                    command.Parameters.AddWithValue("@Description", (object)defect.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DangerCategory", defect.DangerCategory);
                    command.Parameters.Add("@Document", SqlDbType.VarBinary).Value = (object)defect.Document ?? DBNull.Value;
                    command.Parameters.Add("@Photo", SqlDbType.VarBinary).Value = (object)defect.Photo ?? DBNull.Value;
                    command.Parameters.AddWithValue("@Recommendation", (object)defect.Recommendation ?? DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteDefect(int defectId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Defects WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", defectId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public byte[] GetDocument(int defectId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Document FROM Defects WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", defectId);
                    var result = command.ExecuteScalar();
                    return result != DBNull.Value ? (byte[])result : null;
                }
            }
        }

        public byte[] GetPhoto(int defectId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Photo FROM Defects WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", defectId);
                    var result = command.ExecuteScalar();
                    return result != DBNull.Value ? (byte[])result : null;
                }
            }
        }
    }
}

