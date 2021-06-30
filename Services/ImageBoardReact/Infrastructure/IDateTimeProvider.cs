using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Board.Api.Infrastructure
{
    public interface IDateTimeProvider
    {
        void UpdateTime();
        public DateTime GetTime { get; }

    }
}
