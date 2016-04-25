using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ImageGallery.Common
{
    public class TestGeo
    {
        private static string baseUri =
            "http://dev.virtualearth.net/REST/v1/Locations/{0},{1}?o=xml&key=AmAbR13CxXmAFIH2Fr3BiJ5FjWpYgg3BfTrm9xWJalzC4zDHoiOH0sxbUL8BCdU3";
          //"http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";
        string location = string.Empty;

        public static string RetrieveFormatedAddress(string lat, string lng)
        {
            string requestUri = string.Format(baseUri, lat, lng);

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
