using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADLib_Plugin_Kernel
{
    public class InspectionManager : IInspectionManager
    {
        private readonly string _connectionString;

        public InspectionManager(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public DataTable GetAllInspections()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT Id, InspectionDate, InspectorName
                    FROM Inspections";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            }
        }

        public Inspection GetInspectionById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT Id, InspectionDate, InspectorName
                    FROM Inspections
                    WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Inspection
                            {
                                Id = (int)reader["Id"],
                                InspectionDate = reader["InspectionDate"].ToString(),
                                InspectorName = reader["InspectorName"].ToString()
                            };
                        }
                        throw new Exception($"Экспертиза с Id {id} не найдена.");
                    }
                }
            }
        }

        public void AddInspection(Inspection inspection)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    INSERT INTO Inspections (InspectionDate, InspectorName)
                    VALUES (@InspectionDate, @InspectorName)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InspectionDate", DateTime.Now); // Устанавливаем текущую дату
                    command.Parameters.AddWithValue("@InspectorName", inspection.InspectorName);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateInspection(Inspection inspection)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    UPDATE Inspections
                    SET InspectionDate = @InspectionDate,
                        InspectorName = @InspectorName
                    WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", inspection.Id);
                    command.Parameters.AddWithValue("@InspectionDate", DateTime.Now); // Обновляем дату
                    command.Parameters.AddWithValue("@InspectorName", inspection.InspectorName);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteInspection(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                // Обнуляем InspectionId в связанных записях Defects
                string updateDefectsQuery = "UPDATE Defects SET InspectionId = NULL WHERE InspectionId = @Id";
                using (var command = new SqlCommand(updateDefectsQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }

                // Затем удаляем запись из Inspections
                string deleteInspectionQuery = "DELETE FROM Inspections WHERE Id = @Id";
                using (var command = new SqlCommand(deleteInspectionQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}