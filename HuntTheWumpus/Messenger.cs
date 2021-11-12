namespace HuntTheWumpus
{
    static class Messenger
    {
        private const int MESSAGES_MAX_COUNT = 5;
        private static string[] _messages;

        static Messenger()
        {
            _messages = new string[MESSAGES_MAX_COUNT];
        }

        public static void ClearMessages()
        {
            for (int i = 0; i < _messages.Length; i++)
            {
                _messages[i] = null;
            }
        }

        public static void AddMessage(string message)
        {
            bool messageAdded = false;
            for (int i = 0; i < _messages.Length; i++)
            {
                if (_messages[i] == null)
                {
                    _messages[i] = message;
                    messageAdded = true;
                    break;
                }
            }

            if (!messageAdded)
            {
                //Console.WriteLine("ERROR!!!!!!!");
                //TODO: Если сообщение не добавилось, то нужно выбрасывать исключение
            }
        }

        public static string[] GetMessages()
        {
            return _messages;
        }
    }
}
