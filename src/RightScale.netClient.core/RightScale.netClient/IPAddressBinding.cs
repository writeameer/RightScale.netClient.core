﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// An IpAddressBinding represents an abstraction for binding an IpAddress to an instance. The IpAddress is bound immediately for a current instance, or on launch for a next instance. It also allows specifying port forwarding rules for that particular IpAddress and Instance pair.
    /// MediaType reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeIpAddressBinding.html
    /// Resource reference: http://reference.rightscale.com/api1.5/resources/ResourceIpAddressBindings.html
    /// </summary>
    public class IPAddressBinding:Core.RightScaleObjectBase<IPAddressBinding>
    {
        /// <summary>
        /// Private port for NAT rule on this instance of an IP Address Binding
        /// </summary>
        public int private_port { get; set; }

        /// <summary>
        /// Public port for NAT rule on this instance of an IP Address Binding
        /// </summary>
        public int public_port { get; set; }

        /// <summary>
        /// Protocol (TCP or UDP) for this Load Balancing rules
        /// </summary>
        public string protocol { get; set; }

        /// <summary>
        /// Indiciates whether this is a recurring attachment or not
        /// </summary>
        public bool recurring { get; set; }

		#region IPAddressBinding.ctor
		/// <summary>
        /// Default Constructor for IPAddressBinding
        /// </summary>
		public IPAddressBinding():base()
        {
        }
		
		#endregion

        #region IPAddressBinding Relationships

        /// <summary>
        /// Instance for this IPAddressBinding
        /// </summary>
        public Instance instance
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("instance"));
                return Instance.deserialize(jsonString);
            }
        }

        /// <summary>
        /// IPAddress for this IPAddressBinding
        /// </summary>
        public IPAddress ipAddress
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("ip_address"));
                return IPAddress.deserialize(jsonString);
            }
        }

        #endregion

        #region IPAddressBinding.index methods

        /// <summary>
        /// Lists the ip address bindings available to this account
        /// </summary>
        /// <param name="cloudID">ID of the cloud to look for IPAddressBinding objects</param>
        /// <returns>list of IPAddressBinding objects</returns>
        public static List<IPAddressBinding> index(string cloudID)
        {
            return index(cloudID, new List<Filter>());
        }

        /// <summary>
        /// Lists the ip address bindings available to this account
        /// </summary>
        /// <param name="cloudID">ID of the cloud to look for IPAddressBinding objects</param>
        /// <param name="filter">Set of filters for getting specific IPAddressBinding objects</param>
        /// <returns>list of IPAddressBinding objects</returns>
        public static List<IPAddressBinding> index(string cloudID, List<Filter> filter)
        {
            string getHref = string.Format(@"/api/clouds/{0}/ip_address_bindings", cloudID);
            return indexGet(filter, getHref);
        }

        /// <summary>
        /// Lists the ip address bindings available to this account
        /// </summary>
        /// <param name="cloudID">ID of the cloud to look for IPAddressBinding objects</param>
        /// <param name="ipAddressID">ID of the IPAddress to find IPAddressBindings for</param>
        /// <returns>list of IPAddressBinding objects</returns>
        public static List<IPAddressBinding> index(string cloudID, string ipAddressID)
        {
            return index(cloudID, ipAddressID, null);
        }

        /// <summary>
        /// Lists the ip address bindings available to this account
        /// </summary>
        /// <param name="cloudID">ID of the cloud to look for IPAddressBinding objects</param>
        /// <param name="ipAddressID">ID of the IPAddress to find IPAddressBindings for</param>
        /// <param name="filter">Set of filters for getting specific IPAddressBinding objects</param>
        /// <returns>list of IPAddressBinding objects</returns>
        public static List<IPAddressBinding> index(string cloudID, string ipAddressID, List<Filter> filter)
        {
            string getHref = string.Format(@"/api/clouds/{0}/ip_addresses/{1}/ip_address_bindings", cloudID, ipAddressID);
            return indexGet(filter, getHref);
        }

        /// <summary>
        /// internal method to handle GET call to RightScale API for IPAddressBindings
        /// </summary>
        /// <param name="filter">Collection of filters</param>
        /// <param name="getHref">HREF for api GET call</param>
        /// <returns>list of IPAddressBinding objects</returns>
        private static List<IPAddressBinding> indexGet(List<Filter> filter, string getHref)
        {

            string queryString = string.Empty;

            if (filter != null && filter.Count > 0)
            {
                foreach (Filter f in filter)
                {
                    queryString += f.ToString() + "&";
                }
            }

            queryString = queryString.TrimEnd('&');

            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }
        #endregion

        #region IPAddressBinding.show methods

        /// <summary>
        /// Show information about a single IP Address Binding
        /// </summary>
        /// <param name="cloudID">ID of the Cloud where the IPAddressBinding can be found</param>
        /// <param name="ipAddressBindingID">ID of IPAddressBinding to return</param>
        /// <returns>populated instance of IPAddressBinding object</returns>
        public static IPAddressBinding show(string cloudID, string ipAddressBindingID)
        {
            string getHref = string.Format(@"/api/clouds/{0}/ip_addresses_bindings/{1}", cloudID, ipAddressBindingID);
            return showGet(getHref);
        }

        /// <summary>
        /// Show information about a single IP Address Binding
        /// </summary>
        /// <param name="cloudID">ID of the Cloud where the IPAddressBinding can be found</param>
        /// <param name="ipAddressBindingID">ID of IPAddressBinding to return</param>
        /// <param name="ipAddressID">ID of IPAddress belonging to IPAddressBinding</param>
        /// <returns>populated instance of IPAddressBinding object</returns>
        public static IPAddressBinding show(string cloudID, string ipAddressBindingID, string ipAddressID)
        {
            string getHref = string.Format(@"/api/clouds/{0}/ip_addresses/{1}/ip_address_bindings/{2}", cloudID, ipAddressID, ipAddressBindingID);
            return showGet(getHref);
        }

        /// <summary>
        /// Internal method to manage api GET call to retrieve IPAddressBinding 
        /// </summary>
        /// <param name="getHref">HREF for GET call to RS API</param>
        /// <returns>populated instance of IPAddressBinding object</returns>
        private static IPAddressBinding showGet(string getHref)
        {
            string jsonString = Core.APIClient.Instance.Get(getHref);
            return deserialize(jsonString);
        }

        #endregion

        #region IPAddressBinding.create methods

        /// <summary>
        /// Creates an ip address binding which attaches a specified IpAddress resource to a specified instance, and also allows for configuration of port forwarding rules. If the instance specified is a current (running) instance, a one-time IpAddressBinding will be created. If the instance is a next instance, then a recurring IpAddressBinding is created, which will cause the IpAddress to be bound each time the incarnator boots.
        /// </summary>
        /// <param name="cloudID">ID of the Cloud where the IP Address Binding is to be created</param>
        /// <param name="ipAddressID">ID of the IP Address for this IPAddressBinding</param>
        /// <param name="instanceID">ID of the instance to be bound to the specified IP Address</param>
        /// <returns>ID of the newly created IPAddressBinding</returns>
        public static string create(string cloudID, string ipAddressID, string instanceID)
        {
            return create(cloudID, ipAddressID, instanceID);
        }

        /// <summary>
        /// Creates an ip address binding which attaches a specified IpAddress resource to a specified instance, and also allows for configuration of port forwarding rules. If the instance specified is a current (running) instance, a one-time IpAddressBinding will be created. If the instance is a next instance, then a recurring IpAddressBinding is created, which will cause the IpAddress to be bound each time the incarnator boots.
        /// </summary>
        /// <param name="cloudID">ID of the Cloud where the IP Address Binding is to be created</param>
        /// <param name="ipAddressID">ID of the IP Address for this IPAddressBinding</param>
        /// <param name="instanceID">ID of the instance to be bound to the specified IP Address</param>
        /// <param name="publicPort">public port for mapping incoming NAT rules</param>
        /// <param name="privatePort">Port to bind on the private side of the network</param>
        /// <param name="protocol">TCP or UDP protocol specifier</param>
        /// <returns>ID of the newly created IPAddressBinding</returns>
        public static string create(string cloudID, string ipAddressID, string instanceID, string publicPort, string privatePort, string protocol)
        {
            string instanceHref = string.Format(APIHrefs.InstanceByID, cloudID, instanceID);
            string ipAddressHref = string.Format(APIHrefs.IPAddressByID, cloudID, instanceID);

            string postString = string.Format(@"/api/clouds/{0}/ip_addresses/{1}/ip_address_bindings", cloudID, instanceID);
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            List<string> validProtocols = new List<string>() { "UDP", "TCP" };
            Utility.addParameter(instanceHref, "ip_address_binding[instance_href]", postParams);
            Utility.addParameter(privatePort, "ip_address_binding[private_port]", postParams);
            Utility.addParameter(protocol, "ip_address_binding[protocol]", postParams);
            Utility.addParameter(ipAddressHref, "ip_address_binding[public_ip_address_href]", postParams);
            Utility.addParameter(publicPort, "ip_address_binding[public_port]", postParams);
            
            string outString = string.Empty;
            return Core.APIClient.Instance.Post(postString, postParams, "location").Last<string>().Split('/').Last<string>();
        }


        #endregion

        #region IPAddressBinding.destroy methods

        /// <summary>
        /// Deletes a specified IPAddressBinding 
        /// </summary>
        /// <param name="cloudID">ID of cloud where IPAddressBinding is located</param>
        /// <param name="ipAddressBindingID">ID of IPAddressBinding to delete</param>
        /// <param name="ipAddressID">ID of IPAddress associated with the IPAddressBinding to delete</param>
        /// <returns>true if deleted, false if not</returns>
        public static bool destroy(string cloudID, string ipAddressBindingID, string ipAddressID)
        {
            string deleteHref = string.Format(@"/api/clouds/{0}/ip_addresses/{1}/ip_address_bindings/{2}", cloudID, ipAddressID, ipAddressBindingID);
            return Core.APIClient.Instance.Delete(deleteHref);
        }

        /// <summary>
        /// Deletes a specified IPAddressBinding
        /// </summary>
        /// <param name="cloudID">ID of cloud where IPAddressBinding is located</param>
        /// <param name="ipAddressBindingID">ID of IPAddressBinding to delete</param>
        /// <returns>true if deleted, false if not</returns>
        public static bool destroy(string cloudID, string ipAddressBindingID)
        {
            string deleteHref = string.Format(@"/api/clouds/{0}/ip_address_bindings/{1}", cloudID, ipAddressBindingID);
            return Core.APIClient.Instance.Delete(deleteHref);
        }

        #endregion
    }
}
