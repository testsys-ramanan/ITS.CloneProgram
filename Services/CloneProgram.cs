using ITS.CloneProgram.Interfaces;
using ITS.CloneProgram.Utilities;
using System;

namespace ITS.CloneProgram.Services
{
	public class CloneProgram : ICloneProgram
	{
		private readonly IUserRepository _userRepository;

		public CloneProgram(IConnectionStringProvider connectionProvider, IUserRepository userRepository = null)
		{
			if (connectionProvider == null)
				throw new ArgumentNullException(nameof(connectionProvider));

			_userRepository = userRepository ?? new UserRepository(connectionProvider.GetMasterConnectionString());
		}

		public CloneProgram(IUserRepository userRepository)
		{
			_userRepository = userRepository
				?? throw new ArgumentNullException(nameof(userRepository));
		}

		public bool ValidateUser(long userId)
		{
			if (userId <= 0)
			{
				return false;
			}

			try
			{
				return _userRepository.ValidateUser(userId);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error validating user", ex);
			}
		}
	}
}
