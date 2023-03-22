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
                    command.CommandText = "SELECT id, serviceName as ServiceName, price as Price, spyId as SpyId  FROM spyServices";
                    var reader = command.ExecuteReader();
                    var services = new List<SpyServices>();
                    while (reader.Read())
                    {
                        var service = new SpyServices()
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            serviceName = reader.GetString(reader.GetOrdinal("ServiceName")),
                            price = reader.GetInt32(reader.GetOrdinal("Price")),
                            spyId = reader.GetInt32(reader.GetOrdinal("spyId")),
                        };

                        services.Add(service);
                    }

                    reader.Close();

                    return services;
                }
            }
        }

        public List<SpyServices> GetBySpyId(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT spy.id as SpyId, spy.[name], spy.bio, spyServices.id as Id, spyServices.serviceName, spyServices.price FROM spy JOIN spyServices on spy.id = spyServices.spyId WHERE spy.id = @spyId";
                    command.Parameters.AddWithValue("@spyId", id);
                    var services = new List<SpyServices>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var service = new SpyServices()
                        {
                            id = reader.GetInt32(reader.GetOrdinal("Id")),
                            serviceName = reader.GetString(reader.GetOrdinal("serviceName")),
                            price = reader.GetInt32(reader.GetOrdinal("price")),
                            spyId = reader.GetInt32(reader.GetOrdinal("spyId")),
                        };
                        services.Add(service);
                    }
                    reader.Close();

                    return services;
                }
            }
        }

        public SpyServices GetByServiceId(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT spy.id as SpyId, spy.[name], spy.bio, spyServices.id as Id, spyServices.serviceName, spyServices.price FROM spy JOIN spyServices on spy.id = spyServices.spyId WHERE spyServices.id = @serviceId";
                    command.Parameters.AddWithValue("@serviceId", id);
                    var reader = command.ExecuteReader();

                    SpyServices service = null;
                    if (reader.Read())
                    {
                        service = new SpyServices()
                        {
                            id = reader.GetInt32(reader.GetOrdinal("Id")),
                            serviceName = reader.GetString(reader.GetOrdinal("serviceName")),
                            price = reader.GetInt32(reader.GetOrdinal("price")),
                            spyId = reader.GetInt32(reader.GetOrdinal("spyId")),
                        };
                    }
                    reader.Close();

                    return service;
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
