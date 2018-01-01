using System.Linq;
using System.Security.Cryptography;
using System.Data.Linq;

namespace OcToDo.Data.DataBase
{
    public class PeopleEntity
    {
        public OcToDoDataContext DbContext { get; } = new OcToDoDataContext();

        #region Register

        public void Register(string login,string password,string telegramId)
        {
            OcToDoDataContext db = new OcToDoDataContext();
            db.GetTable<People>().InsertOnSubmit(new People() {Login = login, Password = password, Telegram_ID = telegramId});
        }

        public bool Register(string telegramId)
        {
            bool status = false;
            People username = (from un in DbContext.People
                where un.Telegram_ID == telegramId
                select un).SingleOrDefault();

            if (username == null)
            {
                DbContext.GetTable<People>().InsertOnSubmit(new People() { Telegram_ID = telegramId });
                DbContext.SubmitChanges();
                status = true;
            }
            else if  (username.Telegram_ID==telegramId)
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
                where reg.Telegram_ID == telegramId
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
