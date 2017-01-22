﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// An InstanceType represents a basic hardware configuration for an Instance.
    /// Combining all possible configurations of hardware into a smaller, well-known set of options makes instances easier to manage, and allows better allocation efficiency into physical hosts
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeInstanceType.html
    /// Resource Reference: http://reference.rightscale.com/api1.5/resources/ResourceInstanceTypes.html
    /// </summary>
    public class InstanceType:Core.RightScaleObjectBase<InstanceType>
    {
        #region InstanceType Properties

        /// <summary>
        /// Friendly name of this instance type
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// RightScale UID for this InstanceType
        /// </summary>
        public string resource_uid { get; set; }

        /// <summary>
        /// CPU Architecture associated with this InstanceType
        /// </summary>
        public string cpu_architecture { get; set; }

        /// <summary>
        /// Data on local disks for this InstanceType
        /// </summary>
        public string local_disks { get; set; }

        /// <summary>
        /// Amount of memory associated with this InstanceType
        /// </summary>
        public string memory { get; set; }

        /// <summary>
        /// Size of the local disk for this InstanceType
        /// </summary>
        public string local_disk_size { get; set; }

        /// <summary>
        /// Count of CPUs for this InstanceType
        /// </summary>
        public string cpu_count { get; set; }

        /// <summary>
        /// Speed of CPUs for this InstanceType
        /// </summary>
        public string cpu_speed { get; set; }

        /// <summary>
        /// Description for this instanceType
        /// </summary>
        public string description { get; set; }
        
        #endregion

        #region InstanceType Relationships

        /// <summary>
        /// Cloud associated with this InstanceType
        /// </summary>
        public Cloud cloud
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("cloud"));
                return Cloud.deserialize(jsonString);
            }
        }

        #endregion

        #region InstanceType.ctor
        /// <summary>
        /// Default Constructor for InstanceType
        /// </summary>
        public InstanceType()
            : base()
        {
        }

        #endregion
		
        #region InstanceType.index methods

        /// <summary>
        /// Lists instance types.
        /// </summary>
        /// <param name="cloudID">ID of the cloud to enumerate instance types for</param>
        /// <returns>Collection of InstanceTypes</returns>
        public static List<InstanceType> index(string cloudID)
        {
            return index(cloudID, null, null);
        }

        /// <summary>
        /// Lists instance types.
        /// </summary>
        /// <param name="cloudID">ID of the cloud to enumerate instance types for</param>
        /// <param name="filter">Collection of filters for limiting the return set</param>
        /// <returns>Collection of InstanceTypes</returns>
        public static List<InstanceType> index(string cloudID, List<Filter> filter)
        {
            return index(cloudID, filter, null);
        }

        /// <summary>
        /// Lists instance types.
        /// </summary>
        /// <param name="cloudID">ID of the cloud to enumerate instance types for</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Collection of InstanceTypes</returns>
        public static List<InstanceType> index(string cloudID, string view)
        {
            return index(cloudID, null, view);
        }

        /// <summary>
        /// Lists instance types.
        /// </summary>
        /// <param name="cloudID">ID of the cloud to enumerate instance types for</param>
        /// <param name="filter">Collection of filters for limiting the return set</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Collection of InstanceTypes</returns>
        public static List<InstanceType> index(string cloudID, List<Filter> filter, string view)
        {
            string getHref = string.Format(APIHrefs.InstanceType, cloudID);

            if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "cpu_architecture", "description", "name", "resource_uid" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            string queryString = string.Empty;
            if (filter != null && filter.Count > 0)
            {
                queryString += Utility.BuildFilterString(filter);
            }
            queryString += string.Format("view={0}", view);
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }
        #endregion

        #region InstanceType.show methods

        /// <summary>
        /// Displays information about a single Instance type.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the InstanceType can be found</param>
        /// <param name="instanceTypeID">ID of the specific InstanceType to be returned</param>
        /// <returns>Specific instance of InstanceType</returns>
        public static InstanceType show(string cloudID, string instanceTypeID)
        {
            return show(cloudID, instanceTypeID, null);
        }

        /// <summary>
        /// Displays information about a single Instance type.
        /// </summary>
        /// <param name="cloudID">ID of the cloud where the InstanceType can be found</param>
        /// <param name="instanceTypeID">ID of the specific InstanceType to be returned</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>Specific instance of InstanceType</returns>
        public static InstanceType show(string cloudID, string instanceTypeID, string view)
        {
            string getHref = string.Format(APIHrefs.InstanceTypeByID, cloudID, instanceTypeID);
            string queryString = string.Empty;

            if (!string.IsNullOrWhiteSpace(view))
            {
                List<string> validViews = new List<string>() { "default" };
                Utility.CheckStringInput("view", validViews, view);
                queryString += string.Format("view={0}", view);
            }   
            
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserialize(jsonString);
        }

        #endregion

    }
}
