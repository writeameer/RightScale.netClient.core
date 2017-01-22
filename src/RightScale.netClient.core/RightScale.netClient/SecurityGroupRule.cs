﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Security Group Rules represent the ingress/egress rules that define a security group.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeSecurityGroupRule.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceSecurityGroupRules.html
    /// </summary>
    public class SecurityGroupRule : Core.RightScaleObjectBase<SecurityGroupRule>
    {
        #region SecurityGroupRule Properties

        /// <summary>
        /// CIDR IPs for this SecurityGroupRule
        /// </summary>
        public string cidr_ips { get; set; }

        /// <summary>
        /// Protocol for this SecurityGroupRule
        /// </summary>
        public string protocol { get; set; }

        /// <summary>
        /// End Port for this SecurityGroupRule
        /// </summary>
        public string end_port { get; set; }

        /// <summary>
        /// Start Port for this SecurityGroupRule
        /// </summary>
        public string start_port { get; set; }

        #endregion

        #region SecurityGroupRule Relationships

        /// <summary>
        /// SecurityGroup associated with this SecurityGroupRule
        /// </summary>
        public SecurityGroup securityGroup
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("security_group"));
                return SecurityGroup.deserialize(jsonString);
            }
        }


        #endregion

        #region SecurityGroupRule.ctor
        /// <summary>
        /// Default Constructor for SecurityGroupRule
        /// </summary>
        public SecurityGroupRule()
            : base()
        {
        }

        #endregion
		        
        #region SecurityGroupRule.index methods

        /// <summary>
        /// Lists SecurityGroupRules
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the SecurityGroup belongs</param>
        /// <param name="securityGroupID">ID of the SecurityGroup where the rules belong</param>
        /// <returns>List of SecurityGroupRule objects</returns>
        public static List<SecurityGroupRule> index(string cloudID, string securityGroupID)
        {
            return index(cloudID, securityGroupID, null);
        }

        /// <summary>
        /// Lists SecurityGroupRules
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the SecurityGroup belongs</param>
        /// <param name="securityGroupID">ID of the SecurityGroup where the rules belong</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>List of SecurityGroupRule objects</returns>
        public static List<SecurityGroupRule> index(string cloudID, string securityGroupID, string view)
        {
            string getHref = string.Format(APIHrefs.SecurityGroupRule, cloudID, securityGroupID);
            return indexGet(ref view, getHref);
        }

        /// <summary>
        /// Internal get process for indexing SecurityGroupRules
        /// </summary>
        /// <param name="view"></param>
        /// <param name="getHref"></param>
        /// <returns></returns>
        private static List<SecurityGroupRule> indexGet(ref string view, string getHref)
        {
            view = getValidView(view);

            string queryString = string.Format("view={0}", view);
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }

        #endregion

        #region SecurityGroupRule.show methods

        /// <summary>
        /// Displays information about a single SecurityGroupRule
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the SecurityGroup belongs</param>
        /// <param name="securityGroupID">ID of the SecurityGroup where the SecurityGroupRule belongs</param>
        /// <param name="securityGroupRuleID">ID of the SecurityGroupRule being returned</param>
        /// <returns>Populated SecurityGroupRule object</returns>
        public static SecurityGroupRule show(string cloudID, string securityGroupID, string securityGroupRuleID)
        {
            return show(cloudID, securityGroupID, securityGroupRuleID, null);
        }

        /// <summary>
        /// Displays information about a single SecurityGroupRule
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the SecurityGroup belongs</param>
        /// <param name="securityGroupID">ID of the SecurityGroup where the SecurityGroupRule belongs</param>
        /// <param name="securityGroupRuleID">ID of the SecurityGroupRule being returned</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Populated SecurityGroupRule object</returns>
        public static SecurityGroupRule show(string cloudID, string securityGroupID, string securityGroupRuleID, string view)
        {
            string getHref = string.Format(APIHrefs.SecurityGroupRuleByID, cloudID, securityGroupID, securityGroupRuleID);
            return showGet(getHref, view);
        }

        /// <summary>
        /// Displays information about a single SecurityGroupRule
        /// </summary>
        /// <param name="securityGroupRuleHref">Fuly formed href for SecurityGroupRule</param>
        /// <returns>Populated SecurityGroupRule object</returns>
        public static SecurityGroupRule show(string securityGroupRuleHref)
        {
            return show(securityGroupRuleHref, null);
        }

        /// <summary>
        /// Displays information about a single SecurityGroupRule
        /// </summary>
        /// <param name="securityGroupRuleHref">Fuly formed href for SecurityGroupRule</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Populated SecurityGroupRule object</returns>
        public static SecurityGroupRule show(string securityGroupRuleHref, string view)
        {
            return showGet(securityGroupRuleHref, view);
        }

        /// <summary>
        /// Internal method handles the API call for GET calls to RS API for retrieving a SecurityGroupRule
        /// </summary>
        /// <param name="getHref">Href to use to retrieve the object</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Populated SecurityGroupRule object</returns>
        private static SecurityGroupRule showGet(string getHref, string view)
        {
            view = getValidView(view);

            string queryString = string.Format("view={0}", view);
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserialize(jsonString);
        }
        #endregion

        #region SecurityGroupRule.create methods

        /// <summary>
        /// Create a security group rule for a security group
        /// </summary>
        /// <param name="cloudID">ID of cloud where Security Group is located</param>
        /// <param name="securityGroupID">ID of Security Group to create the Security Group Rule in</param>
        /// <param name="protocol">Security Group Rule protocol</param>
        /// <param name="sourceType">Security Group Rule source type</param>
        /// <returns>ID of newly created security group rule</returns>
        public static string create(string cloudID, string securityGroupID, string protocol, string sourceType)
        {
            return create(cloudID, securityGroupID, protocol, sourceType, null, null, null, null, null, null, null);
        }

        /// <summary>
        /// Create a security group rule for a security group
        /// </summary>
        /// <param name="cloudID">ID of cloud where Security Group is located</param>
        /// <param name="securityGroupID">ID of Security Group to create the Security Group Rule in</param>
        /// <param name="protocol">Security Group Rule protocol</param>
        /// <param name="sourceType">Security Group Rule source type</param>
        /// <param name="cidrIPs">Security Group Rule CIDR ips</param>
        /// <param name="groupName">Security Group Rule group name</param>
        /// <param name="groupOwner">Security Group Rule group owner</param>
        /// <param name="startPort">Security Group Rule start port</param>
        /// <param name="endPort">Security Group Rule end port</param>
        /// <param name="icmpCode">Security Group Rule icmp code</param>
        /// <param name="icmpType">Security Group Rule icmp type</param>
        /// <returns>ID of newly created security group rule</returns>
        public static string create(string cloudID, string securityGroupID, string protocol, string sourceType, string cidrIPs, string groupName, string groupOwner, string startPort, string endPort, string icmpCode, string icmpType)
        {
            string postHref = string.Format(APIHrefs.SecurityGroupRule, cloudID, securityGroupID);
            string securityGroupHref = string.Format(APIHrefs.SecurityGroupByID, cloudID, securityGroupID);
            return createPost(protocol, sourceType, cidrIPs, groupName, groupOwner, startPort, endPort, icmpCode, icmpType, postHref, securityGroupHref);
        }

        /// <summary>
        /// Create a security group rule for a security group
        /// </summary>
        /// <param name="securityGroupHref">Security group HREF</param>
        /// <param name="protocol">Security Group Rule protocol</param>
        /// <param name="sourceType">Security Group Rule source type</param>
        /// <returns>ID of newly created security group rule</returns>
        public static string create(string securityGroupHref, string protocol, string sourceType)
        {
            return create(securityGroupHref, protocol, sourceType, null, null, null, null, null, null, null,null);
        }

        /// <summary>
        /// Create a security group rule for a security group
        /// </summary>
        /// <param name="securityGroupHref">Security group HREF</param>
        /// <param name="protocol">Security Group Rule protocol</param>
        /// <param name="sourceType">Security Group Rule source type</param>
        /// <param name="cidrIPs">Security Group Rule CIDR ips</param>
        /// <param name="groupName">Security Group Rule group name</param>
        /// <param name="groupOwner">Security Group Rule group owner</param>
        /// <param name="startPort">Security Group Rule start port</param>
        /// <param name="endPort">Security Group Rule end port</param>
        /// <param name="icmpCode">Security Group Rule icmp code</param>
        /// <param name="icmpType">Security Group Rule icmp type</param>
        /// <returns>ID of newly created security group rule</returns>
        public static string create(string securityGroupHref, string protocol, string sourceType, string cidrIPs, string groupName, string groupOwner, string startPort, string endPort, string icmpCode, string icmpType)
        {
            string postHref = "/api/security_group_rules";
            return createPost(protocol, sourceType, cidrIPs, groupName, groupOwner, startPort, endPort, icmpCode, icmpType, postHref, securityGroupHref);
        }

        /// <summary>
        /// Internal process to manage Post for creating a SecurityGroupRule
        /// </summary>
        /// <param name="protocol">Security Group Rule protocol</param>
        /// <param name="sourceType">Security Group Rule source type</param>
        /// <param name="cidrIPs">Security Group Rule CIDR ips</param>
        /// <param name="groupName">Security Group Rule group name</param>
        /// <param name="groupOwner">Security Group Rule group owner</param>
        /// <param name="startPort">Security Group Rule start port</param>
        /// <param name="endPort">Security Group Rule end port</param>
        /// <param name="icmpCode">Security Group Rule icmp code</param>
        /// <param name="icmpType">Security Group Rule icmp type</param>
        /// <param name="postHref">Post HREF for api call</param>
        /// <param name="securityGroupHref">Security group HREF</param>
        /// <returns>ID of newly created security group rule</returns>
        private static string createPost(string protocol, string sourceType, string cidrIPs, string groupName, string groupOwner, string startPort, string endPort, string icmpCode, string icmpType, string postHref, string securityGroupHref)
        {
            List<string> validSourceTypes = new List<string>() { "cidr_ips", "group" };
            List<String> validProtocols = new List<string>() { "tcp", "udp", "icmp" };
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            Utility.CheckStringHasValue(protocol);
            Utility.CheckStringHasValue(sourceType);
            Utility.addParameter(cidrIPs, "security_group_rule[cidr_ips]", postParams);
            Utility.addParameter(groupName, "security_group_rule[group_name]", postParams);
            Utility.addParameter(groupOwner, "security_group_rule[group_owner]", postParams);
            Utility.addParameter(protocol, "security_group_rule[protocol]", postParams);
            Utility.addParameter(endPort, "security_group_rule[protocol_details][end_port]", postParams);
            Utility.addParameter(icmpCode, "security_group_rule[protocol_details][icmp_code]", postParams);
            Utility.addParameter(icmpType, "security_group_rule[protocol_details][icmp_type]", postParams);
            Utility.addParameter(startPort, "security_group_rule[protocol_details][start_port]", postParams);
            Utility.addParameter(securityGroupHref, "security_group_rule[security_group_href]", postParams);
            Utility.addParameter(sourceType, "security_group_rule[source_type]", postParams);

            return Core.APIClient.Instance.Post(postHref, postParams, "location").Last<string>().Split('/').Last<string>();
        }

        #endregion

        #region SecurityGroupRule.destroy methods

        /// <summary>
        /// Delete a security group rule
        /// </summary>
        /// <param name="securityGroupRuleID">ID of SecurityGroupRule to delete</param>
        /// <returns>true if deleted, false if not</returns>
        public static bool destroy(string securityGroupRuleID)
        {
            string deleteHref = string.Format("/api/security_group_rules/{0}", securityGroupRuleID);
            return destroyDelete(deleteHref);
        }

        /// <summary>
        /// Delete a secuirty group rule
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the security group exists</param>
        /// <param name="securityGroupID">ID of the security group that the security group rule belongs to</param>
        /// <param name="securityGroupRuleID">ID of the security group rule to delete</param>
        /// <returns>true if deleted, false if not</returns>
        public static bool destroy(string cloudID, string securityGroupID, string securityGroupRuleID)
        {
            string deleteHref = string.Format(APIHrefs.SecurityGroupRuleByID, cloudID, securityGroupID, securityGroupRuleID);
            return destroyDelete(deleteHref);
        }

        /// <summary>
        /// Internal call to RightScale API to delete SecurityGroupRules
        /// </summary>
        /// <param name="securityGroupRuleHref">SecurityGroupRule delete href</param>
        /// <returns>true if deleted, false if not</returns>
        private static bool destroyDelete(string securityGroupRuleHref)
        {
            return Core.APIClient.Instance.Delete(securityGroupRuleHref);
        }

        #endregion

        /// <summary>
        /// Helper method to return a valid view value for SecurityGroupRule api calls
        /// </summary>
        /// <param name="view">view value to be tested</param>
        /// <returns>value of input view or default view if input is null or empty</returns>
        private static string getValidView(string view)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default" };
                Utility.CheckStringInput("view", validViews, view);
            }
            return view;
        }
    }
}
