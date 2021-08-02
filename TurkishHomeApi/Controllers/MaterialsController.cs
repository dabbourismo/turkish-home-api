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
    [System.Web.Http.RoutePrefix("api/materials")]
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class MaterialsController : ApiController
    {
        private readonly AppDbContext context;
        public MaterialsController()
        {
            context = new AppDbContext();
        }

        [HttpGet]
        [Route("MaterialsGet")]
        public async Task<List<MaterialDto>> MaterialsGet()
        {
            var items = await context.Materials.ProjectTo<MaterialDto>().ToListAsync();
            return items;
        }

        [HttpGet]
        [Route("MaterialsGet")]
        public async Task<List<MaterialDto>> MaterialsGet(int levelId)
        {
            var items = await context.Materials.Where(x=>x.LevelId == levelId)
                .ProjectTo<MaterialDto>().ToListAsync();
            return items;
        }


        [HttpGet]
        [Route("MaterialsGetDropDownList")]
        public async Task<List<MaterialDropDownList>> MaterialsGetDropDownList()
        {
            var items = await context.Materials.ProjectTo<MaterialDropDownList>().ToListAsync();
            return items;
        }


        [HttpGet]
        [Route("MaterialsGetDropDownListByLevelId")]
        public async Task<List<MaterialDropDownList>> MaterialsGetDropDownList(int levelId)
        {
            var items = await context.Materials.Where(x => x.LevelId == levelId)
                .ProjectTo<MaterialDropDownList>().ToListAsync();
            return items;
        }


        [HttpGet]
        [Route("MaterialsGetById")]
        public async Task<MaterialDto> MaterialsGetById(int id)
        {
            var items = await context.Materials.ProjectTo<MaterialDto>().FirstOrDefaultAsync(x => x.Id == id);
            return items;
        }

        [HttpGet]
        [Route("MaterialsSearch")]
        public async Task<List<MaterialDto>> MaterialsSearch(string name)
        {
            if (name == null) name = "";
            var items = await context.Materials
                         .Where(x => x.Name.Contains(name))
                         .ProjectTo<MaterialDto>()
                         .ToListAsync();
            return items;
        }


        [HttpPost]
        [Route("MaterialsCreate")]
        public async Task<HttpResponseMessage> MaterialCreate([FromBody] MaterialDto material)
        {
            if (ModelState.IsValid)
            {
                var item = Mapper.Map<MaterialDto, Material>(material);
                context.Materials.Add(item);
                await context.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }


        [HttpPost]
        [Route("MaterialsUpdate")]
        public async Task<HttpResponseMessage> MaterialUpdate([FromBody] MaterialDto Material)
        {
            if (ModelState.IsValid)
            {
                var oldMaterial = await context.Materials.FindAsync(Material.Id);

                oldMaterial.Name = Material.Name;
                oldMaterial.LevelId = Material.LevelId;

                await context.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }


        [HttpPost]
        [Route("MaterialsDelete")]
        public async Task<HttpResponseMessage> MaterialDelete(int id)
        {
            var item = await context.Materials.FindAsync(id);
            context.Materials.Remove(item);
            await context.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
