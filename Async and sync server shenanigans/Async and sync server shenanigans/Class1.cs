using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Async_and_sync_server_shenanigans
{
    internal class Fakeserver
    {

        public Fakeserver()
        {
        }

        public int Fakerequest()
        {

            Random rand = new Random();
            int randomInt = rand.Next(1, 101);

            Thread.Sleep(3000);

            return randomInt;
        }
        public async Task<int> FakerequestAsync()
        {

            Random rand = new Random();
            int randomInt = rand.Next(1, 101);

            await Task.Delay(3000);

            return randomInt;
        }

    }
}
