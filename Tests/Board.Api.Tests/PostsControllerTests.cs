using Castle.Core.Logging;
using Board.Api.Controllers;
using Board.Api.Infrastructure;
using Board.Api.Models;
using Board.Api.Models.Images;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Board.Api.Tests
{
    public class PostsControllerTests
    {
        //IPostsRepository repo,
        //    ILogger<PostsController> logger, 
        //    IHostEnvironment environment,
        //    IRepositoryMonitor repositoryMonitor,
        //    IRepositoryManager repositoryManager,
        //    IImageManager imageManager,
        //    IUserPostsHandler userPostsHandler
        Mock<ILogger<PostsController>> loggerMock = new Mock<ILogger<PostsController>>();
        Mock<IPostsRepository> repositoryMock  = new Mock<IPostsRepository>();
        Mock<IHostEnvironment> environmentMock = new Mock<IHostEnvironment>();
        Mock<IRepositoryMonitor> repositoryMonitorMock = new Mock<IRepositoryMonitor>();
        Mock<IRepositoryManager> repositoryManagerMock = new Mock<IRepositoryManager>();
        Mock<IImageManager> imageManagerMock = new Mock<IImageManager>();
        Mock<IUserPostsHandler> userPostsHandlerMock = new Mock<IUserPostsHandler>();

  
        [Fact]
        public async Task GetReturnsAllThreadsSortedByLastPost()
        {
            // arrange
            PostsController postsController = new PostsController(
                repositoryMock.Object,
                loggerMock.Object,
                environmentMock.Object,
                repositoryMonitorMock.Object,
                repositoryManagerMock.Object,
                imageManagerMock.Object,
                userPostsHandlerMock.Object);
            var dt = DateTime.Parse("10:00");
            repositoryMock.Setup(m => m.GetThreadsInOrderAsync())
                .Returns(Task.FromResult(new List<Post>()
                {
                    new Post {Id = 3, DateTime = dt.AddMinutes(3) },
                    new Post {Id = 1, DateTime = dt.AddMinutes(2) },
                    new Post {Id = 2, DateTime = dt.AddMinutes(1) }
                }));

            // act
            var getResult = await postsController.Get();
            //var contents = getResult as List<Post>;
            var result = getResult as ObjectResult;
            // assert
            Assert.Equal(3, (result.Value as List<Post>).Count());
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);

        }
    }
}
