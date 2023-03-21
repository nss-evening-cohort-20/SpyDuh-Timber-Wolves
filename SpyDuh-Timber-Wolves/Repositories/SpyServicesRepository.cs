using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SpyDuh_Timber_Wolves.Models;

namespace SpyDuh_Timber_Wolves.Repositories
{
    public class SpyServicesRepository : BaseRepository, ISpyServicesRepository
    {
        public SpyServicesRepository(IConfiguration configuration) : base(configuration) { }

        public List<SpyServices> GetAll()
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT spy.id as SpyId, spy.[name], spy.bio, spyServices.id as Id, spyServices.serviceName, spyServices.price" +
                        "FROM spy" +
                        "JOIN spyServices on spy.id = spyServices.spyId";
                    var reader = command.ExecuteReader();
                    var services = new List<SpyServices>();
                    while (reader.Read())
                    {
                        var service = new SpyServices()
                        {
                            id = reader.GetInt32(reader.GetOrdinal("Id")),
                            serviceName = reader.GetString(reader.GetOrdinal("serviceName")),
                            price = reader.GetInt32(reader.GetOrdinal("price")),
                            spyId = reader.GetInt32(reader.GetOrdinal("SpyId")),
                        };

                        services.Add(service);
                    }

                    reader.Close();

                    return services;
                }
            }
        }

        public SpyServices GetById(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT spy.id as SpyId, spy.[name], spy.bio, spyServices.id as Id, spyServices.serviceName, spyServices.price" +
                        "FROM spy" +
                        "JOIN spyServices on spy.id = spyServices.spyId WHERE spy.id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    var reader = command.ExecuteReader();

                    SpyServices services = null;
                    if (reader.Read())
                    {
                        services = new SpyServices()
                        {
                            id = reader.GetInt32(reader.GetOrdinal("Id")),
                            serviceName = reader.GetString(reader.GetOrdinal("skillName")),
                            price = reader.GetInt32(reader.GetOrdinal("skillLevel")),
                            spyId = reader.GetInt32(reader.GetOrdinal("SpyId")),
                        };
                    }
                    reader.Close();

                    return services;
                }
            }
        }

        public void Add(SpyServices services)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                            INSERT INTO SpyServices (serviceName, price)
                            OUTPUT INSERTED.ID
                            VALUES (@serviceName, @price)";
                    command.Parameters.AddWithValue("serviceName", services.serviceName);
                    command.Parameters.AddWithValue("price", services.price);

                    services.id = (int)command.ExecuteScalar();
                }
            }
        }

        public void Update(SpyServices services)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                            UPDATE spyservices
                            SET serviceName = @serviceName, price = @price
                            WHERE id = @id";
                    command.Parameters.AddWithValue("@id", services.id);
                    command.Parameters.AddWithValue("@serviceName", services.serviceName);
                    command.Parameters.AddWithValue("@price", services.price);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM spyServices Where Id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
