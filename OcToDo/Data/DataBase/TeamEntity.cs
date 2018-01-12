using System.Diagnostics;
using System.Linq;

namespace OcToDo.Data.DataBase
{
    public class TeamEntity:DbConnection
    {
        public sbyte CreateTeam(string teamName, int teamLeader)
        {
            sbyte statusCode;
            var team = (from tmn in DbContext.Team
                where tmn.TeamName == teamName || tmn.TeamLeader_ID == teamLeader
                select tmn).FirstOrDefault();
            if (team == null)
            {
                DbContext.GetTable<Team>().InsertOnSubmit(new Team { TeamName = teamName, TeamLeader_ID = teamLeader });
                DbContext.SubmitChanges();
                statusCode = 1;
            }
            else if ((team.TeamName == teamName && team.TeamLeader_ID != teamLeader)||(team.TeamName!=teamName && team.TeamLeader_ID==teamLeader))
            {
                DbContext.GetTable<Team>().InsertOnSubmit(new Team { TeamName = teamName, TeamLeader_ID = teamLeader });
                DbContext.SubmitChanges();
                statusCode = 2;
            }
            else if (team.TeamName==teamName && team.TeamLeader_ID == teamLeader)
            {
                statusCode = -1;
            }
            else
            {
                statusCode = 0;
            }
            
            return statusCode;
        }

        public int? FindTeam(string teamName)
        {
            var team = (from un in DbContext.Team
                where un.TeamName == teamName
                select un).FirstOrDefault();
            return team?.Team_ID;
        }

        public sbyte AddToTeam(int teamId,int peopleId)
        {
            sbyte statucCode=0;
            var teamContains = (from teamContent in DbContext.Team_content
                where teamContent.People_ID == teamId || teamContent.Team_ID == teamId
                select teamContent).SingleOrDefault();
            if (teamContains == null)
            {
                DbContext.GetTable<Team_content>()
                    .InsertOnSubmit(new Team_content() {Team_ID = teamId, People_ID = peopleId});
                DbContext.SubmitChanges();
               return statucCode = 1;
            }

            if ((teamContains.Team_ID==teamId && teamContains.People_ID!=peopleId) || (teamContains.Team_ID!=teamId && teamContains.People_ID==peopleId))
            {
                DbContext.GetTable<Team_content>()
                    .InsertOnSubmit(new Team_content() { Team_ID = teamId, People_ID = peopleId });
                DbContext.SubmitChanges();
                return statucCode = 1;
            }
            if(teamContains.People_ID==peopleId&&teamContains.Team_ID==teamId)
            {
               return statucCode = 0;
            }

            return statucCode;
        }

        public string ShowTeam(string username)
        {
            var people = (from un in DbContext.People
                          where un.UserName == username
                          select un).FirstOrDefault();
            var team = (from t in DbContext.Team
                        where t.TeamLeader_ID == people.People_ID
                        select t).ToArray();
            var teamList = "";
            for (var i=0;i<team.Length;i++)
            {
                teamList += "\n" + (i+1)+"." + team[i].TeamName;
            }
            
            Debug.WriteLine(teamList);
            return teamList;
        }

        public int? FindTeamIdByIndex(int index, string username)
        {
            var peopleId = (from p in DbContext.People
                where p.UserName == username
                select p).Single();
            if (peopleId == null)
            {
                return null;
            }

            var teamId = (from team in DbContext.Team
                where team.TeamLeader_ID == peopleId.People_ID
                select team).ToArray();
            return teamId[index-1].Team_ID;
        }

        public int? GetTeamContentId(int teamId, string username)
        {
            var peopleId = (from p in DbContext.People
                where p.UserName == username
                select p).Single();
            if (peopleId==null)
            {
                return null;
            }

            var teamContentId = (from tc in DbContext.Team_content
                where tc.Team_ID == teamId && tc.People_ID == peopleId.People_ID
                select tc).Single();
            if (teamContentId==null)
            {
                return null;
            }

            return teamContentId.TeamContetn_ID;
        }
    }
}
