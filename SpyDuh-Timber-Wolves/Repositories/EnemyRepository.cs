using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SpyDuh_Timber_Wolves.Models;

namespace SpyDuh_Timber_Wolves.Repositories
{
    public class EnemyRepository : BaseRepository
    {
        public EnemyRepository(IConfiguration configuration) : base(configuration) { }

        public List<Enemy> GetAll()
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select a.id as SpyId, a.[name] as SpyName, a.bio as SpyBio, b.id as EnemyId, b.[name] as EnemyName, b.bio as EnemyBio " +
                                            "FROM spy a " +
                                            "join enemy e on a.id = e.spyId " +
                                            "join spy b on e.enemyId = b.Id";
                    var enemies = new List<Enemy>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var enemy = new Enemy()
                        {
                            spyId = reader.GetInt32(reader.GetOrdinal("SpyId")),
                            enemyId = reader.GetInt32(reader.GetOrdinal("EnemyId"))
                        };

                        enemies.Add(enemy);
                    }

                    reader.Close();

                    return enemies;
                }
            }
        }

        public List<Enemy> GetByEnemyId(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"select a.id as SpyId, a.[name] as SpyName, a.bio as SpyBio, b.id as EnemyId, b.[name] as EnemyName, b.bio as EnemyBio
                                            FROM spy a 
                                            join enemy e on a.id = e.spyId
                                            join spy b on e.enemyId = b.Id 
                                            where a.id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    var enemies = new List<Enemy>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var enemy = new Enemy()
                        {
                            spyId = reader.GetInt32(reader.GetOrdinal("SpyId")),
                            enemyId = reader.GetInt32(reader.GetOrdinal("EnemyId"))
                        };
                        enemies.Add(enemy);
                    }
                    reader.Close();

                    return enemies;
                }
            }
        }

        public void Add(Enemy enemy)
        {
            using (var connection = Connection) 
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO Enemy (spyId, enemyId)
                                            OUTPUT INSERTED.ID
                                            VALUES (@spyId, @enemyId)";
                    command.Parameters.AddWithValue("spyId", enemy.spyId);
                    command.Parameters.AddWithValue("enemyId", enemy.enemyId);

                    enemy.Id = (int)command.ExecuteScalar();
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
                    command.CommandText = "DELETE FROM enemy WHERE Id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
