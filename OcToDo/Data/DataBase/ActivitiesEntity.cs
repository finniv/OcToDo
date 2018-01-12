using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcToDo.Data.DataBase
{
    /// <summary>
    /// ActivitiesName 
    /// ActivitiesDescription 
    /// TeamID NOT NULL 
    /// </summary>
    public class ActivitiesEntity:DbConnection
    {
        public sbyte AddActivities(string activitiesName, int teamId)
        {
            sbyte statusCode;
            var activities = (from ac in DbContext.Activities
                where ac.ActivitiesName == activitiesName || ac.Team_ID == teamId
                select ac).FirstOrDefault();
            if (activities == null)
            {
                DbContext.GetTable<Activities>().InsertOnSubmit(new Activities() { ActivitiesName = activitiesName, Team_ID = teamId });
                DbContext.SubmitChanges();
                statusCode = 1;
            }
            else if (activities.Team_ID == teamId && activities.ActivitiesName != activitiesName|| (activities.Team_ID!=teamId && activities.ActivitiesName==activitiesName))
            {
                DbContext.GetTable<Activities>().InsertOnSubmit(new Activities() { ActivitiesName = activitiesName, Team_ID = teamId });
                DbContext.SubmitChanges();
                statusCode = 1;
            }
            else if (activities.ActivitiesName == activitiesName && activities.Team_ID == teamId)
            {
                statusCode = -1;
            }
            else
            {
                statusCode = 0;
            }
            return statusCode;
        }

        public string ShowActivities(int teamId)
        {
            var activities = (from acs in DbContext.Activities
                where acs.Team_ID == teamId
                select acs).ToArray();

            var activitiesList = "";
            for (var i = 0; i < activities.Length; i++)
            {
                activitiesList += "\n" + (i + 1) + "." + activities[i].ActivitiesName+".";
            }

            Debug.WriteLine(activitiesList);
            return activitiesList;
        }

        public int? FindActivitiesIdByIndex(int index, int teamId)
        {
            var activities = (from acs in DbContext.Activities
                where acs.Team_ID == teamId
                select acs).ToArray();
            if (activities == null)
            {
                return null;
            }

            return activities[index - 1].Activities_ID;
        }
    }
}
