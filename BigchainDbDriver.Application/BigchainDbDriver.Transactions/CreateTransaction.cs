using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BigchainDbDriver.Transactions
{
	public class CreateTransaction
	{
		public CreateTransaction()
		{

		}
		public dynamic MakeCreateTransaction(dynamic assets,dynamic metadata, dynamic outputs, List<string> issuers) {
			var assetsDefinition = new AssetsModel {
				Assets = assets ?? null
			};
			var _inputs = makeInputTemplate( issuers);

			return MakeTrasnsaction("CREATE", assetsDefinition, metadata, outputs, _inputs);

		}

		private TxTemplate MakeTrasnsaction(dynamic operation, dynamic assets, dynamic metadata = null, List<dynamic> outputs = null, List<InputTemplate> inputs = null) {
			var tx = MakeTrasactionTemplate();
			tx.Operation = operation;
			tx.Asset = assets;
			tx.Metadata = metadata;
			tx.Inputs = inputs;
			tx.Outputs = outputs;
			return tx;
		}

		private List<InputTemplate> makeInputTemplate(List<string> publicKeys, string fulfills = null, string fulfillment = null)
		{
			var listOfInputTemplates = new List<InputTemplate>();
			foreach (var temp in publicKeys) {
				listOfInputTemplates.Add(new InputTemplate
				{
					Fulfilment = fulfillment,
					Fulfills = fulfills,
					Owners_before = publicKeys,
				});
			}
			return listOfInputTemplates;
		}

		private TxTemplate MakeTrasactionTemplate() {
			return new TxTemplate
			{
				Id = null,
				Operation = null,
				Outputs = new List<dynamic>(),
				Inputs = new List<InputTemplate>(),
				Metadata = null,
				Asset = null,
				Version = "2.0"
			};
		}
	}

	public class InputTemplate {
		public string Fulfilment { get; set; }
		public string Fulfills { get; set; }
		[JsonProperty("owners_before")]
		public List<string> Owners_before { get; set; }
	}

	public class AssetsModel {
		[JsonProperty("data")]
		public string Assets { get; set; }
	}

	public class TxTemplate {
		[JsonProperty("id")]
		public dynamic Id { get; set; }
		[JsonProperty("operation")]
		public dynamic Operation { get; set; }
		[JsonProperty("outputs")]
		public List<dynamic> Outputs { get; set; }
		[JsonProperty("inputs")]
		public List<InputTemplate> Inputs { get; set; }
		[JsonProperty("metadata")]
		public dynamic Metadata { get; set; }
		[JsonProperty("asset")]
		public dynamic Asset { get; set; }
		[JsonProperty("version")]
		public string Version { get; set; }
	}
}
