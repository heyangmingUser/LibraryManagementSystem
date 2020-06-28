using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Library.API.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        public IAuthorRepository AuthorRepository { get; }
        private ILogger logger { get; set; }

        public AuthorController(IAuthorRepository authorRepository,ILoggerFactory loggerFactory)
        {
            AuthorRepository = authorRepository;
            logger = loggerFactory.CreateLogger<AuthorController>();
        }

        [HttpGet]
        public ActionResult<List<AuthorDto>> GetAuthors()
        {
            logger.LogError("测试成功");
            return AuthorRepository.GetAuthors().ToList();
        }

        [HttpGet("{authorId}")]
        public ActionResult<AuthorDto> GetAuthor(Guid authorId)
        {
            var author = AuthorRepository.GetAuthor(authorId);

            if (author == null)
            {
                return NotFound();
            }
            else
            {
                return author;
            }
        }
    }
}
