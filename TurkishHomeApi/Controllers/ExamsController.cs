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
    [System.Web.Http.RoutePrefix("api/exams")]
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class ExamsController : ApiController
    {
        private readonly AppDbContext context;
        public ExamsController()
        {
            context = new AppDbContext();
        }

        [HttpGet]
        [Route("ExamsGet")]
        public async Task<List<ExamDto>> ExamsGet()
        {
            var items = await context.Exams.ProjectTo<ExamDto>().ToListAsync();
            return items;
        }

        [HttpGet]
        [Route("ExamsGet")]
        public async Task<List<ExamDto>> ExamsGet(int unitId)
        {
            var items = await context.Exams
                .Where(x => x.UnitId == unitId)
                .ProjectTo<ExamDto>().ToListAsync();
            return items;
        }

        [HttpGet]
        [Route("ExamsGet")]
        public async Task<List<ExamDto>> ExamsGet(int unitId, int examType)
        {
            var items = await context.Exams
                .Where(x => x.UnitId == unitId && x.ExamType == (EnumExamType)examType)
                .ProjectTo<ExamDto>().ToListAsync();
            return items;
        }


        [HttpGet]
        [Route("ExamsGet")]
        public async Task<List<ExamDto>> ExamsGet(int unitId, int examType, DateTime date)
        {
            var items = await context.Exams
                .Where(x => x.UnitId == unitId
                && x.ExamType == (EnumExamType)examType
                && x.ApperanceDate >= date
                && x.EndDate <= date
                )
                .ProjectTo<ExamDto>().ToListAsync();
            return items;
        }

        [HttpGet]
        [Route("ExamsGetTemp")]
        public async Task<List<ExamDto>> ExamsGetTemp()
        {
            var items = await context.Exams
                .Where(x => x.ExamType == EnumExamType.Temporary)
                .ProjectTo<ExamDto>().ToListAsync();
            return items;
        }

        [HttpGet]
        [Route("ExamsGetPerm")]
        public async Task<List<ExamDto>> ExamsGetPerm(int unitId)
        {
            var items = await context.Exams
                .Where(x => x.ExamType == EnumExamType.Permenant && x.UnitId == unitId)
                .ProjectTo<ExamDto>().ToListAsync();
            return items;
        }


        [HttpGet]
        [Route("ExamsGetDropDownList")]
        public async Task<List<ExamDropDownList>> ExamsGetDropDownList()
        {
            var items = await context.Exams.ProjectTo<ExamDropDownList>().ToListAsync();
            return items;
        }


        [HttpGet]
        [Route("ExamsGetById")]
        public async Task<ExamDto> ExamsGetById(int id)
        {
            var items = await context.Exams.ProjectTo<ExamDto>().FirstOrDefaultAsync(x => x.Id == id);
            return items;
        }

        [HttpGet]
        [Route("ExamsSearch")]
        public async Task<List<ExamDto>> ExamsSearch(string name)
        {
            if (name == null) name = "";
            var items = await context.Exams
                         .Where(x => x.Name.Contains(name))
                         .ProjectTo<ExamDto>()
                         .ToListAsync();
            return items;
        }


        [HttpPost]
        [Route("ExamsCreate")]
        public async Task<HttpResponseMessage> ExamCreate([FromBody] ExamDto Exam)
        {
            if (ModelState.IsValid)
            {
                var item = Mapper.Map<ExamDto, Exam>(Exam);
                context.Exams.Add(item);
                await context.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }


        [HttpPost]
        [Route("ExamsUpdate")]
        public async Task<HttpResponseMessage> ExamUpdate([FromBody] ExamDto Exam)
        {
            if (ModelState.IsValid)
            {
                var oldExam = await context.Exams.FindAsync(Exam.Id);

                oldExam.Name = Exam.Name;
                oldExam.UnitId = Exam.UnitId;
                oldExam.ExamType = Exam.ExamType;
                oldExam.ApperanceDate = Exam.ApperanceDate;
                oldExam.EndDate = Exam.EndDate;
                oldExam.Url = Exam.Url;

                await context.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }


        [HttpPost]
        [Route("ExamsDelete")]
        public async Task<HttpResponseMessage> ExamDelete(int id)
        {
            var item = await context.Exams.FindAsync(id);
            context.Exams.Remove(item);
            await context.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
