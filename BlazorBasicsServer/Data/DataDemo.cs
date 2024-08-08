namespace BlazorBasicsServer.Data
{
    public class DataDemo : IDataDemo
    {
        private readonly int _age;

        public DataDemo()
        {
            var numRnd = new Random();
            _age = numRnd.Next(0, 10);
        }

        public int GetAge()
        {
            return _age;
        }
    }
}
