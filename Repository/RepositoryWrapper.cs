using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
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

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public IArtistRepository Artist
        {
            get
            {
                if(_artist == null)
                {
                    _artist = new ArtistRepository(_repoContext);
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
                    _artistgenre = new ArtistGenreRepository(_repoContext);
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
                    _dateplanning = new DatePlanningRepository(_repoContext);
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
                    _evendate = new EventDateRepository(_repoContext);
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
                    _event = new EventRepository(_repoContext);
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
                    _eventGenre = new EventGenreRepository(_repoContext);
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
                    _genre = new GenreRepository(_repoContext);
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
                    _preference = new PreferenceRepository(_repoContext);
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
                    _review = new ReviewRepository(_repoContext);
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
                    _schedule = new ScheduleRepository(_repoContext);
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
                    _scheduleItem = new ScheduleItemRepository(_repoContext);
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
                    _stage = new StageRepository(_repoContext);
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
                    _user = new UserRepository(_repoContext);
                }

                return _user;
            }
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}