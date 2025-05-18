using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace CADLib_Plugin_Kernel
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly string _connectionString;

        public DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void CheckTablesExist(out bool defectsExists, out bool inspectionsExists)
        {
            defectsExists = false;
            inspectionsExists = false;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Проверяем наличие таблицы Inspections
                string checkInspectionsQuery = @"
                    SELECT COUNT(*) 
                    FROM INFORMATION_SCHEMA.TABLES 
                    WHERE TABLE_NAME = 'Inspections'";

                using (var command = new SqlCommand(checkInspectionsQuery, connection))
                {
                    int count = (int)command.ExecuteScalar();
                    inspectionsExists = count > 0;
                }

                // Проверяем наличие таблицы Defects
                string checkDefectsQuery = @"
                    SELECT COUNT(*) 
                    FROM INFORMATION_SCHEMA.TABLES 
                    WHERE TABLE_NAME = 'Defects'";

                using (var command = new SqlCommand(checkDefectsQuery, connection))
                {
                    int count = (int)command.ExecuteScalar();
                    defectsExists = count > 0;
                }

                connection.Close();
            }
        }

        public void CreateTables()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Создаем таблицу Inspections, если она не существует
                string createInspectionsQuery = @"
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Inspections')
                    BEGIN
                        CREATE TABLE Inspections (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            InspectorName NVARCHAR(100) NOT NULL,
                            InspectionDate DATETIME NOT NULL DEFAULT GETDATE()
                        );
                        CREATE INDEX IX_Inspections_InspectionDate ON Inspections(InspectionDate);
                    END";

                using (var command = new SqlCommand(createInspectionsQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Создаем таблицу Defects, если она не существует
                string createDefectsQuery = @"
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Defects')
                    BEGIN
                        CREATE TABLE Defects (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            idObject INT NOT NULL,
                            DefectNumber INT NOT NULL,
                            Location NVARCHAR(100),
                            Description NVARCHAR(1000),
                            DangerCategory CHAR(1) NOT NULL CHECK (DangerCategory IN ('А', 'Б', 'В')),
                            Document VARBINARY(MAX),
                            Photo VARBINARY(MAX),
                            Recommendation NVARCHAR(1000),
                            InspectionId INT,
                            CONSTRAINT FK_Defects_ObjectsShadow FOREIGN KEY (idObject) 
                                REFERENCES ObjectsShadow(idObject),
                            CONSTRAINT FK_Defects_Inspections FOREIGN KEY (InspectionId) 
                                REFERENCES Inspections(Id)
                        );
                        CREATE INDEX IX_Defects_idObject ON Defects(idObject);

                        -- Триггер для автоматического назначения DefectNumber
                        EXEC('
                            CREATE TRIGGER TRG_SetDefectNumber
                            ON Defects
                            AFTER INSERT
                            AS
                            BEGIN
                                SET NOCOUNT ON;
                                UPDATE d
                                SET DefectNumber = (
                                    SELECT ISNULL(MAX(DefectNumber), 0) + 1
                                    FROM Defects
                                    WHERE idObject = d.idObject
                                    AND Id != d.Id
                                )
                                FROM Defects d
                                INNER JOIN inserted i ON d.Id = i.Id
                                WHERE d.DefectNumber IS NULL OR d.DefectNumber = 0;
                            END
                        ');
                    END";

                using (var command = new SqlCommand(createDefectsQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
