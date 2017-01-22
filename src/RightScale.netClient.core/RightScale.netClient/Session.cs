﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// Session is the first resource that API users interact with, it has links to root resources.
    /// Resources Reference: http://reference.rightscale.com/api1.5/resources/ResourceSessions.html
    /// MediaType Reference: http://reference.rightscale.com/api1.5/media_types/MediaTypeSession.html
    /// </summary>
    public class Session:Core.RightScaleObjectBase<Session>
    {
        #region Session Properties

        /// <summary>
        /// Message related to this Session object
        /// </summary>
        public string message { get; set; }

        #endregion

        #region Session.ctor
        /// <summary>
        /// Default Constructor for Session
        /// </summary>
        public Session()
            : base()
        {
        }
        
        #endregion

        #region Session.index methods

        /// <summary>
        /// Returns a list of root resources so an authenticated session can use them as a starting point or a way to know what features are available within its privileges.
        /// </summary>
        /// <returns>list of session objects</returns>
        public static Session index()
        {
            string getHref = "/api/session";
            string jsonString = Core.APIClient.Instance.Get(getHref);
            var j = deserialize(jsonString);
            return j;
        }

        #endregion

        #region Session.create methods

        /// <summary>
        /// Creates API session scoped to a given account. (API login)
        /// This call requires a form of authentication (user and password), as well as the account for which the session needs to be created. Upon successfully authenticating the credentials, the system will return a 204 code and set of two cookies that will serve as the credentials for the session. Both of these cookies must be passed in any of the subsequent requests for this session. If an 302 redirect code is returned, the client is responsible of re-issuing the POST request against the content of the received Location header, passing the exact same parameters again.
        /// </summary>
        /// <param name="email">The email to login with</param>
        /// <param name="password">The corresponding password</param>
        /// <param name="accountID">The account id for which the session needs to be created.</param>
        public static bool create(string email, string password, string accountID)
        {
            Core.APIClient.Instance.InitWebClient();
            return Core.APIClient.Instance.Authenticate(email, password, accountID);
        }

        #endregion

        #region Session.accounts methods

        /// <summary>
        /// List all the accounts that a user has access to before asking for a session
        /// This call requires a form of authentication (user and password). Upon successfully authenticating the credentials, the system will return a 200 OK code and return the list of accounts. If an 302 redirect code is returned, the client is responsible of re-issuing the POST request against the content of the received Location header, passing the exact same parameters again.
        /// </summary>
        /// <param name="email">The email to log in with</param>
        /// <param name="password">The corresponding password</param>
        /// <returns></returns>
        public static List<Account> accounts(string email, string password)
        {
            string queryString = string.Format("email={0}&password={1}", email, password);

            string jsonString = Core.APIClient.Instance.Get(APIHrefs.SessionAccount, queryString);

            return Account.populateObjectListFromJson(jsonString);
        }

        #endregion

        #region Session.create_instance_session

        /// <summary>
        /// Creates API session scoped to a given account and instance.
        /// This call requires a form of authentication (token), as well as the account for which the session needs to be created. Upon successfully authenticating the credentials, the system will return a 204 code and set of two cookies that will serve as the credentials for the session. Both of these cookies must be passed in any of the subsequent requests for this session. If an 302 redirect code is returned, the client is responsible of re-issuing the POST request against the content of the received Location header, passing the exact same parameters again
        /// </summary>
        /// <param name="accountID">The account id for which the session needs to be created</param>
        /// <param name="instanceToken">The instance token to login with</param>
        public static bool create_instance_session(string instanceToken)
        {
            Core.APIClient.Instance.InitWebClient();
            return Core.APIClient.Instance.Authenticate_Instance(instanceToken);
        }

        #endregion

        #region Session.index_instance_session
        
        /// <summary>
        /// Shows the full attributes of the instance (that has the token used to log-in). This call can be used by an instance to get it's own details
        /// </summary>
        /// <returns>Returns an Instance object for the current server instance accessed via create_instance_session</returns>
        public static Instance index_instance_session()
        {
            string getHref = "/api/session/instance";
            string jsonString = Core.APIClient.Instance.Get(getHref);
            return Instance.populateObjectFromJson(jsonString);
        }

        #endregion

    }
}
