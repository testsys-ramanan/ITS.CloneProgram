using ITS.CloneProgram.Interfaces;
using ITS.Configuration30;

namespace ITS.CloneProgram.Utilities
{
	public class ConnectionStringProvider : IConnectionStringProvider
	{
		public string GetMasterConnectionString()
		{
			return ConnectionManagement.GetConnectionString(DatabaseType.Master);
		}
	}
}
