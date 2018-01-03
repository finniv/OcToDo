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
            else if (team.TeamName == teamName || team.TeamLeader_ID == teamLeader)
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
            var people = (from un in DbContext.Team
                where un.TeamName == teamName
                select un).FirstOrDefault();
            return people?.Team_ID;
        }

        public void AddToTeam(int teamId,int peopleId)
        {
            DbContext.GetTable<Team_content>().InsertOnSubmit(new Team_content() { Team_ID = teamId, People_ID = peopleId });
            DbContext.SubmitChanges();
        }
    }
}
