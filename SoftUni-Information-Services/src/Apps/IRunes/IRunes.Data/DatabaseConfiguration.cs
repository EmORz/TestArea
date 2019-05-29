namespace IRunes.Data
{
    public class DatabaseConfiguration
    {
        //public const string ConnectionString =
        //    @"Server=.;Database=IRunesDB;Trusted_Connection=True;Integrated Security=True;";
        //todo problem was with connection to server ! Fixed
        public const string ConnectionString =
            "Server=.\\SQLEXPRESS;Database=IRunesDb;Trusted_Connection=True;Integrated Security=True";
    }
}
