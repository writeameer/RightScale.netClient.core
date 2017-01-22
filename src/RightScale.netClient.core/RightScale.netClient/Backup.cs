﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    public class Backup : Core.RightScaleObjectBase<Backup>
    {
        public string name { get; set; }
        public string lineage { get; set; }
        public string created_at { get; set; }
        public List<VolumeSnapshot> volume_snapshots { get; set; }
        public bool completed { get; set; }
        public bool from_master { get; set; }
        public int volume_snapshot_count { get; set; }
        public bool committed { get; set; }
        public string description { get; set; }

        #region Backup.ctor
        /// <summary>
        /// Default Constructor for Backup
        /// </summary>
        public Backup()
            : base()
        {
        }

        #endregion

        #region Backup.show() methods

        /// <summary>
        /// Lists the attributes of a given backup
        /// </summary>
        /// <param name="backupID">ID of the backup to return</param>
        /// <returns>populated instance of Backup object specified</returns>
        public static Backup show(string backupID)
        {
            Utility.CheckStringIsNumeric(backupID);
            string getURL = string.Format("/api/backups/{0}", backupID);
            string jsonString = Core.APIClient.Instance.Get(getURL);
            return deserialize(jsonString);
        }

        #endregion

        #region Backup.index methods

        /// <summary>
        /// Lists all of the backups with the given lineage tag. Filters can be used to search for a particular backup. If the 'latest_before' filter is set, only one backup is returned (the latest backup before the given timestamp).
        /// To get the latest completed backup, the 'completed' filter should be set to 'true' and the 'latest_before' filter should be set to the current timestamp. The format of the timestamp must be YYYY/MM/DD HH:MM:SS [+/-]ZZZZ e.g. 2011/07/11 00:00:00 +0000.
        /// To get the latest completed backup just before, say 25 June 2009, then the 'completed' filter should be set to 'true' and the 'latest_before' filter should be set to 2009/06/25 00:00:00 +0000.
        /// </summary>
        /// <returns>Collection of Backup objects</returns>
        public static List<Backup> index()
        {
            return index(null, null);
        }

        /// Lists all of the backups with the given lineage tag. Filters can be used to search for a particular backup. If the 'latest_before' filter is set, only one backup is returned (the latest backup before the given timestamp).
        /// To get the latest completed backup, the 'completed' filter should be set to 'true' and the 'latest_before' filter should be set to the current timestamp. The format of the timestamp must be YYYY/MM/DD HH:MM:SS [+/-]ZZZZ e.g. 2011/07/11 00:00:00 +0000.
        /// To get the latest completed backup just before, say 25 June 2009, then the 'completed' filter should be set to 'true' and the 'latest_before' filter should be set to 2009/06/25 00:00:00 +0000.
        /// <param name="filter">Filters limiting the return set from the API</param>
        /// <returns>Collection of Backup objects</returns>
        public static List<Backup> index(List<Filter> filter)
        {
            return index(filter, null);
        }

        /// Lists all of the backups with the given lineage tag. Filters can be used to search for a particular backup. If the 'latest_before' filter is set, only one backup is returned (the latest backup before the given timestamp).
        /// To get the latest completed backup, the 'completed' filter should be set to 'true' and the 'latest_before' filter should be set to the current timestamp. The format of the timestamp must be YYYY/MM/DD HH:MM:SS [+/-]ZZZZ e.g. 2011/07/11 00:00:00 +0000.
        /// To get the latest completed backup just before, say 25 June 2009, then the 'completed' filter should be set to 'true' and the 'latest_before' filter should be set to 2009/06/25 00:00:00 +0000.
        /// <param name="lineage">Backups belonging to this lineage</param>
        /// <returns>Collection of Backup objects</returns>
        public static List<Backup> index(string lineage)
        {
            return index(null, lineage);
        }

        /// Lists all of the backups with the given lineage tag. Filters can be used to search for a particular backup. If the 'latest_before' filter is set, only one backup is returned (the latest backup before the given timestamp).
        /// To get the latest completed backup, the 'completed' filter should be set to 'true' and the 'latest_before' filter should be set to the current timestamp. The format of the timestamp must be YYYY/MM/DD HH:MM:SS [+/-]ZZZZ e.g. 2011/07/11 00:00:00 +0000.
        /// To get the latest completed backup just before, say 25 June 2009, then the 'completed' filter should be set to 'true' and the 'latest_before' filter should be set to 2009/06/25 00:00:00 +0000.
        /// <param name="filter">Filters limiting the return set from the API</param>
        /// <param name="lineage">Backups belonging to this lineage</param>
        /// <returns>Collection of Backup objects</returns>
        public static List<Backup> index(List<Filter> filter, string lineage)
        {
            Utility.CheckStringHasValue(lineage);			
            List<string> validFilters = new List<string>() { "cloud_href", "committed", "completed", "from_master", "latest_before" };
            Utility.CheckFilterInput("filter", validFilters, filter);
            string queryString = string.Empty;
            if (filter != null && filter.Count > 0)
            {
                foreach (Filter f in filter)
                {
                    queryString += f.ToString() + "&";
                }
            }
            queryString += string.Format("lineage={0}", lineage);
            string jsonString = Core.APIClient.Instance.Get(APIHrefs.Backup, queryString);
            return deserializeList(jsonString);
        }
		#endregion

        #region Backup.create() methods

        /// <summary>
        /// Takes in an array of volumeattachment IDs and takes a snapshot of each. The volumeattachment IDs must belong to the same instance.
        /// </summary>
        /// <param name="name">The name to be set on each of the volume snapshots</param>
        /// <param name="volumeAttachmentIDs">Array of volume attachment IDs that are to be backed-up</param>
        /// <param name="lineage">A unique value to create backups belonging to a particular system. This will be used to set the tag e.g. 'rs_backup:lineage=prod_mysqldb'</param>
        /// <returns>ID of the newly created Backup object</returns>
        public static string create(string name, string[] volumeAttachmentIDs, string lineage)
        {
            return create(name, volumeAttachmentIDs.ToList<string>(), lineage);
        }

        /// <summary>
        /// Takes in an array of volumeattachment IDs and takes a snapshot of each. The volumeattachment IDs must belong to the same instance.
        /// </summary>
        /// <param name="name">The name to be set on each of the volume snapshots</param>
        /// <param name="volumeAttachmentIDs">List of volume attachment IDs that are to be backed-up</param>
        /// <param name="lineage">A unique value to create backups belonging to a particular system. This will be used to set the tag e.g. 'rs_backup:lineage=prod_mysqldb'</param>
        /// <returns>ID of the newly created Backup object</returns>
        public static string create(string name, List<string> volumeAttachmentIDs, string lineage)
        {
            return create(name, volumeAttachmentIDs, lineage, null, true);
        }

        /// <summary>
        /// Takes in an array of volumeattachment IDs and takes a snapshot of each. The volumeattachment IDs must belong to the same instance.
        /// </summary>
        /// <param name="name">The name to be set on each of the volume snapshots</param>
        /// <param name="volumeAttachmentIDs">Array of volume attachment IDs that are to be backed-up</param>
        /// <param name="lineage">A unique value to create backups belonging to a particular system. This will be used to set the tag e.g. 'rs_backup:lineage=prod_mysqldb'</param>
        /// <param name="description">The description to be set on each of the volume snapshots</param>
        /// <param name="fromMaster">Setting this to 'true' will create a tag 'rs_backup:from_master=true' on the snapshots so that one can filter them later</param>
        /// <returns>ID of the newly created Backup object</returns>
        public static string create(string name, string[] volumeAttachmentIDs, string lineage, string description, bool fromMaster)
        {
            return create(name, volumeAttachmentIDs.ToList<string>(), lineage, description, fromMaster);
        }

        /// <summary>
        /// Takes in an array of volumeattachment IDs and takes a snapshot of each. The volumeattachment IDs must belong to the same instance.
        /// </summary>
        /// <param name="name">The name to be set on each of the volume snapshots</param>
        /// <param name="volumeAttachmentIDs">List of volume attachment IDs that are to be backed-up</param>
        /// <param name="lineage">A unique value to create backups belonging to a particular system. This will be used to set the tag e.g. 'rs_backup:lineage=prod_mysqldb'</param>
        /// <param name="description">The description to be set on each of the volume snapshots</param>
        /// <param name="fromMaster">Setting this to 'true' will create a tag 'rs_backup:from_master=true' on the snapshots so that one can filter them later</param>
        /// <returns>ID of the newly created Backup object</returns>
        public static string create(string name, List<string> volumeAttachmentIDs, string lineage, string description, bool fromMaster)
        {
            Utility.CheckStringHasValue(name);
            Utility.CheckStringHasValue(lineage);
            if (volumeAttachmentIDs == null || volumeAttachmentIDs.Count == 0)
            {
                throw new ArgumentException("VolumeAttachmentID collection did not contain any VolumeAttachment IDs");
            }

            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(name, "backup[name]", postParams);
            Utility.addParameter(lineage, "backup[lineage]", postParams);
            foreach (string vaid in volumeAttachmentIDs)
            {
                Utility.addParameter("backup[volume_attachment_hrefs]", string.Format(APIHrefs.VolumeAttachmentByID, vaid), postParams);
            }
            Utility.addParameter(description, "backup[description]", postParams);
            Utility.addParameter(fromMaster.ToString().ToLower(), "backup[from_master]", postParams);
            return Core.APIClient.Instance.Post(APIHrefs.Backup, postParams, "location").Last<string>().Split('/').Last<string>();
        }

        #endregion

        #region Backup.update() methods

        /// <summary>
        /// Updates the committed tag for all of the VolumeSnapshots in the given Backup to the given value.
        /// </summary>
        /// <param name="backupID">ID of the backup to be updated</param>
        /// <param name="committed">Setting this to 'true' will update the 'rs_backup:committed=false' tag to 'rs_backup:committed=true' on all the snapshots</param>
        /// <returns>true if updated, false if not</returns>
        public static bool update(string backupID, bool committed)
        {
            string putHref = string.Format(APIHrefs.BackupByID, backupID);
            List<KeyValuePair<string, string>> putParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(committed.ToString().ToLower(), "backup[committed]", putParams);
            return Core.APIClient.Instance.Put(putHref, putParams);
        }

        #endregion

        #region Backup.cleanup() methods

        /// <summary>
        /// Deletes old backups that meet the given criteria. For example, if a user calls cleanup with keep monthlies set to 12, then the latest backup for each month, for 12 months, will be kept.
        /// </summary>
        /// <param name="lineage">The lineage of the backups that are to be cleaned-up</param>
        /// <param name="keepLast">The number of backups that should be kept</param>
        /// <returns>true if successful, false if not</returns>
        public static bool cleanup(string lineage, int keepLast)
        {
            return cleanup(lineage, keepLast, null, -1, -1, -1, -1);
        }

        /// <summary>
        /// Deletes old backups that meet the given criteria. For example, if a user calls cleanup with keep monthlies set to 12, then the latest backup for each month, for 12 months, will be kept.
        /// </summary>
        /// <param name="lineage">The lineage of the backups that are to be cleaned-up</param>
        /// <param name="keepLast">The number of backups that should be kept</param>
        /// <param name="cloudID">Backups belonging to only this cloud are considered for cleanup. Otherwise, all backups in the account with the same lineage will be considered</param>
        /// <param name="dailies">The number of daily backups(the latest one in each day) that should be kept</param>
        /// <param name="weeklies">The number of weekly backups(the latest one in each week) that should be kept</param>
        /// <param name="monthlies">The number of monthly backups(the latest one in each month) that should be kept</param>
        /// <param name="yearlies">The number of yearly backups(the latest one in each year) that should be kept</param>
        /// <returns>true if succesful, false if not</returns>
        public static bool cleanup(string lineage, int keepLast, string cloudID, int dailies, int weeklies, int monthlies, int yearlies)
        {
            Utility.CheckStringHasValue(lineage);

            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(keepLast, "keep_last", postParams);
            Utility.addParameter(string.Format(APIHrefs.CloudByID, cloudID), "cloud_href", postParams);
            Utility.addParameter(dailies, "dailies", postParams);
            Utility.addParameter(lineage, "lineage", postParams);
            Utility.addParameter(monthlies, "monthlies", postParams);
            Utility.addParameter(weeklies, "weeklies", postParams);
            Utility.addParameter(yearlies, "yearlies", postParams);

            return Core.APIClient.Instance.Post(APIHrefs.BackupCleanup, postParams);
        }

        #endregion

        #region Backup.destroy()

        /// <summary>
        /// Deletes a given backup by deleting all of its snapshots, this call will succeed even if the backup has not completed.
        /// </summary>
        /// <param name="backupID">ID of the backup to delete</param>
        /// <returns>true if successful, false if not</returns>
        public static bool destroy(string backupID)
        {
            string deleteHref = string.Format(APIHrefs.BackupByID, backupID);
            return Core.APIClient.Instance.Delete(deleteHref);
        }

        #endregion

        #region Backup.restore()

        /// <summary>
        /// Restores the given Backup
        /// </summary>
        /// <param name="backupID">ID of the backup to restore</param>
        /// <param name="instanceID">ID of the instance that the backup will be restored to</param>
        /// <returns>ID of the Task to follow the status</returns>
        public static string restore(string backupID, string instanceID)
        {
            return restore(backupID, instanceID, null, null, -1, null, -1);
        }

        /// <summary>
        /// Restores the given Backup
        /// </summary>
        /// <param name="backupID">ID of the backup to restore</param>
        /// <param name="instanceID">ID of the instance that the backup will be restored to</param>
        /// <param name="name">Each volume is created with this name instead of the volume snapshot's name</param>
        /// <param name="description">Each volume is created with this description instead of the volume snapshot's description</param>
        /// <param name="size">Each volume is created with this size in gigabytes (GB) instead of the volume snapshot's size (must be equal or larger). Some volume types have predefined sizes and do not allow selecting a custom size on volume creation</param>
        /// <param name="volumeTypeID">The ID of the volume type. Each volume is created with this volume type instead of the default volume type for the cloud. A Name, Resource UID and optional Size is associated with a volume type</param>
        /// <param name="iops">The number of IOPS (I/O Operations Per Second) each volume should support. Only available on clouds supporting performance provisioning</param>
        /// <returns>ID of the Task to follow the status</returns>
        public static string restore(string backupID, string instanceID, string name, string description, int size, string volumeTypeID, int iops)
        {
            Utility.CheckStringHasValue(instanceID);
            Utility.CheckStringHasValue(backupID);

            string postHref = string.Format(APIHrefs.BackupByID, backupID);
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            Utility.addParameter(description, "backup[description]", postParams);
            Utility.addParameter(iops, "backup[iops]", postParams);
            Utility.addParameter(name, "backup[name]", postParams);
            Utility.addParameter(size, "backup[size]", postParams);
            Utility.addParameter(string.Format(APIHrefs.VolumeTypeByID, volumeTypeID), "backup[volume_type_href]", postParams);
            Utility.addParameter(string.Format(APIHrefs.InstanceByID, instanceID), "instance_href", postParams);
            return Core.APIClient.Instance.Post(postHref, postParams, "location").Last<string>().Split('/').Last<string>();
        }

        #endregion
    }
}
