using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logics
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Artist, ArtistDto>();
            CreateMap<ArtistForCreationDto, Artist>();
            CreateMap<ArtistForUpdateDto, Artist>();
            
            CreateMap<DatePlanning, DatePlanningDto>();
            CreateMap<DatePlanningForCreationDto, DatePlanning>();
            CreateMap<DatePlanningForUpdateDto, DatePlanning>();
            
            CreateMap<Event, EventDto>();
            CreateMap<EventForCreationDto, Event>();
            CreateMap<EventForUpdateDto, Event>();
            
            CreateMap<EventGenre, EventGenreDto>();
            
            CreateMap<EventDate, EventDateDto>();
            CreateMap<EventDateForCreationDto, EventDate>();
            CreateMap<EventDateForUpdateDto, EventDate>();
            
            CreateMap<Genre, GenreDto>();
            
            CreateMap<Preference, PreferenceDto>();
            CreateMap<PreferenceForCreationDto, Preference>();
            CreateMap<PreferenceForUpdateDto, Preference>();
            
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewForCreationDto, Review>();
            CreateMap<ReviewForUpdateDto, Review>();
            
            CreateMap<Schedule, ScheduleDto>();
            CreateMap<ScheduleForCreationDto, Schedule>();
            CreateMap<ScheduleForUpdateDto, Schedule>();

            CreateMap<ScheduleItem, ScheduleItemDto>();
            CreateMap<ScheduleItemForCreationDto, ScheduleItem>();
            CreateMap<ScheduleItemForUpdateDto, ScheduleItem>();
            
            CreateMap<Stage, StageDto>();
            CreateMap<StageForCreationDto, Stage>();
            CreateMap<StageForUpdateDto, Stage>();
            
            CreateMap<User, UserDto>();
            CreateMap<UserForCreationDto, User>();
            CreateMap<UserForUpdateDto, User>();
            
        }
    }
}
