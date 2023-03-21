using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SpyDuh_Timber_Wolves.Models;

namespace SpyDuh_Timber_Wolves.Repositories
{
    public class SpySkillsRepository : BaseRepository
    {
        public SpySkillsRepository(IConfiguration configuration) : base(configuration) { }

        public List<SpySkills> GetAll()
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT spy.id as SpyId, spy.[name], spy.bio, spySkills.id as Id, spySkills.skillName, spySkills.skillLevel" +
                        "FROM spy" +
                        "JOIN spySkills on spy.id = spySkills.spyId";
                    var reader = command.ExecuteReader();
                    var skills = new List<SpySkills>();
                    while (reader.Read())
                    {
                        var skill = new SpySkills()
                        {
                            id = reader.GetInt32(reader.GetOrdinal("Id")),
                            skillName = reader.GetString(reader.GetOrdinal("skillName")),
                            skillLevel = reader.GetInt32(reader.GetOrdinal("skillLevel")),
                            spyId = reader.GetInt32(reader.GetOrdinal("SpyId")),                           
                        };

                        skills.Add(skill);
                    }

                    reader.Close();

                    return skills;
                }
            }
        }

        public SpySkills GetById(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT spy.id as SpyId, spy.[name], spy.bio, spySkills.id as Id, spySkills.skillName, spySkills.skillLevel" +                      
                        "FROM spy" + 
                        "JOIN spySkills on spy.id = spySkills.spyId WHERE spy.id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    var reader = command.ExecuteReader();

                    SpySkills skills = null;
                    if (reader.Read())
                    {
                        skills = new SpySkills()
                        {
                            id = reader.GetInt32(reader.GetOrdinal("Id")),
                            skillName = reader.GetString(reader.GetOrdinal("skillName")),
                            skillLevel = reader.GetInt32(reader.GetOrdinal("skillLevel")),
                            spyId = reader.GetInt32(reader.GetOrdinal("SpyId")),                            
                        };
                    }
                    reader.Close();

                    return skills;
                }
            }
        }

        public void Add(SpySkills skills)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                            INSERT INTO SpySkills (skillName, skillLevel)
                            OUTPUT INSERTED.ID
                            VALUES (@skillName, @skillLevel)";
                    command.Parameters.AddWithValue("skillName", skills.skillName);
                    command.Parameters.AddWithValue("skillLevel", skills.skillLevel);

                    skills.id = (int)command.ExecuteScalar();
                }
            }
        }

        public void Update(SpySkills skills)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                            UPDATE spySkills
                            SET skillName = @skillName, skillLevel = @skillLevel
                            WHERE id = @id";
                    command.Parameters.AddWithValue("@id", skills.id);
                    command.Parameters.AddWithValue("@skillName", skills.skillName);
                    command.Parameters.AddWithValue("@skillLevel", skills.skillLevel);

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
                    command.CommandText = "DELETE FROM spySkills Where Id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
