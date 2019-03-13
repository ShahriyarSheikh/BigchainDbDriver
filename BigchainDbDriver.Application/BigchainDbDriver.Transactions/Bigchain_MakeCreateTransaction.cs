﻿using System;
using System.Collections.Generic;
using System.Text;
using BigchainDbDriver.Assets.Models.TransactionModels;
using NBitcoin.DataEncoders;
using Newtonsoft.Json;

namespace BigchainDbDriver.Transactions
{
	public class Bigchain_MakeCreateTransaction
	{
        private readonly DataEncoder encoder;
        public Bigchain_MakeCreateTransaction()
        {
             encoder = Encoders.Base58;
        }

        /// <summary>
        /// Creates a transaction object to be pushed to bigchaindb
        /// </summary>
        /// <param name="assets"> dynamic object that can hold anything</param>
        /// <param name="metadata"> dynamic object that can hold anything</param>
        /// <param name="outputs"></param>
        /// <param name="issuers"></param>
        /// <returns></returns>
		public TxTemplate MakeCreateTransaction(Asset assets,dynamic metadata, List<Output> outputs, List<string> issuers) {
			var assetsDefinition = new AssetDefinition {
				Data = assets.Assets.Data ?? null
			};
			var _inputs = makeInputTemplate( issuers);

			return MakeTrasnsaction("CREATE", 
                assetsDefinition, 
                metadata, 
                outputs, 
                _inputs);

		}

		private TxTemplate MakeTrasnsaction(string operation, dynamic assets, dynamic metadata = null, List<Output> outputs = null, List<InputTemplate> inputs = null) {
			var tx = MakeTrasactionTemplate();
			tx.Operation = operation;
			tx.Asset = assets;
			tx.Metadata = metadata;
			tx.Inputs = inputs;
			tx.Outputs = outputs;
			return tx;
		}

        public List<Output> MakeOutput(MakeEd25519Condition condition, string amount = "1")
        {
            IList<string> pubKeys = new List<string>();

            if (condition.Details.Type == "ed25519-sha-256") {
                pubKeys.Add(condition.Details.PublicKey);
            }
            var outputs = new List<Output>();
             outputs.Add(new Output
            {
                Amount = amount,
                Condition = condition,
                PublicKeys = pubKeys
            });

            return outputs;

        }

        public MakeEd25519Condition MakeEd25519Condition(string pubkey) {

            return new MakeEd25519Condition {
                Details = new Details {
                    PublicKey = pubkey,
                    Type = "ed25519-sha-256"
                },
                Uri = ""
            };
        }

		private List<InputTemplate> makeInputTemplate(List<string> publicKeys, string fulfills = null, string fulfillment = null)
		{
			var listOfInputTemplates = new List<InputTemplate>();
			foreach (var temp in publicKeys) {
				listOfInputTemplates.Add(new InputTemplate
				{
					Fulfillment = fulfillment,
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
				Outputs = new List<Output>(),
				Inputs = new List<InputTemplate>(),
				Metadata = null,
				Asset = null,
				Version = "2.0"
			};
		}
	}

	public class TxTemplate {
		[JsonProperty("id")]
		public dynamic Id { get; set; }
		[JsonProperty("operation")]
		public dynamic Operation { get; set; }
		[JsonProperty("outputs")]
		public List<Output> Outputs { get; set; }
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
