using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SpyDuh_Timber_Wolves.Models;

namespace SpyDuh_Timber_Wolves.Repositories
{
    public class SpySkillsRepository : BaseRepository, ISpySkillsRepository
    {
        public SpySkillsRepository(IConfiguration configuration) : base(configuration) { }

        public List<SpySkills> GetAll()
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT id, skillName as SkillName, skillLevel as SkillLevel, spyId as SpyId FROM spySkills";
                    var reader = command.ExecuteReader();
                    var skills = new List<SpySkills>();
                    while (reader.Read())
                    {
                        var skill = new SpySkills()
                        {
                            id = reader.GetInt32(reader.GetOrdinal("Id")),
                            skillName = reader.GetString(reader.GetOrdinal("SkillName")),
                            skillLevel = reader.GetInt32(reader.GetOrdinal("SkillLevel")),
                            spyId = reader.GetInt32(reader.GetOrdinal("SpyId")),
                        };

                        skills.Add(skill);
                    }

                    reader.Close();

                    return skills;
                }
            }
        }

        public List<SpySkills> GetBySpyId(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT spy.id as SpyId, spy.[name], spy.bio, spySkills.id as Id, spySkills.skillName, spySkills.skillLevel FROM spy JOIN spySkills on spy.id = spySkills.spyId WHERE spy.id = @spyId";
                    command.Parameters.AddWithValue("@spyId", id);
                    var skills = new List<SpySkills>();
                    var reader = command.ExecuteReader();

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

        public SpySkills GetBySkillId(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT spy.id as SpyId, spy.[name], spy.bio, spySkills.id as Id, spySkills.skillName, spySkills.skillLevel FROM spy JOIN spySkills on spy.id = spySkills.spyId WHERE spySkills.id = @skillId";
                    command.Parameters.AddWithValue("@skillId", id);
                    var reader = command.ExecuteReader();

                    SpySkills skill = null;
                    if (reader.Read())
                    {
                        skill = new SpySkills()
                        {
                            id = reader.GetInt32(reader.GetOrdinal("Id")),
                            skillName = reader.GetString(reader.GetOrdinal("skillName")),
                            skillLevel = reader.GetInt32(reader.GetOrdinal("skillLevel")),
                            spyId = reader.GetInt32(reader.GetOrdinal("SpyId")),
                        };
                    }
                    reader.Close();

                    return skill;
                }
            }
        }

        public List<SpySkills> GetBySkillName(string skillName)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT spy.id as SpyId, spy.[name], spy.bio, spySkills.id as Id, spySkills.skillName, spySkills.skillLevel FROM spy JOIN spySkills on spy.id = spySkills.spyId WHERE spySkills.skillName = @skillName";
                    command.Parameters.AddWithValue("@skillName", skillName);
                    var skills = new List<SpySkills>();
                    var reader = command.ExecuteReader();


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

        public void Add(SpySkills skills)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                            INSERT INTO SpySkills (skillName, skillLevel, spyId)
                            OUTPUT INSERTED.ID
                            VALUES (@skillName, @skillLevel, @spyId)";
                    command.Parameters.AddWithValue("skillName", skills.skillName);
                    command.Parameters.AddWithValue("skillLevel", skills.skillLevel);
                    command.Parameters.AddWithValue("spyId", skills.spyId);

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
