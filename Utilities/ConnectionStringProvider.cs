using ITS.CloneProgram.Interfaces;
using ITS.Configuration30;
using System.Data;
using System.Data.SqlClient;

namespace ITS.CloneProgram.Utilities
{
	public class ConnectionStringProvider : IConnectionStringProvider
	{
		public string GetMasterConnectionString()
		{
			IDbConnection masterConnection = new SqlConnection
				(ConnectionManagement.GetConnectionString(DatabaseType.Master));

			return masterConnection.ConnectionString;
		}
	}
}
