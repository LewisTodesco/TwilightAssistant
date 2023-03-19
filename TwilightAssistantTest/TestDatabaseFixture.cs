using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightAssistantTest
{
    public class TestDatabaseFixture
    {
        private const string ConnectionString = "";

        private static readonly object _lock = new();
        private static bool _databaseInitialised;

        public TestDatabaseFixture()
        {
            lock (_lock) 
            { 
                if (!_databaseInitialised)
                {
                    
                }
            
            }
        }
    }
}
