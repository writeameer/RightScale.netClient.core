﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// This resource represents links between ServerTemplates and MultiCloud Images, it enables you to effectively add MultiCloud Images to ServerTemplates and make them the default one
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeServerTemplateMultiCloudImage.html
    /// Resources Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeServerTemplateMultiCloudImage.html
    /// </summary>
    public class ServerTemplateMultiCloudImage:Core.RightScaleObjectBase<ServerTemplateMultiCloudImage>
    {
        #region ServerTemplateMultiCloudImage Properties

        /// <summary>
        /// Datetime when this ServerTemplateMultiCloudImage was created
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// Boolean indicating that this image is the default ServerTemplateMultiCloudImage
        /// </summary>
        public bool is_default { get; set; }

        /// <summary>
        /// Datetime when this ServerTemplateMultiCloudImage was last updated
        /// </summary>
        public string updated_at { get; set; }

        #endregion

        #region ServerTemplateMultiCloudImage Relationships

        /// <summary>
        /// MultiCloudImage associated with this ServerTemplateMultiCloudImage
        /// </summary>
        public MultiCloudImage multiCloudImage
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("multi_cloud_image"));
                return MultiCloudImage.deserialize(jsonString);
            }
        }

        /// <summary>
        /// ServerTemplate associated with this ServerTemplateMultiCloudImage
        /// </summary>
        public ServerTemplate serverTemplate
        {
            get
            {
                string jsonString = Core.APIClient.Instance.Get(getLinkValue("server_template"));
                return ServerTemplate.deserialize(jsonString);
            }
        }

        #endregion

        #region ServerTemplateMultiCloudImage.ctor
        /// <summary>
        /// Default Constructor for ServerTemplateMultiCloudImage
        /// </summary>
        public ServerTemplateMultiCloudImage()
            : base()
        {
        }
        
        #endregion

        #region ServerTemplateMultiCloudImage.index methods

        /// <summary>
        /// Lists the ServerTemplateMultiCloudImages that are available to this account
        /// </summary>
        /// <returns>List of ServerTemplateMultiCloudImage objects</returns>
        public static List<ServerTemplateMultiCloudImage> index()
        {
            return index(null, null);
        }

        /// <summary>
        /// Lists the ServerTemplateMultiCloudImages that are available to this account
        /// </summary>
        /// <param name="filter">Set of filters to limit return of query</param>
        /// <returns>List of ServerTemplateMultiCloudImage objects</returns>
        public static List<ServerTemplateMultiCloudImage> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        /// <summary>
        /// Lists the ServerTemplateMultiCloudImages that are available to this account
        /// </summary>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>List of ServerTemplateMultiCloudImage objects</returns>
        public static List<ServerTemplateMultiCloudImage> index(string view)
        {
            return index(null, view);
        }

        /// <summary>
        /// Lists the ServerTemplateMultiCloudImages that are available to this account
        /// </summary>
        /// <param name="filter">Set of filters to limit return of query</param>
        /// <param name="view">Specifies how many attributes and/or expanded nested relationships to include</param>
        /// <returns>List of ServerTemplateMultiCloudImage objects</returns>
        public static List<ServerTemplateMultiCloudImage> index(List<Filter> filter, string view)
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

            List<string> validFilters = new List<string>() { "is_default", "multi_cloud_image_href", "server_template_href" };
            Utility.CheckFilterInput("filter", validFilters, filter);

            string queryString = string.Empty;

            if (filter != null && filter.Count > 0)
            {
                queryString += Utility.BuildFilterString(filter) + "&";
            }

            queryString += string.Format("view={0}", view);

            string getHref = APIHrefs.ServerTemplateMultiCloudImages;
            string jsonString = Core.APIClient.Instance.Get(getHref, queryString);
            return deserializeList(jsonString);
        }
        #endregion

        #region ServerTemplateMultiCloudImage.show methods

        /// <summary>
        /// Show information about a single ServerTemplateMultiCloudImage which represents an association between a ServerTemplate and a MultiCloudImage
        /// </summary>
        /// <param name="serverTemplateMultiCloudImageID">ID of the ServerTemplateMultiCloudImage</param>
        /// <returns>Populated instance of a ServerTemplateMultiCloudImage</returns>
        public static ServerTemplateMultiCloudImage show(string serverTemplateMultiCloudImageID)
        {
            string getHref = string.Format(APIHrefs.ServerTemplateMultiCloudImagesByID, serverTemplateMultiCloudImageID);
            string jsonString = Core.APIClient.Instance.Get(getHref);
            return deserialize(jsonString);
        }

        #endregion

        #region ServerTemplateMultiCloudImage.create methods

        /// <summary>
        /// Creates a new ServerTemplateMultiCloudImage with the given parameters
        /// </summary>
        /// <param name="multiCloudImageID">ID of the MultiCloudImage</param>
        /// <param name="serverTemplateID">ID of the ServerTemplate</param>
        /// <returns>ID of the newly created ServerTemplateMultiCloudImage object</returns>
        public static string create(string multiCloudImageID, string serverTemplateID)
        {
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(Utility.multiCloudImageHref(multiCloudImageID), "server_template_multi_cloud_image[multi_cloud_image_href]", postParams);
            Utility.addParameter(Utility.serverTemplateHref(serverTemplateID), "server_template_multi_cloud_image[server_template_href]", postParams);
            string outString = string.Empty;
            List<string> retVal = Core.APIClient.Instance.Post(APIHrefs.ServerTemplateMultiCloudImages, postParams, "location", out outString);
            return retVal.Last<string>().Split('/').Last<string>();
        }

        #endregion

        #region ServerTemplateMultiCloudImage.destroy methods

        /// <summary>
        /// Deletes a given ServerTemplateMultiCloudImage
        /// </summary>
        /// <param name="serverTemplateMultiCloudImageID">ID of the ServerTemplateMultiCloudImage to delete</param>
        /// <returns>true if success, false if not</returns>
        public static bool destroy(string serverTemplateMultiCloudImageID)
        {
            string destroyHref = string.Format(APIHrefs.ServerTemplateMultiCloudImagesByID, serverTemplateMultiCloudImageID);
            return Core.APIClient.Instance.Delete(destroyHref);
        }

        #endregion

        #region ServerTemplateMultiCloudImage.make_default methods

        /// <summary>
        /// Makes a given ServerTemplateMultiCloudImage the default for the ServerTemplate
        /// </summary>
        /// <param name="serverTemplateMultiCloudImageID">ID of the ServerTemplateMultiCloudImage</param>
        /// <returns>True if successful, false if not</returns>
        public static bool make_default(string serverTemplateMultiCloudImageID)
        {
            string postHref = string.Format(APIHrefs.ServerTemplateMultiCloudImagesMakeDefault, serverTemplateMultiCloudImageID);
            return Core.APIClient.Instance.Post(postHref);
        }

        #endregion

    }
}
