namespace OcToDo.Model
{
    public static class BotCore
    {
        /// <summary>
        /// Токен-идентификатор бота
        /// </summary>
        private static string _botToken = "482508630:AAGDsUvLR3Lt1ZHFHmaNlPldOK0RKTQWp40";

        private static string _name;

        #region GetSet

        public static string BotToken
        {
            get { return _botToken; }
        }

        public static string Name
        {
            get { return _name; }

            set { _name = value; }
        }


        #endregion
    }
}
