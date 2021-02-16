namespace ChatApplication
{
    public static class ChatConstants
    {
        public const int RetryDelay = 5_000;
        public static readonly string ErrorMessage = "Something get wrong.Please, try later.";
        public static readonly string ByeMessage = "Bye!!!";
        public static readonly string Me = "Me";
        public static readonly string GreetingMessage = "Welcome.Please,enter your name.";
        public static readonly string WaitMessage = "Waiting for other user";
        public static readonly string ConnectionEstablished = "Connection established.You can write messages";
        public static readonly string ParseError = "Message from remote recieved with error";
        public static readonly string RecieveError = "Something get wrong with your message.It was not delivered";
        public static readonly string FriendReturned = "Your friend returned";
        public static readonly string OtherUserStillOnLine = "Seems like other use is still on-line.Recover chat history";
    }
}