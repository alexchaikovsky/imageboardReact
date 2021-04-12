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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImageBoardReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IPostsRepository repository;
        private readonly ILogger _logger;
        private int newPostId;
        private readonly IHostEnvironment _hostingEnvironment;
        private readonly IUserPostsHandler _userPostsHandler;
        private readonly IImageManager imageManager;
        //private DbContext context;
        public PostsController(IPostsRepository repo, ILogger<PostsController> logger, IHostEnvironment environment, IUserPostsHandler userPostsHandler)
        {
            repository = repo;
            _logger = logger;
            newPostId = repository.Posts.OrderBy(post => post.Id).Last().Id;
            _hostingEnvironment = environment;
            _userPostsHandler = userPostsHandler;
            imageManager = new LocalImageManager(_hostingEnvironment, "StaticFiles");

        }
        // GET: api/<PostsController>
        // GET threads
        [HttpGet]
        async public Task<IActionResult> Get()
        {

            var posts = await repository.Posts.Where(x => x.Id == x.ThreadId).ToListAsync();
            if (posts == null)
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
            var posts = await repository.Posts.Where(x => x.ThreadId == id).ToListAsync();
            if (posts.Count == 0)
            {
                return NotFound();
            }
            return Ok(posts);
        }

        // POST api/<PostsController>
        //Create new thread
        [HttpPost]
        async public Task<IActionResult> Post([FromForm] UserPost userPost)
        {
            newPostId++;
            Post newPost = await _userPostsHandler.BuildFromUserPostAsync(userPost, imageManager, newPostId, newPostId);
            await repository.SavePost(newPost);
            return Ok();
        }
        [HttpPost("{id}")]
        async public Task<IActionResult> PostInThread(int id, [FromForm] UserPost userPost)
        {
            _logger.LogWarning(userPost.Text);
            //Console.ReadKey();
            if (ContentChecker.CheckFieldsOk(userPost))
            {
                newPostId++;
                _logger.LogInformation($"New ID = {newPostId}");
                Post newPost = await _userPostsHandler.BuildFromUserPostAsync(userPost, imageManager, newPostId, id);
                await repository.SavePost(newPost);
                return Ok();
            }
            return UnprocessableEntity();
            //context.Posts.AddAsync(new Post {Text = value, DateTime = DateTime.Now });
        }
        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
