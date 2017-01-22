﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Cloud : Core.RightScaleObjectBase<Cloud>
    {
        public string name { get; set; }
        public string cloud_type { get; set; }
        public string description { get; set; }


        #region Cloud.ctor
        /// <summary>
        /// Default Constructor for Cloud
        /// </summary>
        public Cloud()
            : base()
        {
        }

        #endregion
		
        #region Cloud Relationships

        /// <summary>
        /// list of datacenters associated with this cloud
        /// </summary>
        public List<DataCenter> datacenters
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("datacenters"));
                return DataCenter.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// list of volume snapshots associated with this cloud
        /// </summary>
        public List<VolumeSnapshot> volumeSnapshots
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("volume_snapshots"));
                return VolumeSnapshot.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// List of instances associated with this cloud
        /// </summary>
        public List<Instance> instances
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("instances"));
                return Instance.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// List of Voulume Types associated with this cloud
        /// </summary>
        public List<VolumeType> volumeTypes
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("volume_types"));
                return VolumeType.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// List of SSH keys associated with this cloud
        /// </summary>
        public List<SshKey> sshKeys
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("ssh_keys"));
                return SshKey.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// List of recurring volume attachments associated with this cloud
        /// </summary>
        public List<RecurringVolumeAttachment> recurringVolumeAttachments
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("recurring_volume_attachments"));
                return RecurringVolumeAttachment.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// List of Volume Attachments associated with this cloud
        /// </summary>
        public List<VolumeAttachment> volumeAttachments
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("volume_attachments"));
                return VolumeAttachment.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// list of Volumes associated with this cloud
        /// </summary>
        public List<Volume> volumes
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("volumes"));
                return Volume.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// List of IP Address Bindings associated with this cloud
        /// </summary>
        public List<IPAddressBinding> ipAddressBindings
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("ip_address_bindings"));
                return IPAddressBinding.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// List of images associated with this cloud
        /// </summary>
        public List<Image> images
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("images"));
                return Image.deserializeList(jsonString);
            }
        }
        
        /// <summary>
        /// List of instance types associated with this cloud
        /// </summary>
        public List<InstanceType> instanceTypes
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("instance_types"));
                return InstanceType.deserializeList(jsonString);
            }
        }

        /// <summary>
        /// List of IP Addressess associated with this cloud
        /// </summary>
        public List<IPAddress> ipAddresses
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("ip_addresses"));
                return IPAddress.deserializeList(jsonString);
            }
        }

        #endregion

        #region Cloud.index methods

        /// <summary>
        /// Lists the clouds available to this account
        /// </summary>
        /// <returns>List of clouds available to this account</returns>
        public static List<Cloud> index()
        {
            return index(null);
        }

        /// <summary>
        /// Lists the clouds available to this account
        /// </summary>
        /// <param name="filter">Filter limits results returned</param>
        /// <returns>List of clouds available to this account</returns>
        public static List<Cloud> index(List<Filter> filter)
        {    

            string queryString = string.Empty;

            if (filter != null && filter.Count > 0)
            {
                List<string> validFilters = new List<string>() { "cloud_type", "description", "name" };
                Utility.CheckFilterInput("filter", validFilters, filter);

                foreach (var f in filter)
                {
                    queryString += f.ToString() + "&";
                }
            }

            queryString = queryString.TrimEnd('&');

            string jsonString = Core.APIClient.Instance.Get(APIHrefs.Cloud, queryString);
            return Cloud.deserializeList(jsonString);
        }
        #endregion

        #region Cloud.show methods

        /// <summary>
        /// Show information about a single cloud
        /// </summary>
        /// <param name="cloudID">ID of the cloud to get details on</param>
        /// <returns>Populated instance of a Cloud object</returns>
        public static Cloud show(string cloudID)
        {
            string getHref = string.Format(APIHrefs.CloudByID, cloudID);
            string jsonString = Core.APIClient.Instance.Get(getHref);
            return Cloud.deserialize(jsonString);
        }

        #endregion

    }
}
