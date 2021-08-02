using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using TurkishHomeApi.Models.Business;
using TurkishHomeApi.Models.Dtos;

namespace TurkishHomeApi.Controllers
{
    [System.Web.Http.RoutePrefix("api/levels")]
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class LevelsController : ApiController
    {
        private readonly AppDbContext context;
        public LevelsController()
        {
            context = new AppDbContext();
        }

        [HttpGet]
        [Route("LevelsGet")]
        public async Task<List<LevelDto>> LevelsGet()
        {
            var items = await context.Levels
                .ProjectTo<LevelDto>()
                .ToListAsync();
         
           return items;
        }


        [HttpGet]
        [Route("LevelsGetDropDownList")]
        public async Task<List<LevelDropDownList>> LevelsGetDropDownList()
        {
            var items = await context.Levels.ProjectTo<LevelDropDownList>().ToListAsync();
            return items;
        }


        [HttpGet]
        [Route("LevelsGetById")]
        public async Task<LevelDto> LevelsGetById(int id)
        {
            var items = await context.Levels.ProjectTo<LevelDto>().FirstOrDefaultAsync(x => x.Id == id);
            return items;
        }

        [HttpGet]
        [Route("LevelsSearch")]
        public async Task<List<LevelDto>> LevelsSearch(string name)
        {
            if (name == null) name = "";

            var items = await context.Levels
                         .Where(x => x.Name.Contains(name))
                         .ProjectTo<LevelDto>()
                         .ToListAsync();
            return items;
        }


        [HttpPost]
        [Route("LevelsCreate")]
        public async Task<HttpResponseMessage> LevelCreate([FromBody] LevelDto level)
        {
            if (ModelState.IsValid)
            {
                var item = Mapper.Map<LevelDto, Level>(level);
                context.Levels.Add(item);
                await context.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }


        [HttpPost]
        [Route("LevelsUpdate")]
        public async Task<HttpResponseMessage> LevelUpdate([FromBody] LevelDto Level)
        {
            if (ModelState.IsValid)
            {
                var oldLevel = await context.Levels.FindAsync(Level.Id);
                oldLevel.Name = Level.Name;
                await context.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }


        [HttpPost]
        [Route("LevelsDelete")]
        public async Task<HttpResponseMessage> LevelDelete(int id)
        {
            var item = await context.Levels.FindAsync(id);
            context.Levels.Remove(item);
            await context.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
