using AutoMapper;
using GameTimeMonitor.Application.DTOs;
using GameTimeMonitor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeMonitor.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User Mappings
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Device Mappings
            CreateMap<Device, DeviceDto>();
            CreateMap<CreateDeviceDto, Device>();
            CreateMap<UpdateDeviceDto, Device>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Activity Mappings
            CreateMap<Activity, ActivityDto>();
            CreateMap<CreateActivityDto, Activity>();
            CreateMap<UpdateActivityDto, Activity>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // RemoteControlCommand Mappings
            CreateMap<RemoteControlCommand, RemoteControlCommandDto>();
            CreateMap<CreateRemoteControlCommandDto, RemoteControlCommand>();
        }
    }
}
