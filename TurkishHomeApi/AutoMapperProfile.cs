using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TurkishHomeApi.Models.Business;
using TurkishHomeApi.Models.Dtos;

namespace TurkishHomeApi
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public static void Run() => AutoMapper.Mapper.Initialize(a => { a.AddProfile<AutoMapperProfile>(); });


        public AutoMapperProfile()
        {
            AllowNullDestinationValues = true;

            CreateMap<Level, LevelDto>();
            CreateMap<LevelDto, Level>();

            CreateMap<Level, LevelDropDownList>();
            CreateMap<LevelDropDownList, Level>();

            CreateMap<Material, MaterialDropDownList>();
            CreateMap<MaterialDropDownList, Material>();

            CreateMap<Exam, ExamDropDownList>();
            CreateMap<ExamDropDownList, Exam>();

            CreateMap<Unit, UnitDto>()
              .ForMember(dto => dto.LevelName, opt => opt.MapFrom(obj => obj.Material.Level.Name))
              .ForMember(dto => dto.MaterialName, opt => opt.MapFrom(obj => obj.Material.Name));
            CreateMap<UnitDto, Unit>();

            CreateMap<Material, MaterialDto>()
              .ForMember(dto => dto.LevelName, opt => opt.MapFrom(obj => obj.Level.Name));
            CreateMap<MaterialDto, Material>();


            CreateMap<Exam, ExamDto>()
              .ForMember(dto => dto.MaterialName, opt => opt.MapFrom(obj => obj.Unit.Material.Name))
              .ForMember(dto => dto.UnitName, opt => opt.MapFrom(obj => obj.Unit.Name));
            CreateMap<ExamDto, Exam>();


            CreateMap<Notification, NotificationDto>()
            .ForMember(dto => dto.LevelName, opt => opt.MapFrom(obj => obj.Level.Name));
            CreateMap<NotificationDto, Notification>();

            CreateMap<Setting, SettingDto>();
            CreateMap<SettingDto, Setting>();
        }
    }
}