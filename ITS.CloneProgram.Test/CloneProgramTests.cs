using ITS.CloneProgram.Interfaces;
using Moq;

namespace ITS.CloneProgram.Tests
{
	public class CloneProgramTests
	{
		private readonly Mock<IUserRepository> _mockUserRepository;

		public CloneProgramTests()
		{
			_mockUserRepository = new Mock<IUserRepository>();
		}

		[Fact]
		public void ValidateUser_WithValidId_ReturnsTrue()
		{
			// Arrange
			_mockUserRepository.Setup(x => x.ValidateUser(It.IsAny<long>()))
				.Returns(true);

			var cloneProgram = new Services.CloneProgram(_mockUserRepository.Object);  
			// Act
			var result = cloneProgram.ValidateUser(1);

			// Assert
			Assert.True(result);
			_mockUserRepository.Verify(x => x.ValidateUser(1), Times.Once);
		}

		[Fact]
		public void ValidateUser_WithInvalidId_ReturnsFalse()
		{
			// Arrange
			var cloneProgram = new Services.CloneProgram(_mockUserRepository.Object);  

			// Act
			var result = cloneProgram.ValidateUser(0);

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
			var cloneProgram = new Services.CloneProgram(_mockUserRepository.Object);  

			// Act & Assert
			var ex = Assert.Throws<InvalidOperationException>(
				() => cloneProgram.ValidateUser(1) 
			);
			Assert.Equal("Error validating user", ex.Message);
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		public void ValidateUser_WithNonPositiveId_ReturnsFalse(long userId)
		{
			// Arrange
			var cloneProgram = new Services.CloneProgram(_mockUserRepository.Object);  

			// Act
			var result = cloneProgram.ValidateUser(userId);

			// Assert
			Assert.False(result);
		}
	}
}

