﻿using System.Linq;

namespace OcToDo.Data.DataBase
{
    public class PeopleEntity 
    {
        private OcToDoDataContext DbContext { get; } = new OcToDoDataContext();
        #region Register
        public byte Register(string userName,int userId)
        {
            byte statusCode;
            var people = (from un in DbContext.People
                where un.UserName == userName || un.Telegram_ID == userId
                select un).SingleOrDefault();

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
                statusCode = 3;
            }
            else
            {
                statusCode = 0;
            }
            return statusCode;
        }
        #endregion

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
