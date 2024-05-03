namespace Business_Logic_Layer.Models
{
	public class Control_AddressesModel
	{
	
		public int ID { get; set; }
		public int ControlID { get; set; }
		public ControlModel Control { get; set; }
		public string Address { get; set; }

		
	}
}
