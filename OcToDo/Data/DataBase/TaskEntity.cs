using System.Diagnostics;
using System.Linq;

namespace OcToDo.Data.DataBase
{
    class TaskEntity:DbConnection
    {
        public sbyte AddTask(string taskName, int activitiesId, int teamContentId)
        {
            sbyte statusCode;
            var task = (from ts in DbContext.Task
                where ts.TeamContent_ID==teamContentId && ts.Activities_ID==activitiesId
                select ts).FirstOrDefault();
            if (task == null)
            {
                DbContext.GetTable<Task>().InsertOnSubmit(new Task() { Activities_ID = activitiesId, TeamContent_ID = teamContentId, TaskName = taskName });
                DbContext.SubmitChanges();
                statusCode = 1;
            }
            else if (task.Activities_ID==activitiesId && task.TeamContent_ID==teamContentId && task.TaskName!=taskName|| task.Activities_ID==activitiesId && task.TaskName==taskName && task.TeamContent_ID!=teamContentId)
            {
                DbContext.GetTable<Task>().InsertOnSubmit(new Task() { Activities_ID = activitiesId, TeamContent_ID = teamContentId,TaskName = taskName});
                DbContext.SubmitChanges();
                statusCode = 1;
            }
            else if (task.Activities_ID == activitiesId && task.TeamContent_ID == teamContentId&& task.TaskName==taskName)
            {
                statusCode = -1;
            }
            else
            {
                statusCode = 0;
            }
            return statusCode;
        }

        public string ShowTask(string username)
        {
            var task = (from ts in DbContext.AllTask
                where ts.UserName == username
                select ts).ToArray();
            
            var taskList = "";
            for (var i = 0; i < task.Length; i++)
            {
                var taskStatus = task[i].TaskStatus;
                var statuscode = "";
                switch (taskStatus)
                {
                    case 0:
                        statuscode = "\U00002B55";
                        break;
                    case 1:
                        statuscode = "\U00002705";
                        break;
                    case -1:
                        statuscode = "\U0000274C";
                        break;
                }

                if (i==0)
                {
                    taskList+= "\n*" + task[i].TeamName+"*";
                }
                else if (task[i - 1].Team_ID != task[i].Team_ID)
                {
                    taskList += "\n*" + task[i].TeamName+"*";
                }
                taskList += "\n" + (i + 1) + "." + task[i].TaskName+" : "+statuscode;
            }

            Debug.WriteLine(taskList);
            return taskList;
        }

        public string ShowTask(int teamId)
        {
            var taskList = "";
            var task = (from ts in DbContext.AllTask
                where ts.Team_ID == teamId
                select ts).OrderBy(s=>s.Activities_ID).ToArray();
            for (var i = 0; i < task.Length; i++)
            {
                var taskStatus = task[i].TaskStatus;
                var statuscode = "";
                switch (taskStatus)
                {
                    case 0:
                        statuscode = "\U00002B55";
                        break;
                    case 1:
                        statuscode = "\U00002705";
                        break;
                    case -1:
                        statuscode = "\U0000274C";
                        break;
                }

                if (i == 0)
                {
                    taskList += "\n*" + task[i].TeamName + "*";
                    taskList += "\n_*" + task[i].ActivitiesName + "*_";
                }
                else if (task[i - 1].Activities_ID != task[i].Activities_ID)
                {
                    taskList += "\n_*" + task[i].ActivitiesName + "*_";
                }
                taskList += "\n" + (i + 1) + "." + task[i].TaskName + " : " + statuscode + " " + task[i].UserName;
            }
            return taskList;
        }

        public void UpdateStatus(int index,string username, short status)
        {
            var allTask = (from ts in DbContext.AllTask
                where ts.UserName == username
                select ts).ToArray();
            switch (status)
            {
                case 3:
                    status = 0;
                    break;
                case 2:
                    status = -1;
                    break;
                case 1:
                    status = 1;
                    break;
                    default:
                        status = 0;
                        break;
            }

            var task = (from t in DbContext.Task
                where t.Task_ID == allTask[index - 1].Task_ID
                select t).Single();
            task.TaskStatus = status;
            DbContext.SubmitChanges();
        }
    }
}
