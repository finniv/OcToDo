using System.Data.Linq;
using System.Linq;

namespace OcToDo.Data.DataBase
{
    public class PeopleEntity
    {
        public void Register(string Login,string Password,string Telegram_ID)
        {
            OcToDoDataContext db = new OcToDoDataContext();
            db.GetTable<People>().InsertOnSubmit(new People() {Login = Login, Password = Password, Telegram_ID = Telegram_ID});
        }

        public bool Register(string Telegram_ID)
        {

            OcToDoDataContext dbContext = new OcToDoDataContext();

            People username = (from un in dbContext.People
                where un.Telegram_ID == Telegram_ID
                select un).SingleOrDefault<People>();

            if (username == null)
            {
                dbContext.GetTable<People>().InsertOnSubmit(new People() { Telegram_ID = Telegram_ID });
                dbContext.SubmitChanges();
            }
            else if  (username.Telegram_ID==Telegram_ID)
            {
                return false;
            }
            

            return true;
        }

        public void Register(string FName, string MName, string LName, string Adress, string Phone, string BirthDate,
            string Telegram_ID, string Login, string Password)
        {

        }
    }
}
