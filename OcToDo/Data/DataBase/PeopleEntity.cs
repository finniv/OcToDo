using System.Linq;
using System.Security.Cryptography;
using System.Data.Linq;

namespace OcToDo.Data.DataBase
{
    public class PeopleEntity
    {
        public OcToDoDataContext DbContext { get; } = new OcToDoDataContext();

        #region Register
        public bool Register(string userName,int userId)
        {
            bool status = false;
            People username = (from un in DbContext.People
                where un.UserName == userName || un.Telegram_ID == userId
                select un).SingleOrDefault();

            if (username == null)
            {
                DbContext.GetTable<People>().InsertOnSubmit(new People() { UserName = userName,Telegram_ID=userId });
                DbContext.SubmitChanges();
                status = true;
            }
            else if  (username.UserName==userName)
            {
                status = false;
            }
            return status;
        }

        public void Register(string fName, string mName, string lName, string adress, string phone, string birthDate,
            string telegramId, string login, string password)
        {

        }

        #endregion

        public bool Authorize(string telegramId)
        {
            bool status = false;
            People isRegisteredPeople = (from reg in DbContext.People
                where reg.UserName == telegramId
                select reg).SingleOrDefault();
            if (isRegisteredPeople != null)
            {
                status = true;
            }
            else if (isRegisteredPeople == null)
            {
                status = false;
            }
            return status;
        }
    }
}
