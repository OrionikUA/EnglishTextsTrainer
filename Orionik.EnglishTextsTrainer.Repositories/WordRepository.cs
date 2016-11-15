using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orionik.EnglishTextsTrainer.Logger;
using Orionik.EnglishTextsTrainer.Models;

namespace Orionik.EnglishTextsTrainer.Repositories
{
    public class WordRepository : BaseRepository, IWordRepository, IRepository<Word>
    {
        public WordRepository(string connection) : base(connection)
        {
        }

        public List<Word> GetList()
        {
            Logging.Instance.Write(typeof(WordRepository), "Start GetList");
            var wordList = new List<Word>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var sqlCommand = new SqlCommand(Procedures.SpGetWords, connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            wordList.Add( new Word
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Meanings = (string)reader["Meanings"],
                                Ignore = (bool)reader["Ignored"],
                                Know = (bool)reader["Know"]
                            });
                        }
                    }
                }
            }
            Logging.Instance.Write(typeof(WordRepository), "End GetList");
            return wordList;
        }

        public Word Insert(Word item)
        {
            Logging.Instance.Write(typeof(WordRepository), "Start Insert");
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var sqlCommand = new SqlCommand(Procedures.SpAddNewWord, connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = item.Name;
                    sqlCommand.Parameters.Add("@Meanings", SqlDbType.NVarChar).Value = item.Meanings;
                    sqlCommand.Parameters.Add("@Ignored", SqlDbType.Bit).Value = item.Ignore;
                    sqlCommand.Parameters.Add("@Know", SqlDbType.Bit).Value = item.Know;
                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();
                    item.Id = (int) sqlCommand.Parameters["@Id"].Value;
                }
            }
            Logging.Instance.Write(typeof(WordRepository), "End Insert");
            return item;
        }

        public void Update(Word item)
        {
            Logging.Instance.Write(typeof(WordRepository), "Start Update");
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var sqlCommand = new SqlCommand(Procedures.SpUpdateWord, connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = item.Name;
                    sqlCommand.Parameters.Add("@Meanings", SqlDbType.NVarChar).Value = item.Meanings;
                    sqlCommand.Parameters.Add("@Ignored", SqlDbType.Bit).Value = item.Ignore;
                    sqlCommand.Parameters.Add("@Know", SqlDbType.Bit).Value = item.Know;
                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = item.Id;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            Logging.Instance.Write(typeof(WordRepository), "End Update");
        }

        public void Delete(int id)
        {
            Logging.Instance.Write(typeof(WordRepository), "Start Delete");
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var sqlCommand = new SqlCommand(Procedures.SpDeleteWord, connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            Logging.Instance.Write(typeof(WordRepository), "End Delete");
        }
    }
}
