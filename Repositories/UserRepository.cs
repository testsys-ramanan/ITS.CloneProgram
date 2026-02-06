using Dapper;
using ITS.CloneProgram.Interfaces;
using ITS.CloneProgram.Models;
using System.Data;
using System.Data.SqlClient;

namespace ITS.CloneProgram.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly string _connectionString;

		public UserRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public bool ValidateUser(long userId)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var result = db.QuerySingleOrDefault<User>
					("SELECT * FROM Users WHERE UserId = @UserId", new { UserId = userId });
				return result != null;
			}
		}
	}
}
