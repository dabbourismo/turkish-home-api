using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using TurkishHomeApi.Extensions;
using TurkishHomeApi.Helpers;
using TurkishHomeApi.Models.Business;
using TurkishHomeApi.Models.Dtos;
using TurkishHomeApi.Models.Enums;

namespace TurkishHomeApi.Controllers
{
    [System.Web.Http.RoutePrefix("api/settings")]
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class SettingsController : ApiController
    {
        private readonly AppDbContext context;
        public SettingsController()
        {
            context = new AppDbContext();
        }


        [HttpGet]
        [Route("SettingsGet")]
        public async Task<SettingDto> SettingsGet()
        {
            var items = await context.Settings
                .Where(x => x.Id == 1)
                .ProjectTo<SettingDto>().FirstOrDefaultAsync();
            return items;
        }

        [HttpPost]
        [Route("uploadPhoto")]
        public async Task<HttpResponseMessage> UploadPhoto(int requestId)
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ?
                         HttpContext.Current.Request.Files[0] : null;

            if (file != null)
            {
                FileUploader.UploadFTPFile(file.FileName
                           , 1
                           , file
                           , HttpContext.Current.Server.MapPath("~/SettingsPhotos"));
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }



        [HttpPost]
        [Route("SettingsUpdate")]
        public async Task<HttpResponseMessage> SettingUpdate([FromBody] SettingDto Setting)
        {
            if (ModelState.IsValid)
            {
                var oldSetting = await context.Settings.FindAsync(1);

                oldSetting.Name = Setting.Name;

                await context.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
    }
}
