using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// A Network is a logical grouping of network devices.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeNetwork.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceNetworks.html
    /// </summary>
    public class Network : Core.TaggableResourceBase<Network>
    {
        #region Properties
        public string name { get; set; }
        public string cidr_block { get; set; }
        public string description { get; set; }
        public string instance_tenancy { get; set; }
        public bool is_default { get; set; }
        #endregion

        #region ctor()
        public Network() : base()
        {

        }
        #endregion

        #region index methods

        /// <summary>
        /// Lists the clouds available to this account
        /// </summary>
        /// <param name="filter">Filter limits results returned</param>
        /// <returns>List of clouds available to this account</returns>
        public static List<Network> index(List<Filter> filter)
        {

            string queryString = string.Empty;

            if (filter != null && filter.Count > 0)
            {
                List<string> validFilters = new List<string>() { "cidr_black", "cloud_href", "name", "resource_uid" };
                Utility.CheckFilterInput("filter", validFilters, filter);

                foreach (var f in filter)
                {
                    queryString += f.ToString() + "&";
                }
            }

            queryString = queryString.TrimEnd('&');

            string jsonString = Core.APIClient.Instance.Get(APIHrefs.Network, queryString);
            return Network.deserializeList(jsonString);
        }

        public static List<Network> index()
        {
            return index(null);
        }
        #endregion
    }
}
