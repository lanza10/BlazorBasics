using System.Runtime.CompilerServices;
using AutoMapper;
using BlogApi.Models;
using BlogApi.Models.Dtos.Posts;
using BlogApi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController(IPostRepository postRepo, IMapper mapper) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPosts()
        {
            var postsList = postRepo.GetPosts();
            var postsDtoList = new List<PostDto>();

            foreach (var post in postsList)
            {
                postsDtoList.Add(mapper.Map<PostDto>(post));
            }

            return Ok(postsDtoList);
        }
        [AllowAnonymous]
        [HttpGet("{id:int}",Name = "GetPost")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPost(int id)
        {
            var post = postRepo.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            var postDto = mapper.Map<PostDto>(post);
            return Ok(postDto);
        }

        //[Authorize]
        [HttpPost]
        [ProducesResponseType(201,Type = typeof(PostCreateDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreatePost([FromBody] PostCreateDto postCreateDto)
        {
            // Validate that the required data is included and its constraints
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (postCreateDto == null)
            {
                return BadRequest(ModelState);
            }

            if (postRepo.PostExists(postCreateDto.Title))
            {
                ModelState.AddModelError("","A post with that title already exists");
                return StatusCode(404, ModelState);
            }

            var post = mapper.Map<Post>(postCreateDto);
            if (!postRepo.CreatePost(post))
            {
                ModelState.AddModelError("", $"Something went wrong while saving the post {post.Title}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPost", new { id = post.Id }, post);
        }

        //[Authorize]
        [HttpPatch("{postId:int}", Name = "UpdatePatchPost")]
        [ProducesResponseType(201, Type = typeof(PostCreateDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePatchPost(int postId, [FromBody] PostUpdateDto postUpdateDto)
        {
            // Validate that the required data is included and its constraints
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (postUpdateDto == null || postId != postUpdateDto.Id)
            {
                return BadRequest(ModelState);
            }

            var post = mapper.Map<Post>(postUpdateDto);
            if (!postRepo.UpdatePost(post))
            {
                ModelState.AddModelError("", $"Something went wrong while updating the post {post.Title}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        //[Authorize]
        [HttpDelete("{id:int}", Name = "DeletePost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePost(int id)
        {
            if (!postRepo.PostExists(id))
            {
                return NotFound();
            }

            var post = postRepo.GetPost(id);

            if (!postRepo.DeletePost(post))
            {
                ModelState.AddModelError("",$"Something went wrong deleting the post{post.Title}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
