﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace RightScale.netClient
{
    /// <summary>
    /// Images represent base VM image existing in a cloud. An image will define the initial Operating System and root disk contents for a new Instance to have, and therefore it represents the basic starting point for creating a new one.
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeImage.html
    /// Resources Reference: http://reference.rightscale.com/api1.5/resources/ResourceImages.html
    /// </summary>
    public class Image : Core.TaggableResourceBase<Image>
    {
        #region Image Properties

        /// <summary>
        /// Name of this Image
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// RightScale Resource Unique Identifier for this Image
        /// </summary>
        public string resource_uid { get; set; }
        
        /// <summary>
        /// CPU Architecture for this Image
        /// </summary>
        public string cpu_architecture { get; set; }

        /// <summary>
        /// Image Type for this image
        /// </summary>
        public string image_type { get; set; }

        /// <summary>
        /// Virtualization Type for this Image
        /// </summary>
        public string virtualization_type { get; set; }

        /// <summary>
        /// OS Platform for this Image
        /// </summary>
        public string os_platform { get; set; }

        /// <summary>
        /// Description for this Image
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Visibility for this Image
        /// </summary>
        public string visibility { get; set; }

        #endregion

        #region Image Relationships

        /// <summary>
        /// Cloud associated with this Image
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

        #region Image.ctor
        /// <summary>
        /// Default Constructor for Image
        /// </summary>
        public Image()
            : base()
        {
        }

        #endregion

        #region Image.index methods

        /// <summary>
        /// Lists the Images owned by this Account.
        /// </summary>
        /// <returns>List of Images</returns>
        public static List<Image> index(string cloudID)
        {

            return index(cloudID, new List<Filter>(), null);

        }

        /// <summary>
        /// Lists the Images owned by this Account.
        /// </summary>
        /// <param name="filterList">Set of filters to modify query to return Images from RightScale API</param>
        /// <returns>Filtered list of Images</returns>
        public static List<Image> index(string cloudID, List<Filter> filterList)
        {
            return index(cloudID, filterList, string.Empty);
        }

        /// <summary>
        /// Lists the Images owned by this Account.
        /// </summary>
        /// <param name="view">Defines specific view to limit the Images returned from RightScale API</param>
        /// <returns>Filtered list of Images based on view input</returns>
        public static List<Image> index(string cloudID, string view)
        {
            return index(cloudID, new List<Filter>(), view);

        }

        /// <summary>
        /// This method is intended for use within PowerShell.
        /// Lists the Images owned by this Account.
        /// </summary>
        /// <param name="filterList">Set of filters to modify query to return Images from RightScale API</param>
        /// <param name="view">Defines specific view to limit the Images returned from RightScale API</param>
        /// <returns>Filtered list of Images based on filter and view input</returns>
        public static List<Image> index(string cloudID, string filterList, string view)
        {
           List<Filter> filter = Filter.parseFilterList(filterList);

           return index(cloudID, filter, view);
        }

        /// <summary>
        /// Lists the Images owned by this Account.
        /// </summary>
        /// <param name="filterList">Set of filters to modify query to return Images from RightScale API</param>
        /// <param name="view">Defines specific view to limit the Images returned from RightScale API</param>
        /// <returns>Filtered list of Images based on filter and view input</returns>
        public static List<Image> index(string cloudID, List<Filter> filterList, string view)
        {

            string getUrl = string.Format(APIHrefs.Image, cloudID);
            string queryString = string.Empty;
            
           if (string.IsNullOrWhiteSpace(view))
            {
                view = "default";
            }
            else
            {
                List<string> validViews = new List<string>() { "default" };
                Utility.CheckStringInput("view", validViews, view);
            }

            List<string> validFilters = new List<string>() { "cpu_architecture", "description", "image_type", "name", "os_platform", "resource_uid", "visibility" };
            Utility.CheckFilterInput("filter", validFilters, filterList);


            if (filterList != null && filterList.Count > 0)
            {
                queryString += Utility.BuildFilterString(filterList) + "&";
            }
            if (!string.IsNullOrWhiteSpace(view))
            {
                queryString += string.Format("view={0}", view);
            }

            string jsonString = Core.APIClient.Instance.Get(getUrl, queryString);

            return deserializeList(jsonString);
        }
        #endregion

        #region Image.show methods

        /// <summary>
        /// Shows the information of a single image.
        /// </summary>
        /// <param name="serverid">ID of the image to be retrieved</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include.</param>
        /// <returns>Populated Image object</returns>
        public static Image show(string cloudID, string imageID, string view)
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

            string getHref = string.Format(APIHrefs.ImageByID, cloudID, imageID);
            return showGet(getHref, view);
        }

        /// <summary>
        /// Internal implementation of show for both deployment and non-deployment calls.  
        /// </summary>
        /// <param name="getHref"></param>
        /// <param name="view"></param>
        /// <returns>Image object with data</returns>
        private static Image showGet(string getHref, string view)
        {
            List<string> validViews = new List<string>() { "default" };
            Utility.CheckStringInput("view", validViews, view);

            string queryString = string.Empty;

            if (!string.IsNullOrWhiteSpace(view))
            {
                queryString += string.Format("view={0}", view);
            }

            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserialize(jsonString);
        }

        
        #endregion
    }
}
