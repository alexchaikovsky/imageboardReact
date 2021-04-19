using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBoardReact.Infrastructure
{
    public class UTCDateTimeProvider : IDateTimeProvider
    {
        DateTime dateTime = DateTime.UtcNow;
        public DateTime GetTime => dateTime;

        public void UpdateTime()
        {
            dateTime = DateTime.UtcNow;
        }
    }
}
