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
using TurkishHomeApi.Extensions;
using TurkishHomeApi.Models.Business;
using TurkishHomeApi.Models.Dtos;
using TurkishHomeApi.Models.Enums;

namespace TurkishHomeApi.Controllers
{
    [System.Web.Http.RoutePrefix("api/units")]
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class UnitController : ApiController
    {
        private readonly AppDbContext context;
        public UnitController()
        {
            context = new AppDbContext();
        }

        [HttpGet]
        [Route("UnitsGet")]
        public async Task<List<UnitDto>> UnitsGet()
        {
            var items = await context.Units.ProjectTo<UnitDto>().ToListAsync();
            return items;
        }

        [HttpGet]
        [Route("UnitsGet")]
        public async Task<List<UnitDto>> UnitsGet(int materialId)
        {
            var items = await context.Units
                .Where(x => x.MaterialId == materialId)
                .ProjectTo<UnitDto>().ToListAsync();
            return items;
        }


        [HttpGet]
        [Route("UnitsGetDropDownList")]
        public async Task<List<UnitDropDownList>> UnitsGetDropDownList()
        {
            var items = await context.Units.ProjectTo<UnitDropDownList>().ToListAsync();
            return items;
        }

        [HttpGet]
        [Route("UnitsGetDropDownList")]
        public async Task<List<UnitDropDownList>> UnitsGetDropDownList(int materialId)
        {
            var items = await context.Units
                .Where(x => x.MaterialId == materialId)
                .ProjectTo<UnitDropDownList>().ToListAsync();
            return items;
        }


        [HttpGet]
        [Route("UnitsGetById")]
        public async Task<UnitDto> UnitsGetById(int id)
        {
            var items = await context.Units.ProjectTo<UnitDto>().FirstOrDefaultAsync(x => x.Id == id);
            return items;
        }

        [HttpGet]
        [Route("UnitsSearch")]
        public async Task<List<UnitDto>> UnitsSearch(string name)
        {
            if (name == null) name = "";
            var items = await context.Units
                         .Where(x => x.Name.Contains(name))
                         .ProjectTo<UnitDto>()
                         .ToListAsync();
            return items;
        }


        [HttpPost]
        [Route("UnitsCreate")]
        public async Task<HttpResponseMessage> UnitCreate([FromBody] UnitDto Unit)
        {
            if (ModelState.IsValid)
            {
                var item = Mapper.Map<UnitDto, Unit>(Unit);
                context.Units.Add(item);
                await context.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }


        [HttpPost]
        [Route("UnitsUpdate")]
        public async Task<HttpResponseMessage> UnitUpdate([FromBody] UnitDto Unit)
        {
            if (ModelState.IsValid)
            {
                var oldUnit = await context.Units.FindAsync(Unit.Id);

                oldUnit.Name = Unit.Name;
                oldUnit.MaterialId = Unit.MaterialId;

                await context.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }


        [HttpPost]
        [Route("UnitsDelete")]
        public async Task<HttpResponseMessage> UnitDelete(int id)
        {
            var item = await context.Units.FindAsync(id);
            context.Units.Remove(item);
            await context.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

