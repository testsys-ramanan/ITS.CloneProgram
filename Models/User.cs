namespace ITS.CloneProgram.Models
{
	public class User
	{
		public long UserID { get; internal set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string Middle { get; set; }
		public string LastName { get; set; }
		public bool Active { get; internal set; }
		public bool AllPrograms { get; set; }
		public bool Deleted { get; set; }
	}
}
