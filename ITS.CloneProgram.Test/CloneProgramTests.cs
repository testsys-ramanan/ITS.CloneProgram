using ITS.CloneProgram.Interfaces;
using Moq;

namespace ITS.CloneProgram.Tests
{
	public class CloneProgramTests
	{
		private readonly Mock<IUserRepository> _mockUserRepository;
		private readonly Services.CloneProgram _cloneProgram;

		public CloneProgramTests()
		{
			_mockUserRepository = new Mock<IUserRepository>();
			_cloneProgram = new Services.CloneProgram(_mockUserRepository.Object);
		}

		[Fact]
		public void Constructor_WithNullConnectionProvider_ThrowsArgumentNullException()
		{
			// Act & Assert
			var ex = Assert.Throws<ArgumentNullException>(
				() => new Services.CloneProgram((IConnectionStringProvider)null)
			);
			Assert.Equal("connectionProvider", ex.ParamName);
		}

		[Fact]
		public void ValidateUser_WithValidId_ReturnsTrue()
		{
			// Arrange
			_mockUserRepository.Setup(x => x.ValidateUser(It.IsAny<long>()))
				.Returns(true);

			// Act
			var result = _cloneProgram.ValidateUser(1);

			// Assert
			Assert.True(result);
			_mockUserRepository.Verify(x => x.ValidateUser(1), Times.Once);
		}

		[Fact]
		public void ValidateUser_WithInvalidId_ReturnsFalse()
		{
			// Act
			var result = _cloneProgram.ValidateUser(0);

			// Assert
			Assert.False(result);
			_mockUserRepository.Verify(x => x.ValidateUser(It.IsAny<long>()), Times.Never);
		}

		[Fact]
		public void ValidateUser_WhenRepositoryThrows_ThrowsInvalidOperation()
		{
			// Arrange
			_mockUserRepository.Setup(x => x.ValidateUser(It.IsAny<long>()))
				.Throws<Exception>();

			// Act & Assert
			var ex = Assert.Throws<InvalidOperationException>(
				() => _cloneProgram.ValidateUser(1) 
			);
			Assert.Equal("Error validating user", ex.Message);
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		public void ValidateUser_WithNonPositiveId_ReturnsFalse(long userId)
		{
			// Act
			var result = _cloneProgram.ValidateUser(userId);

			// Assert
			Assert.False(result);
		}
	}
}

