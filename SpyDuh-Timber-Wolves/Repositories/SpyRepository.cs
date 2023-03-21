using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SpyDuh_Timber_Wolves.Models;

namespace SpyDuh_Timber_Wolves.Repositories
{
    public class SpyRepository : BaseRepository, ISpyRepository
    {

        public SpyRepository(IConfiguration configuration) : base(configuration) { }

        public List<Spy> GetAll()
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT spy.id as Id, spy.name as Name, spy.bio as Bio, spySkills.id as skillId, spySkills.skillName, spySkills.skillLevel, spyServices.id as serviceId, spyServices.serviceName, spyServices.price FROM spy JOIN spySkills on spy.id = spySkills.spyId JOIN spyServices on spy.id = spyServices.spyId";
                    var reader = command.ExecuteReader();
                    var spies = new List<Spy>();
                    while (reader.Read())
                    {
                        var spy = new Spy()
                        {
                            id = reader.GetInt32(reader.GetOrdinal("Id")),
                            name = reader.GetString(reader.GetOrdinal("Name")),
                            bio = reader.GetString(reader.GetOrdinal("Bio")),
                            spySkills = new List <SpySkills>(),                           
                            spyServices = new List <SpyServices>(),
                        };
                        spy.spySkills.Add(new SpySkills()
                        {

                            id = reader.GetInt32(reader.GetOrdinal("skillId")),
                            skillName = reader.GetString(reader.GetOrdinal("skillName")),
                            skillLevel = reader.GetInt32(reader.GetOrdinal("skillLevel")),
                            spyId = reader.GetInt32(reader.GetOrdinal("Id"))
                        });
                        spy.spyServices.Add(new SpyServices()
                        {

                            id = reader.GetInt32(reader.GetOrdinal("skillId")),
                            serviceName = reader.GetString(reader.GetOrdinal("serviceName")),
                            price = reader.GetInt32(reader.GetOrdinal("price")),
                            spyId = reader.GetInt32(reader.GetOrdinal("Id"))
                        });

                        spies.Add(spy);
                    }
                    
                    reader.Close();

                    return spies;
                }
            }
        }

        public Spy GetById(int Id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT spy.id as Id, spy.name as Name, spy.bio as Bio, spySkills.id as skillId, spySkills.skillName, spySkills.skillLevel, spyServices.id as serviceId, spyServices.serviceName, spyServices.price FROM spy JOIN spySkills on spy.id = spySkills.spyId JOIN spyServices on spy.id = spyServices.spyId WHERE spy.id = @id";

                    command.Parameters.AddWithValue("@id", Id);

                    var reader = command.ExecuteReader();

                    Spy spy = null;
                    if (reader.Read())
                    {
                        spy = new Spy()
                        {
                            id = reader.GetInt32(reader.GetOrdinal("Id")),
                            name = reader.GetString(reader.GetOrdinal("Name")),
                            bio = reader.GetString(reader.GetOrdinal("Bio")),
                            spySkills = new List <SpySkills>(),
                            spyServices = new List <SpyServices>(),
                        };
                        spy.spySkills.Add(new SpySkills()
                        {

                            id = reader.GetInt32(reader.GetOrdinal("skillId")),
                            skillName = reader.GetString(reader.GetOrdinal("skillName")),
                            skillLevel = reader.GetInt32(reader.GetOrdinal("skillLevel")),
                            spyId = reader.GetInt32(reader.GetOrdinal("Id"))
                        });
                        spy.spyServices.Add(new SpyServices()
                        {

                            id = reader.GetInt32(reader.GetOrdinal("skillId")),
                            serviceName = reader.GetString(reader.GetOrdinal("serviceName")),
                            price = reader.GetInt32(reader.GetOrdinal("price")),
                            spyId = reader.GetInt32(reader.GetOrdinal("Id"))
                        });
                    }
                    reader.Close();

                    return spy;
                }
            }
        }

        public void Add(Spy spy)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                            INSERT INTO Spy (name, bio)
                            OUTPUT INSERTED.ID
                            VALUES (@name, @bio)";
                    command.Parameters.AddWithValue("name", spy.name);
                    command.Parameters.AddWithValue("bio", spy.bio);

                    spy.id = (int)command.ExecuteScalar();
                }
            }
        }

        public void Update(Spy spy)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                            UPDATE spy
                            SET name = @name, bio = @bio
                            WHERE id = @id";
                    command.Parameters.AddWithValue("@id", spy.id);
                    command.Parameters.AddWithValue("@name", spy.name);
                    command.Parameters.AddWithValue("@bio", spy.bio);

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
                    command.CommandText = "DELETE FROM spy Where Id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
