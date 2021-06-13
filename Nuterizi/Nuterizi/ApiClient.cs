using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuterizi
{
    class ApiClient
    {
        private static string appId = "5798d148";
        private static string appKey = "bec15e632bad1752056a2ee7cb9b9ca3";
        public static List<ListMakanan> CariMakanan(string query)
        {
            List<ListMakanan> returnList = new List<ListMakanan>();
            var client = new RestClient("https://trackapi.nutritionix.com/v2/search/instant");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-app-id", appId);
            request.AddHeader("x-app-key", appKey);
            request.AddParameter("branded", false);
            request.AddParameter("query", query);

            IRestResponse response = client.Execute(request);
            JsonObject obj = (JsonObject)SimpleJson.DeserializeObject(response.Content);
            JsonArray listMakanan = obj["common"] as JsonArray;

            foreach (JsonObject makanan in listMakanan)
            {
                ListMakanan ListMakanan = new ListMakanan();
                ListMakanan.nama = (string)makanan["food_name"];
                returnList.Add(ListMakanan);
            }

            return returnList;
        }

        public static Makanan GetDetailMakanan(string query)
        {
            Makanan makanan = new Makanan();
            var client = new RestClient("https://trackapi.nutritionix.com/v2/natural/nutrients");
            var request = new RestRequest(Method.POST);
            request.AddHeader("x-app-id", appId);
            request.AddHeader("x-app-key", appKey);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter(
                "application/x-www-form-urlencoded",
                $"query={query}",
                ParameterType.RequestBody
            );

            IRestResponse response = client.Execute(request);
            JsonObject obj = (JsonObject)SimpleJson.DeserializeObject(response.Content);
            JsonArray listMakanan = obj["foods"] as JsonArray;
            JsonObject detailMakanan = listMakanan[0] as JsonObject;

            makanan.nama = (string)detailMakanan["food_name"];
            makanan.sajian = detailMakanan["serving_weight_grams"].ToString();
            makanan.kalori = detailMakanan["nf_calories"].ToString();
            makanan.karbohidrat = detailMakanan["nf_total_carbohydrate"].ToString();
            makanan.protein = detailMakanan["nf_protein"].ToString();
            makanan.lemak = detailMakanan["nf_total_fat"].ToString();

            return makanan;
        }
    }

    public class ListMakanan
    {
        public string nama { get; set; }
    }

    public class Makanan
    {
        public string nama { get; set; }
        public string sajian { get; set; }
        public string kalori { get; set; }
        public string karbohidrat { get; set; }
        public string protein { get; set; }
        public string lemak { get; set; }
    }
}