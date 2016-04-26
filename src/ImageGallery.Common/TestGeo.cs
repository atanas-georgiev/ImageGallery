using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.String;

namespace ImageGallery.Common
{
    public class TestGeo
    {
        private static string key = "AIzaSyAJOGz_xyAi_2CdRPW4HX-g5E1WcTwQMSY";
        private static string baseUri = "https://maps.googleapis.com/maps/api/geocode/xml?address={0}+CA&key={1}";

    public static Tuple<double, double> RetrieveCoordinates(string location)
    {
        string requestUri = Format(baseUri, location, key);

        using (WebClient wc = new WebClient())
        {
            string result = wc.DownloadString(requestUri);
            var xmlElm = XElement.Parse(result);
            var status = (from elm in xmlElm.Descendants()
                            where
                            elm.Name == "status"
                            select elm).FirstOrDefault();
            if (status.Value.ToLower() == "ok")
            {
                var res = (from elm in xmlElm.Descendants()
                            where
                            elm.Name == "formatted_address"
                            select elm).FirstOrDefault();
                requestUri = res.Value;
                return new Tuple<double, double>(1,1);
            }
            return new Tuple<double, double>(1, 1); ;
        }
    }

    public static string RetrieveFormatedAddress(string lat, string lng)
    {
        string requestUri = Format(baseUri, lat, lng);

        using (WebClient wc = new WebClient())
        {
            string result = wc.DownloadString(requestUri);
            var xmlElm = XElement.Parse(result);
            var status = (from elm in xmlElm.Descendants()
                            where
                            elm.Name == "status"
                            select elm).FirstOrDefault();
            if (status.Value.ToLower() == "ok")
            {
                var res = (from elm in xmlElm.Descendants()
                            where
                            elm.Name == "formatted_address"
                            select elm).FirstOrDefault();
                requestUri = res.Value;
                return requestUri;
            }
            return "";
        }
    }
    }
}
