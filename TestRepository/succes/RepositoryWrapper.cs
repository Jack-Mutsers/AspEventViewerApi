using Contracts;
using Contracts.Repository;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestRepository.succes
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext Context {get;}
        private IArtistRepository _artist;
        private IArtistGenreRepository _artistgenre;
        private IDatePlanningRepository _dateplanning;
        private IEventDateRepository _evendate;
        private IEventRepository _event;
        private IEventGenreRepository _eventGenre;
        private IGenreRepository _genre;
        private IPreferenceRepository _preference;
        private IReviewRepository _review;
        private IScheduleRepository _schedule;
        private IScheduleItemRepository _scheduleItem;
        private IStageRepository _stage;
        private IUserRepository _user;

        public IArtistRepository Artist
        {
            get
            {
                if(_artist == null)
                {
                    _artist = new ArtistRepository();
                }

                return _artist;
            }
        }

        public IArtistGenreRepository ArtistGenre
        {
            get
            {
                if(_artistgenre == null)
                {
                    _artistgenre = new ArtistGenreRepository();
                }

                return _artistgenre;
            }
        }

        public IDatePlanningRepository DatePlanning
        {
            get
            {
                if (_dateplanning == null)
                {
                    _dateplanning = new DatePlanningRepository();
                }

                return _dateplanning;
            }
        }

        public IEventDateRepository EventDate
        {
            get
            {
                if (_evendate == null)
                {
                    _evendate = new EventDateRepository();
                }

                return _evendate;
            }
        }

        public IEventRepository Event
        {
            get
            {
                if (_event == null)
                {
                    _event = new EventRepository();
                }

                return _event;
            }
        }

        public IEventGenreRepository EventGenre
        {
            get
            {
                if (_eventGenre == null)
                {
                    _eventGenre = new EventGenreRepository();
                }

                return _eventGenre;
            }
        }

        public IGenreRepository Genre
        {
            get
            {
                if (_genre == null)
                {
                    _genre = new GenreRepository();
                }

                return _genre;
            }
        }

        public IPreferenceRepository Preference
        {
            get
            {
                if (_preference == null)
                {
                    _preference = new PreferenceRepository();
                }

                return _preference;
            }
        }

        public IReviewRepository Review
        {
            get
            {
                if (_review == null)
                {
                    _review = new ReviewRepository();
                }

                return _review;
            }
        }

        public IScheduleRepository Schedule
        {
            get
            {
                if (_schedule == null)
                {
                    _schedule = new ScheduleRepository();
                }

                return _schedule;
            }
        }
        
        public IScheduleItemRepository ScheduleItem
        {
            get
            {
                if (_scheduleItem == null)
                {
                    _scheduleItem = new ScheduleItemRepository();
                }

                return _scheduleItem;
            }
        }

        public IStageRepository Stage
        {
            get
            {
                if (_stage == null)
                {
                    _stage = new StageRepository();
                }

                return _stage;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository();
                }

                return _user;
            }
        }

        public void Save(){}
    }
}