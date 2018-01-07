using System.Diagnostics;
using System.Linq;

namespace OcToDo.Data.DataBase
{
    class TaskEntity:DbConnection
    {
        public sbyte AddTask(string taskName, int activitiesId, int peopleId)
        {
            sbyte statusCode;
            var task = (from ts in DbContext.Task
                where ts.TeamContent_ID==peopleId || ts.Activities_ID==activitiesId
                select ts).FirstOrDefault();
            if (task == null)
            {
                DbContext.GetTable<Task>().InsertOnSubmit(new Task() { Activities_ID = activitiesId, TeamContent_ID = peopleId, TaskName = taskName });
                DbContext.SubmitChanges();
                statusCode = 1;
            }
            else if (task.Activities_ID==activitiesId && task.TeamContent_ID==peopleId && task.TaskName!=taskName|| task.Activities_ID==activitiesId && task.TaskName==taskName && task.TeamContent_ID!=peopleId)
            {
                DbContext.GetTable<Task>().InsertOnSubmit(new Task() { Activities_ID = activitiesId, TeamContent_ID = peopleId,TaskName = taskName});
                DbContext.SubmitChanges();
                statusCode = 1;
            }
            else if (task.Activities_ID == activitiesId && task.TeamContent_ID == peopleId&& task.TaskName==taskName)
            {
                statusCode = -1;
            }
            else
            {
                statusCode = 0;
            }
            return statusCode;
        }

        public string ShowTask(int activitiesId)
        {
            var task = (from ts in DbContext.Task
                where ts.Activities_ID == activitiesId
                select ts).ToArray();
            
            var taskList = "";
            for (var i = 0; i < task.Length; i++)
            {
                taskList += "\n" + (i + 1) + "." + task[i].TaskName;
            }

            Debug.WriteLine(taskList);
            return taskList;
        }
    }
}
