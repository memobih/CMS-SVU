namespace Data_Access_Layer.Reposatory.Entities
{
	public class Control_Addresses
	{
	
		public int ID { get; set; }
		public int ControlID { get; set; }
		public Control Control { get; set; }
		public string Address { get; set; }

		
	}
}
