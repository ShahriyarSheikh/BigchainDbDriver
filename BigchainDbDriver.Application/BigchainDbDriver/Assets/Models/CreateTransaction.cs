using System.Collections.Generic;

namespace BigchainDbDriver.Assets.Models
{
	public class CreateTransaction
	{
		public dynamic Model { get; set; }
		public dynamic Metadata { get; set; }
		public List<dynamic> Output { get; set; }
		public string PubKey { get; set; }
	}
}
