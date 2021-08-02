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
    [System.Web.Http.RoutePrefix("api/notifications")]
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class NotificationsController : ApiController
    {
        private readonly AppDbContext context;
        public NotificationsController()
        {
            context = new AppDbContext();
        }

        [HttpGet]
        [Route("NotificationsGet")]
        public async Task<List<NotificationDto>> NotificationsGet()
        {
            var items = await context.Notifications.ProjectTo<NotificationDto>().ToListAsync();
            return items;
        }

        [HttpGet]
        [Route("NotificationsGet")]
        public async Task<List<NotificationDto>> NotificationsGet(int levelId)
        {
            var items = await context.Notifications.Where(x => x.LevelId == levelId)
                .ProjectTo<NotificationDto>().ToListAsync();
            return items;
        }

        [HttpGet]
        [Route("NotificationsGetById")]
        public async Task<NotificationDto> NotificationsGetById(int id)
        {
            var items = await context.Notifications.ProjectTo<NotificationDto>().FirstOrDefaultAsync(x => x.Id == id);
            return items;
        }

        [HttpGet]
        [Route("NotificationsSearch")]
        public async Task<List<NotificationDto>> NotificationsSearch(string name)
        {
            if (name == null) name = "";
            var items = await context.Notifications
                         .Where(x => x.Name.Contains(name))
                         .ProjectTo<NotificationDto>()
                         .ToListAsync();
            return items;
        }


        [HttpPost]
        [Route("NotificationsCreate")]
        public async Task<HttpResponseMessage> NotificationCreate([FromBody] NotificationDto Notification)
        {
            if (ModelState.IsValid)
            {
                var item = Mapper.Map<NotificationDto, Notification>(Notification);
                context.Notifications.Add(item);
                await context.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }


        [HttpPost]
        [Route("NotificationsUpdate")]
        public async Task<HttpResponseMessage> NotificationUpdate([FromBody] NotificationDto Notification)
        {
            if (ModelState.IsValid)
            {
                var oldNotification = await context.Notifications.FindAsync(Notification.Id);

                oldNotification.Name = Notification.Name;
                oldNotification.LevelId = Notification.LevelId;
                oldNotification.ApperanceDate = Notification.ApperanceDate;
                oldNotification.EndDate = Notification.EndDate;

                await context.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }


        [HttpPost]
        [Route("NotificationsDelete")]
        public async Task<HttpResponseMessage> NotificationDelete(int id)
        {
            var item = await context.Notifications.FindAsync(id);
            context.Notifications.Remove(item);
            await context.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

