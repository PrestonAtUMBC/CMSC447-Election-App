﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ElectionAppLibrary.Models;

namespace ElectionAppLibrary.DataAccess
{
    public interface IRepAPIRequests
    {
        Task<RepresentativeModel?> RepOnGet(string ocdId);
    }

    public class RepAPIRequests : IRepAPIRequests
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RepAPIRequests(IHttpClientFactory httpClientFactory) =>
        _httpClientFactory = httpClientFactory;
        //ocdId in format: "ocd-division/country:country/state:state/cd:#
        public async Task<RepresentativeModel?> RepOnGet(string ocdId)
        {
            ocdId = ocdId.Replace("/", "%2F");
            ocdId = ocdId.Replace(":", "%3A");
            var httpClient = _httpClientFactory.CreateClient("RepByDiv");
            try
            {
                var representative = await httpClient.GetFromJsonAsync<RepresentativeModel>($"{ocdId}?key=AIzaSyDRPOb2Wy4TIGZ2HcSuXLxxuIoNytPGIzE");
                return representative;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}
