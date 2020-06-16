using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspEventVieuwerAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Artist, ArtistDto>();
            CreateMap<ArtistDto, Artist>();
            CreateMap<ArtistForCreationDto, Artist>();
            CreateMap<ArtistForUpdateDto, Artist>();

            CreateMap<ArtistGenre, ArtistGenreDto>();
            CreateMap<ArtistGenreDto, ArtistGenre>();
            CreateMap<ArtistGenreForCreationDto, ArtistGenre>();

            CreateMap<DatePlanning, DatePlanningDto>();
            CreateMap<DatePlanningDto, DatePlanning>();
            CreateMap<DatePlanningForCreationDto, DatePlanning>();
            CreateMap<DatePlanningForUpdateDto, DatePlanning>();

            CreateMap<Event, EventDto>();
            CreateMap<EventDto, Event>();
            CreateMap<EventForCreationDto, Event>();
            CreateMap<EventForUpdateDto, Event>();

            CreateMap<EventGenre, EventGenreDto>();
            CreateMap<EventGenreDto, EventGenre>();
            CreateMap<EventGenreDto, EventGenre>();
            CreateMap<EventGenreForCreationDto, EventGenre>();

            CreateMap<EventDate, EventDateDto>();
            CreateMap<EventDateForCreationDto, EventDate>();
            CreateMap<EventDateForUpdateDto, EventDate>();

            CreateMap<Genre, GenreDto>();

            CreateMap<Preference, PreferenceDto>();
            CreateMap<PreferenceDto, Preference>();
            CreateMap<PreferenceForCreationDto, Preference>();

            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<ReviewForCreationDto, Review>();
            CreateMap<ReviewForUpdateDto, Review>();

            CreateMap<Schedule, ScheduleDto>();
            CreateMap<ScheduleDto, Schedule>();
            CreateMap<ScheduleForCreationDto, Schedule>();
            CreateMap<ScheduleForUpdateDto, Schedule>();

            CreateMap<ScheduleItem, ScheduleItemDto>();
            CreateMap<ScheduleItemDto, ScheduleItem>();
            CreateMap<ScheduleItemForCreationDto, ScheduleItem>();
            CreateMap<ScheduleItemForUpdateDto, ScheduleItem>();

            CreateMap<Stage, StageDto>();
            CreateMap<StageDto, Stage>();
            CreateMap<StageForCreationDto, Stage>();
            CreateMap<StageForUpdateDto, Stage>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserForCreationDto, User>();
            CreateMap<UserForUpdateDto, User>();

            CreateMap<UserRight, UserRightDto>();
            CreateMap<UserRightDto, UserRight>();
        }
    }
}
