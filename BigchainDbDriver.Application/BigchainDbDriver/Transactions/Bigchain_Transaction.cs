﻿using System.Collections.Generic;
using BigchainDbDriver.Assets.Enums;
using BigchainDbDriver.Assets.Models.TransactionModels;
using NBitcoin.DataEncoders;

namespace BigchainDbDriver.Transactions
{
    public class Bigchain_Transaction : IBigchain_TransactionWrite
    {
        private readonly DataEncoder encoder;
        private readonly string Ed25519ConditionType = "ed25519-sha-256";

        public Bigchain_Transaction()
        {
             encoder = Encoders.Base58;
        }

       
		public TxTemplate MakeCreateTransaction(Asset assets,dynamic metadata, List<Output> outputs, List<string> issuers) {
			var assetsDefinition = new AssetDefinition {
				Data = assets.Assets.Data ?? null
			};


            var _inputs = new List<InputTemplate>();
            foreach (var issuer in issuers) {
                _inputs.Add(makeInputTemplate(new List<string> { issuer}));
            }
			//var _inputs = makeInputTemplate( issuers);

			return MakeTrasnsaction(Transaction.CREATE.ToString(), 
                assetsDefinition, 
                metadata, 
                outputs, 
                _inputs);

		}


        public TxTemplate MakeTransferTransaction(List<UnspentOutput> unspentOutputs, List<Output> outputs, dynamic metadata)
        {

            var inputTemplates = new List<InputTemplate>();
            foreach (var uo in unspentOutputs)
            {
                var tx = uo.Tx;
                var fulfilledOutput = tx.Outputs[uo.OutputIndex];
                var transactionLink = new Fulfill
                {
                    OutputIndex = uo.OutputIndex,
                    TransactionId = tx.Id
                };

                inputTemplates.Add(makeInputTemplate(fulfilledOutput.PublicKeys, transactionLink));

            }

            var assetLink = new AssetLink
            {
                Id = unspentOutputs[0].Tx.Operation == Transaction.CREATE.ToString() ? unspentOutputs[0].Tx.Id : unspentOutputs[0].Tx.Asset.Id
            };

            return MakeTrasnsaction(Transaction.TRANSFER.ToString(), assetLink, metadata, outputs, inputTemplates);

        }

        private TxTemplate MakeTrasnsaction(string operation, dynamic assets, dynamic metadata = null, List<Output> outputs = null, List<InputTemplate> inputs = null)
        {
            var tx = MakeTransactionTemplate();
            tx.Operation = operation;
            tx.Asset = assets;
            tx.Metadata = metadata;
            tx.Inputs = inputs;
            tx.Outputs = outputs;
            return tx;
        }

        public List<Output> MakeOutput(Ed25519Condition condition, string amount = "1")
        {
            List<string> pubKeys = new List<string>();

            if (condition.Details.Type == Ed25519ConditionType) {
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

		private InputTemplate makeInputTemplate(IList<string> publicKeys, Fulfill fulfills = null, string fulfillment = null)
		{
            return new InputTemplate {
                Fulfillment = fulfillment,
                Fulfills = fulfills,
                Owners_before = publicKeys
            };
		}

		private TxTemplate MakeTransactionTemplate() {
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



        public Output MakeTransferOutput(Ed25519Condition condition)
        {
            var amount = condition == null ? null : "1";

            if (amount == null)
                return null;

            var pubKey = new List<string>();

            if (condition.Details.Type == Ed25519ConditionType)
            {
                pubKey.Add(condition.Details.PublicKey);
            }


            return new Output
            {
                Amount = amount,
                Condition = condition,
                PublicKeys = pubKey
            };

        }


    }
}
