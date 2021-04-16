﻿using ImageBoardReact.Infrastructure;
using ImageBoardReact.Models.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBoardReact.Models
{
    public interface IRepositoryManager
    {
        int MaxNumberOfThreads { get; }
        Task RemoveOldestThreads();
    }
}