﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace BrightLocal
{
    public class LocationService: BrightLocalService
    {
        public LocationService(string apiKey = null, string apiSecret = null) : base(apiKey, apiSecret) { }

        BrightLocalRequestor request = new BrightLocalRequestor();

        public virtual BrightLocalSuccess Create(LocationOptions createOptions)
        {
            var parameters = Parameters.convertListToParameters(createOptions);
            var success = request.Post(Urls.Locations, parameters, this.api_key, this.api_secret);
            return JsonConvert.DeserializeObject<BrightLocalSuccess>(success.Content);
            
        }

        public virtual BrightLocalSuccess Update(UpdateLocationOptions updateOptions)
        {
            var url = string.Format(Urls.Locations + "{0}", updateOptions.locationId);
            var parameters = Parameters.convertListToParameters(updateOptions);
            var success = request.Put(url, parameters, this.api_key, this.api_secret);
            return JsonConvert.DeserializeObject<BrightLocalSuccess>(success.Content);
        }

        public virtual BrightLocalSuccess Delete(int locationId)
        {
            var url = string.Format(Urls.Locations + "{0}", locationId);
            var parameters = new Parameters.requestParameters();            
            var success = request.Delete(url, parameters, this.api_key, this.api_secret);
            return JsonConvert.DeserializeObject<BrightLocalSuccess>(success.Content);
        }

        public virtual BrightLocalLocation Get(int locationId)
        {
            var url = string.Format(Urls.Locations + "{0}", locationId);
            var parameters = new Parameters.requestParameters();
            var success = request.Get(url, parameters, this.api_key, this.api_secret);
            JObject o = JObject.Parse(success.Content);
            return JsonConvert.DeserializeObject<BrightLocalLocation>(o.SelectToken("location").ToString());            
        }

        public virtual BrightLocalLocationSearch Search(string query)
        {
            var url = string.Format(Urls.Locations + "search");
            var parameters = new Parameters.requestParameters();
            parameters.Add("q", query);
            var results = request.Get(url, parameters, this.api_key, this.api_secret);
            return JsonConvert.DeserializeObject<BrightLocalLocationSearch>(results.Content);
        }
    }
}
