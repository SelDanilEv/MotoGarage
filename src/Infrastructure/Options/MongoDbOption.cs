namespace Infrastructure.Options
{
    public class MongoDbOption
    {
        public string AppName { get; set; }
        public string ConnectionString { get; set; }
        public string AppPrefix { get; set; }
        public string EnvironmentPrefix { get; set; }

        public string GetConnectionString => string.Format(ConnectionString, AppName);
    }
}
