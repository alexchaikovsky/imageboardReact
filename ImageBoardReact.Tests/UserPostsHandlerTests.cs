using ImageBoardReact.Infrastructure;
using ImageBoardReact.Models;
using ImageBoardReact.Models.Images;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ImageBoardReact.Tests
{
    public class UserPostsHandlerTests
    {
        Mock<IImageManager> imageManagerMock = new Mock<IImageManager>();
        Mock<IDateTimeProvider> dateTimeProviderMock = new();

        [Fact]
        public async Task PostBuildsCorrectlyFromUserPostWithOutImages() 
        {
            // arrange
            dateTimeProviderMock.Setup(x => x.GetTime).Returns(DateTime.Parse("10:00"));
            imageManagerMock
                .Setup(x => x.SaveImagesPackAsync(It.IsAny<List<IFormFile>>()))
                .Returns(Task.FromResult(Array.Empty<string>()));

            UserPostsHandler userPostsHandler = new(imageManagerMock.Object, dateTimeProviderMock.Object);
            
            UserPost userPost = new UserPost() { Text = "Hello" };
            int threadId = 1;
            // act
            var post = await userPostsHandler.BuildFromUserPostAsync(userPost, threadId);

            // assert

            Assert.Equal(threadId, post.ThreadId);
            Assert.Equal(DateTime.Parse("10:00"), post.DateTime);
            Assert.Equal(Array.Empty<string>(), post.ImagesSource);
            Assert.Equal("Hello", post.Text);
            dateTimeProviderMock.Verify(x => x.UpdateTime(), Times.Once);
            imageManagerMock.Verify(x => x.SaveImagesPackAsync(It.IsAny<List<IFormFile>>()), Times.Never);
        }
        [Fact]
        public async Task PostBuildsCorrectlyFromUserPostWithImages()
        {
            // arrange
            Mock<IFormFile> image1 = new();
            Mock<IFormFile> image2 = new();
            Mock<IFormFile> image3 = new();

            dateTimeProviderMock.Setup(x => x.GetTime).Returns(DateTime.Parse("10:00"));
            imageManagerMock
                .Setup(x => x.SaveImagesPackAsync(It.IsAny<List<IFormFile>>()))
                .Returns(Task.FromResult(new string[3] {"img1.png", "img2.png", "img3.png" }));

            UserPostsHandler userPostsHandler = new UserPostsHandler(imageManagerMock.Object, dateTimeProviderMock.Object);

            UserPost userPost = new UserPost() { Text = "Hello", Images = new List<IFormFile>() {image1.Object, image2.Object, image3.Object } };
            int threadId = 1;
            // act
            var post = await userPostsHandler.BuildFromUserPostAsync(userPost, threadId);

            // assert

            Assert.Equal(threadId, post.ThreadId);
            Assert.Equal("Hello", post.Text);
            Assert.Equal(DateTime.Parse("10:00"), post.DateTime);
            Assert.Equal(new string[3] { "img1.png", "img2.png", "img3.png" }, post.ImagesSource);
            dateTimeProviderMock.Verify(x => x.UpdateTime(), Times.Once);
            imageManagerMock.Verify(x => x.SaveImagesPackAsync(It.IsAny<List<IFormFile>>()), Times.Once);
        }
    }
    
}
