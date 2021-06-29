using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBoardReact.Infrastructure
{
    public interface IDateTimeProvider
    {
        void UpdateTime();
        public DateTime GetTime { get; }

    }
}
