using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SpyDuh_Timber_Wolves.Models;

namespace SpyDuh_Timber_Wolves.Repositories
{
    public class FriendRepository : BaseRepository
    {
        public FriendRepository(IConfiguration configuration) : base(configuration) { }
        public List<Friend> GetAll()
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select a.id as SpyId ,a.[name] as SpyName ,a.bio as SpyBio ,b.id as FriendId ,b.[name] as FriendName ,b.bio as FriendBio From spy a join friend f on a.id = f.spyId " +
                        "join spy b on f.friendId = b.id";
                    var friends = new List<Friend>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var friend = new Friend()
                        {
                            spyId = reader.GetInt32(reader.GetOrdinal("SpyId")),
                            friendId = reader.GetInt32(reader.GetOrdinal("FriendId"))
                        };

                        friends.Add(friend);
                    }

                    reader.Close();

                    return friends;
                }
            }
        }

        public List<Friend> GetByFriendId(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"select a.id as SpyId, a.[name] as SpyName, a.bio as SpyBio ,b.id as FriendId, b.[name] as FriendName, b.bio as FriendBio " +
                                            "From spy a join friend f on a.id = f.spyId join spy b on f.friendId = b.id where a.id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    var friends = new List<Friend>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var friend = new Friend()
                        {
                            spyId = reader.GetInt32(reader.GetOrdinal("SpyId")),
                            friendId = reader.GetInt32(reader.GetOrdinal("FriendId"))
                        };
                        friends.Add(friend);
                    }
                    reader.Close();

                    return friends;
                }
            }
        }

        public void Add(Friend friend)
        {
            using (var connection = Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO Friend (spyId, friendId)
                                            OUTPUT INSERTED.ID
                                            VALUES (@spyId, @friendId)";
                    command.Parameters.AddWithValue("spyId", friend.spyId);
                    command.Parameters.AddWithValue("friendId", friend.friendId);

                    friend.Id = (int)command.ExecuteScalar();
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
                    command.CommandText = "DELETE FROM enemy Where Id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
