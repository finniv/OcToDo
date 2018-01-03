using System.Linq;

namespace OcToDo.Data.DataBase
{
    public class PeopleEntity : DbConnection
    {
        
        #region Register
        public sbyte Register(string userName,int userId)
        {
            sbyte statusCode;
            var people = (from un in DbContext.People
                where un.UserName == userName || un.Telegram_ID == userId
                select un).FirstOrDefault();

            if (people == null)
            {
                DbContext.GetTable<People>().InsertOnSubmit(new People() { UserName = userName, Telegram_ID = userId });
                DbContext.SubmitChanges();
                statusCode = 1;
            }
            else if (people.Telegram_ID == userId && people.UserName != userName)
            {
                people.UserName = userName;
                statusCode = 2;
                DbContext.SubmitChanges();
            }
            else if (people.Telegram_ID == userId && people.UserName == userName)
            {
                statusCode = -1;
            }
            else
            {
                statusCode = 0;
            }
            return statusCode;
        }
        #endregion

        public int? FindPeople(string userName)
        {
            var people = (from un in DbContext.People
                where un.UserName == userName
                select un).FirstOrDefault();
            return people?.People_ID;
        }

        public bool Authorize(string telegramId)
        {
            var isRegisteredPeople = (from reg in DbContext.People
                where reg.UserName == telegramId
                select reg).SingleOrDefault();
            var status = isRegisteredPeople != null;

            return status;
        }
    }
}
