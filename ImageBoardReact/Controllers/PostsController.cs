using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageBoardReact.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using ImageBoardReact.Infrastructure;
using ImageBoardReact.Models.Images;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImageBoardReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepository repository;
        private readonly ILogger _logger;
        private readonly IHostEnvironment _hostingEnvironment;
        private readonly IUserPostsHandler _userPostsHandler;
        private readonly IImageManager imageManager;
        private readonly IRepositoryMonitor _repositoryMonitor;
        private readonly IRepositoryManager _repositoryManager;
        //private DbContext context;
        public PostsController(
            IPostsRepository repo, 
            ILogger<PostsController> logger, 
            IHostEnvironment environment,
            IRepositoryMonitor repositoryMonitor,
            IRepositoryManager repositoryManager,
            IImageManager imageManager,
            IUserPostsHandler userPostsHandler)
        {
            repository = repo;
            _logger = logger;
            _hostingEnvironment = environment;
            _userPostsHandler = userPostsHandler;
            _repositoryMonitor = repositoryMonitor;
            _repositoryManager = repositoryManager;
            this.imageManager = imageManager;
            //imageManager = new LocalImageManager(_hostingEnvironment, _logger, "StaticFiles");

        }
        // GET: api/<PostsController>
        // GET threads
        [HttpGet]
        async public Task<IActionResult> Get()
        {
            //var posts = await repository.Posts
            //    .Where(post => post.Id == post.ThreadId)
            //    .OrderByDescending(post => post.LastPostTime)
            //    .ToListAsync();

            _logger.LogInformation($"Get all threads called");

            var posts = await repository.GetThreadsInOrderAsync();
            if (posts.Count() == 0)
            {
                return NotFound();
            }
            return Ok(posts);
        }

        // GET api/<PostsController>/5
        // GET posts from thread id
        //[EnableCors("Access-Control-Allow-Headers")] 
        [HttpGet("{id}")]
        async public Task<IActionResult> Get(int id)
        {
            //var posts = await repository.Posts
            //    .Where(x => x.ThreadId == id)
            //    .OrderBy(post => post.DateTime)
            //    .ToListAsync();
            _logger.LogInformation($"Get thread {id} called");
            var posts = await repository.GetPostsInOrderAsync(id);
            if (posts.Count == 0)
            {
                return NotFound();
            }
            return Ok(posts);
        }

        [HttpGet("{id}/{number}")]
        async public Task<IActionResult> GetLastPosts(int id, int number)
        {
            var posts = await repository.Posts
                .Where(x => x.ThreadId == id)
                .OrderBy(post => post.DateTime)
                .ToListAsync();

            if (posts.Count == 0)
            {
                return NotFound();
            }
            return Ok(posts.TakeLast(number));
        }

        // POST api/<PostsController>
        //Create new thread
        [HttpPost]
        async public Task<IActionResult> Post([FromForm] UserPost userPost)
        {
            Post newPost = await _userPostsHandler.BuildFromUserPostAsync(userPost);
            await repository.SaveThreadAsync(newPost);
            _repositoryMonitor.Update(repository);
            _repositoryMonitor.LogState();

            await _repositoryManager.RemoveOldestThreads().ConfigureAwait(false);
            _logger.LogInformation("Returning from controller");
            return Ok();
        }
        [HttpPost("{id}")]
        async public Task<IActionResult> PostInThread(int id, [FromForm] UserPost userPost)
        {
            _logger.LogWarning(userPost.Text);
       
            if (ContentChecker.CheckFieldsOk(userPost))
            {
                Post newPost = await _userPostsHandler.BuildFromUserPostAsync(userPost, id);
                await repository.SavePostAsync(newPost);
                _repositoryMonitor.Update(repository);
                _repositoryMonitor.LogState();
                return Ok();
            }
            return UnprocessableEntity();
        }
        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        // DELETE api/<PostsController>/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        //[HttpDelete]
        async public Task<IActionResult> DeletePost(int id)
        {

            bool result  = await _repositoryManager.RemoveByIdAsync(id);// await repository.DeleteAsync(id);
            
            if (result == true)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
