﻿using BigchainDbDriver.Assets.Models.ResponseModels;
using BigchainDbDriver.Assets.Models.TransactionModels;
using BigchainDbDriver.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace BigchainDbDriver.Client
{
    public class BigchainConnection
    {
        private static HttpClientHandler _httpClientHandler;
        private static HttpClient client;


        private readonly string _path;
        private readonly Dictionary<string, string> _headers;
        private readonly bool _ignoreSslErrors;

        public BigchainConnection(string path, Dictionary<string, string> headers = null, bool ignoreSslErrors = false)
        {
            _httpClientHandler = new HttpClientHandler();
            _path = path;
            _headers = headers;
            _ignoreSslErrors = ignoreSslErrors;
        }

        private string GetApiUrls(string endpoint)
        {
            return _path + BigchainDbUrls.BigchainDbURLs.GetValueOrDefault(endpoint);
        }

        public async Task<(SignedTxResponse, HttpStatusCode)> PostTransactionCommit(SignedTxResponse transaction)
        {
            EnsureClient();
            var txSerialized = JsonConvert.SerializeObject(transaction);
            var canonicalString = JsonUtility.SerializeTransactionIntoCanonicalString(txSerialized);
            var response = await client.PostAsync(GetApiUrls(BigchainDbUrls.Transactions), new StringContent(canonicalString, Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
            {
                return (null, response.StatusCode);
            }
            //TODO: after determining proper type, cast here
            var responseContent = await response.Content.ReadAsAsync<SignedTxResponse>();
            return (responseContent, response.StatusCode);
        }
        public async Task<Block> ListBlocks(string transctionId)
        {
            EnsureClient();
            var response = await client.GetAsync($"{GetApiUrls(BigchainDbUrls.Transactions)}?asset_id={transctionId}");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            var responseContent = await response.Content.ReadAsAsync<List<Block>>();
            return responseContent[responseContent.Count - 1]; //gets latest element
        }


        public async Task<List<T>> GetTransactionDetails<T>(string transactionId)
        {
            try
            {
                EnsureClient();
                var response = await client.GetAsync($"{GetApiUrls(BigchainDbUrls.TransactionsDetail)}?asset_id={transactionId}");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return default;
                }
                var responseContent = await response.Content.ReadAsAsync<List<T>>();
                return responseContent;
            }
            catch 
            {
                throw;
            }
        }


        private void EnsureClient()
        {
            if (client != null) return;

            SetupCertCheckIgnoreForDebug();

            client = new HttpClient(_httpClientHandler);
            var baseAddress = this.GetServerUri();
            client.BaseAddress = baseAddress;
        }

        private void SetupCertCheckIgnoreForDebug()
        {
            if (!_ignoreSslErrors) return;

            _httpClientHandler.ServerCertificateCustomValidationCallback = (message,
                cert, chain, errors) =>
            { return true; };
        }

        private Uri GetServerUri()
        {
            var parsedUrl = new Uri(_path);
            return parsedUrl;
        }

    }
}
