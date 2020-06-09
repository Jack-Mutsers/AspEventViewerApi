using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestRepository
{
    public class RepositoryCollection
    {
        public List<ArtistGenre> artistGenres { get; } = new List<ArtistGenre>()
        {
            new ArtistGenre(){ artist_id = 1, genre_id = 1 },
            new ArtistGenre(){ artist_id = 1, genre_id = 2 },
            new ArtistGenre(){ artist_id = 1, genre_id = 3 },
            new ArtistGenre(){ artist_id = 2, genre_id = 2 },
            new ArtistGenre(){ artist_id = 2, genre_id = 3 },
            new ArtistGenre(){ artist_id = 3, genre_id = 1 },
        };

        public List<Genre> genres { get; } = new List<Genre>()
        {
            new Genre() { id = 1, name = "Rock"},
            new Genre() { id = 2, name = "pop"},
            new Genre() { id = 3, name = "metal"},
            new Genre() { id = 4, name = "country"},
            new Genre() { id = 5, name = "jazz"},
            new Genre() { id = 6, name = "clasic"},
            new Genre() { id = 7, name = "electronic"},
            new Genre() { id = 8, name = "regea"},
            new Genre() { id = 9, name = "hardstyle"},
            new Genre() { id = 10, name = "folk"},
            new Genre() { id = 11, name = "disco"},
            new Genre() { id = 12, name = "hardcore"},
            new Genre() { id = 13, name = "hiphop"},
            new Genre() { id = 14, name = "trance"},
            new Genre() { id = 15, name = "drum and base"},
            new Genre() { id = 16, name = "nightcore"},
            new Genre() { id = 17, name = "house"},
            new Genre() { id = 18, name = "R&B"},
        };

        public List<Artist> artists { get; } = new List<Artist>()
        {
                new Artist(){id = 1, name = "artist A"},
                new Artist(){id = 2, name = "artist B"},
                new Artist(){id = 3, name = "artist C"},
                new Artist(){id = 4, name = "artist D"}
        };

        public List<DatePlanning> datePlannings { get; } = new List<DatePlanning>()
        {
            new DatePlanning(){id = 1, Eventid = 1, start = new DateTime(2019, 4, 5, 12, 30, 0), end = new DateTime(2019, 4, 7, 3, 0, 0) },
            new DatePlanning(){id = 2, Eventid = 1, start = new DateTime(2020, 4, 3, 12, 30, 0), end = new DateTime(2020, 4, 7, 3, 0, 0) },
            new DatePlanning(){id = 3, Eventid = 1, start = new DateTime(2021, 4, 2, 12, 30, 0), end = new DateTime(2021, 4, 7, 3, 0, 0) },
            new DatePlanning(){id = 4, Eventid = 1, start = new DateTime(2020, 6, 11, 12, 30, 0), end = new DateTime(2020, 6, 14, 3, 0, 0) },
            new DatePlanning(){id = 5, Eventid = 2, start = new DateTime(2020, 6, 18, 12, 30, 0), end = new DateTime(2020, 6, 21, 3, 0, 0) },
            new DatePlanning(){id = 6, Eventid = 3, start = new DateTime(2020, 6, 11, 12, 30, 0), end = new DateTime(2020, 6, 14, 3, 0, 0) },
            new DatePlanning(){id = 7, Eventid = 3, start = new DateTime(2020, 6, 7, 12, 30, 0), end = new DateTime(2020, 6, 9, 3, 0, 0) },
        };

        public List<Event> events { get; } = new List<Event>()
        {
            new Event(){ id = 1, active = true, name = "PinkPop", description = "this is a description", poster = "{\"url\":\"images/Girlboss.jpg\",\"name\":\"Girlboss.jpg\",\"image\":true}" },
            new Event(){ id = 2, active = true, name = "event 1", description = "this is a description", poster = "{\"url\":\"images/Retroevent.jpg\",\"name\":\"Retroevent.jpg\",\"image\":true}" },
            new Event(){ id = 3, active = true, name = "event 2", description = "this is a description", poster = "{\"url\":\"images/purelight.jpg\",\"name\":\"purelight.jpg\",\"image\":true}" },
            new Event(){ id = 4, active = true, name = "event 3", description = "this is a description", poster = "{\"url\":\"images/beachparty.jpg\",\"name\":\"beachparty.jpg\",\"image\":true}" },
            new Event(){ id = 5, active = true, name = "event 4", description = "this is a description", poster = "{\"url\":\"images/commedycollege.jpg\",\"name\":\"commedycollege.jpg\",\"image\":true}" },
            new Event(){ id = 6, active = true, name = "event 5", description = "this is a description", poster = "{\"url\":\"images/partyboat.jpg\",\"name\":\"partyboat.jpg\",\"image\":true}" },
        };

        public List<EventGenre> eventGenres { get; } = new List<EventGenre>()
        {
            new EventGenre(){event_id = 1, genre_id = 1},
            new EventGenre(){event_id = 1, genre_id = 2},
            new EventGenre(){event_id = 2, genre_id = 2},
            new EventGenre(){event_id = 3, genre_id = 3},
            new EventGenre(){event_id = 4, genre_id = 4},
            new EventGenre(){event_id = 5, genre_id = 5},
            new EventGenre(){event_id = 6, genre_id = 1},
            new EventGenre(){event_id = 6, genre_id = 3},
            new EventGenre(){event_id = 6, genre_id = 5},
            new EventGenre(){event_id = 6, genre_id = 7},
        };

        public List<EventDate> eventDates { get; } = new List<EventDate>()
        {
            new EventDate(){ id = 1, active = true, planning_id = 1, location = "Eindhoven",  poster = "{\"url\":\"images/eventnight.jpg\",\"name\":\"eventnight.jpg\",\"image\":true}",       images = "", videos = ""},
            new EventDate(){ id = 2, active = true, planning_id = 2, location = "Helmond",    poster = "{\"url\":\"images/trapnight.jpg\",\"name\":\"trapnight.jpg\",\"image\":true}",         images = "[{\"url\":\"images/Camera.jpg\",\"name\":\"Camera.jpg\",\"image\":true},{\"url\":\"images/tree.jpg\",\"name\":\"tree.jpg\",\"image\":true},{\"url\":\"images/india.jpg\",\"name\":\"india.jpg\",\"image\":true},{\"url\":\"images/boat.jpg\",\"name\":\"boat.jpg\",\"image\":true},{\"url\":\"images/tree.jpg\",\"name\":\"tree.jpg\",\"image\":true},{\"url\":\"images/india.jpg\",\"name\":\"india.jpg\",\"image\":true},{\"url\":\"images/boat.jpg\",\"name\":\"boat.jpg\",\"image\":true},{\"url\":\"images/Camera.jpg\",\"name\":\"Camera.jpg\",\"image\":true},{\"url\":\"images/india.jpg\",\"name\":\"india.jpg\",\"image\":true},{\"url\":\"images/boat.jpg\",\"name\":\"boat.jpg\",\"image\":true},{\"url\":\"images/Camera.jpg\",\"name\":\"Camera.jpg\",\"image\":true},{\"url\":\"images/tree.jpg\",\"name\":\"tree.jpg\",\"image\":true},{\"url\":\"images/boat.jpg\",\"name\":\"boat.jpg\",\"image\":true},{\"url\":\"images/Camera.jpg\",\"name\":\"Camera.jpg\",\"image\":true},{\"url\":\"images/tree.jpg\",\"name\":\"tree.jpg\",\"image\":true},{\"url\":\"images/india.jpg\",\"name\":\"india.jpg\",\"image\":true}]", videos = "[\"https://www.youtube.com/watch?v=sxGKgndSfCE\",\"https://www.youtube.com/watch?v=KWy7lLW6fnM\",\"https://www.youtube.com/watch?v=x0ZKspyIHH0\",\"https://www.youtube.com/watch?v=OPdbdjctx2I\",\"https://www.youtube.com/watch?v=YpLMnziTwCA\"]"}
            new EventDate(){ id = 3, active = true, planning_id = 3, location = "Maastricht", poster = "{\"url\":\"images/musicfestival.jpg\",\"name\":\"musicfestival.jpg\",\"image\":true}", images = "", videos = ""},
            new EventDate(){ id = 4, active = true, planning_id = 4, location = "Maastricht", poster = "{\"url\":\"images/musicfestival.jpg\",\"name\":\"musicfestival.jpg\",\"image\":true}", images = "", videos = ""},
            new EventDate(){ id = 5, active = true, planning_id = 5, location = "Helmond",    poster = "{\"url\":\"images/trapnight.jpg\",\"name\":\"trapnight.jpg\",\"image\":true}",         images = "[{\"url\":\"images/Camera.jpg\",\"name\":\"Camera.jpg\",\"image\":true},{\"url\":\"images/tree.jpg\",\"name\":\"tree.jpg\",\"image\":true},{\"url\":\"images/india.jpg\",\"name\":\"india.jpg\",\"image\":true},{\"url\":\"images/boat.jpg\",\"name\":\"boat.jpg\",\"image\":true},{\"url\":\"images/tree.jpg\",\"name\":\"tree.jpg\",\"image\":true},{\"url\":\"images/india.jpg\",\"name\":\"india.jpg\",\"image\":true},{\"url\":\"images/boat.jpg\",\"name\":\"boat.jpg\",\"image\":true},{\"url\":\"images/Camera.jpg\",\"name\":\"Camera.jpg\",\"image\":true},{\"url\":\"images/india.jpg\",\"name\":\"india.jpg\",\"image\":true},{\"url\":\"images/boat.jpg\",\"name\":\"boat.jpg\",\"image\":true},{\"url\":\"images/Camera.jpg\",\"name\":\"Camera.jpg\",\"image\":true},{\"url\":\"images/tree.jpg\",\"name\":\"tree.jpg\",\"image\":true},{\"url\":\"images/boat.jpg\",\"name\":\"boat.jpg\",\"image\":true},{\"url\":\"images/Camera.jpg\",\"name\":\"Camera.jpg\",\"image\":true},{\"url\":\"images/tree.jpg\",\"name\":\"tree.jpg\",\"image\":true},{\"url\":\"images/india.jpg\",\"name\":\"india.jpg\",\"image\":true}]", videos = "[\"https://www.youtube.com/watch?v=sxGKgndSfCE\",\"https://www.youtube.com/watch?v=KWy7lLW6fnM\",\"https://www.youtube.com/watch?v=x0ZKspyIHH0\",\"https://www.youtube.com/watch?v=OPdbdjctx2I\",\"https://www.youtube.com/watch?v=YpLMnziTwCA\"]"}
            new EventDate(){ id = 6, active = true, planning_id = 6, location = "Maastricht", poster = "{\"url\":\"images/musicfestival.jpg\",\"name\":\"musicfestival.jpg\",\"image\":true}", images = "", videos = ""},
            new EventDate(){ id = 7, active = true, planning_id = 7, location = "Maastricht", poster = "{\"url\":\"images/musicfestival.jpg\",\"name\":\"musicfestival.jpg\",\"image\":true}", images = "", videos = ""},
        };

        public List<Preference> preferences { get; } = new List<Preference>()
        {
            new Preference(){ id = 1, user_id = 1, genre_id = 1 },
            new Preference(){ id = 2, user_id = 1, genre_id = 2 },
            new Preference(){ id = 3, user_id = 1, genre_id = 3 },
            new Preference(){ id = 4, user_id = 1, genre_id = 4 },
            new Preference(){ id = 5, user_id = 2, genre_id = 5 },
            new Preference(){ id = 6, user_id = 2, genre_id = 6 },
            new Preference(){ id = 7, user_id = 2, genre_id = 7 },
            new Preference(){ id = 8, user_id = 2, genre_id = 8 }
        };

        public List<Review> reviews { get; } = new List<Review>()
        {
            new Review(){ id = 1, event_date_id = 2, user_id = 1, review = "review content 1", rating = 6, validated = true },
            new Review(){ id = 2, event_date_id = 2, user_id = 2, review = "review content 2", rating = 9, validated = true },
            new Review(){ id = 3, event_date_id = 2, user_id = 3, review = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", rating = 7, validated = true },
            new Review(){ id = 4, event_date_id = 2, user_id = 4, review = "review content 4", rating = 10, validated = true },
            new Review(){ id = 5, event_date_id = 2, user_id = 1, review = "etst", rating = 8, validated = false },
            new Review(){ id = 6, event_date_id = 2, user_id = 1, review = "test", rating = 4, validated = false },
            new Review(){ id = 7, event_date_id = 2, user_id = 2, review = "asdfd", rating = 8, validated = false },
            new Review(){ id = 8, event_date_id = 2, user_id = 4, review = "test", rating = 5, validated = true },
        };

        public List<ScheduleItem> scheduleItems { get; } = new List<ScheduleItem>()
        {
            new ScheduleItem(){ id = 1, schedule_id = 3, artist_id = 1, start = new DateTime(2021, 4, 1, 12, 0, 0), stage_time = 30},
            new ScheduleItem(){ id = 2, schedule_id = 3, artist_id = 2, start = new DateTime(2021, 4, 1, 12, 30, 0), stage_time = 30},
            new ScheduleItem(){ id = 3, schedule_id = 3, artist_id = 1, start = new DateTime(2021, 4, 1, 13, 0, 0), stage_time = 30},
            new ScheduleItem(){ id = 4, schedule_id = 3, artist_id = 2, start = new DateTime(2021, 4, 1, 13, 30, 0), stage_time = 30},
            new ScheduleItem(){ id = 5, schedule_id = 3, artist_id = 1, start = new DateTime(2021, 4, 1, 14, 0, 0), stage_time = 30},
            new ScheduleItem(){ id = 6, schedule_id = 3, artist_id = 2, start = new DateTime(2021, 4, 1, 14, 30, 0), stage_time = 30},
            new ScheduleItem(){ id = 7, schedule_id = 3, artist_id = 1, start = new DateTime(2021, 4, 1, 15, 0, 0), stage_time = 30},
            new ScheduleItem(){ id = 8, schedule_id = 3, artist_id = 2, start = new DateTime(2021, 4, 1, 15, 30, 0), stage_time = 30},
            new ScheduleItem(){ id = 9, schedule_id = 2, artist_id = 4, start = new DateTime(2021, 4, 1, 12, 0, 0), stage_time = 30},
            new ScheduleItem(){ id = 10, schedule_id = 2, artist_id = 3, start = new DateTime(2021, 4, 1, 12, 30, 0), stage_time = 30},
            new ScheduleItem(){ id = 11, schedule_id = 2, artist_id = 4, start = new DateTime(2021, 4, 1, 13, 0, 0), stage_time = 30},
            new ScheduleItem(){ id = 12, schedule_id = 2, artist_id = 3, start = new DateTime(2021, 4, 1, 13, 30, 0), stage_time = 30},
            new ScheduleItem(){ id = 13, schedule_id = 2, artist_id = 4, start = new DateTime(2021, 4, 1, 14, 0, 0), stage_time = 30},
            new ScheduleItem(){ id = 14, schedule_id = 2, artist_id = 3, start = new DateTime(2021, 4, 1, 14, 30, 0), stage_time = 30},
            new ScheduleItem(){ id = 15, schedule_id = 2, artist_id = 4, start = new DateTime(2021, 4, 1, 15, 0, 0), stage_time = 30},
            new ScheduleItem(){ id = 16, schedule_id = 2, artist_id = 3, start = new DateTime(2021, 4, 1, 15, 30, 0), stage_time = 30},
        };

        public List<Schedule> schedules { get; } = new List<Schedule>()
        {
            new Schedule(){ id = 1, stage_id = 1, event_id = 1, dateTime = new DateTime(2019, 4, 1) },
            new Schedule(){ id = 2, stage_id = 2, event_id = 2, dateTime = new DateTime(2020, 4, 1) },
            new Schedule(){ id = 3, stage_id = 3, event_id = 3, dateTime = new DateTime(2021, 4, 1) }
        };

        public List<Stage> stages { get; } = new List<Stage>()
        {
            new Stage(){ id = 1, event_date_id = 1, name = "stage A"},
            new Stage(){ id = 2, event_date_id = 3, name = "stage A"},
            new Stage(){ id = 3, event_date_id = 3, name = "stage B"}
        };

        public List<User> users { get; } = new List<User>()
        {
            new User(){ id = 1, name = "test user 1", username = "tuser1", password = "$2a$10$R4I/SVk1aSd/uZZVZmhYVeM5wVOrrcy6jVbBGTfmE0F95s9qibIsq", right_id = 1},
            new User(){ id = 2, name = "test user 2", username = "tuser2", password = "$2a$10$R4I/SVk1aSd/uZZVZmhYVeM5wVOrrcy6jVbBGTfmE0F95s9qibIsq", right_id = 2},
            new User(){ id = 3, name = "test user 3", username = "tuser3", password = "$2a$10$R4I/SVk1aSd/uZZVZmhYVeM5wVOrrcy6jVbBGTfmE0F95s9qibIsq", right_id = 1},
            new User(){ id = 4, name = "test user 4", username = "tuser4", password = "$2a$10$R4I/SVk1aSd/uZZVZmhYVeM5wVOrrcy6jVbBGTfmE0F95s9qibIsq", right_id = 2}
        };
    }
}
