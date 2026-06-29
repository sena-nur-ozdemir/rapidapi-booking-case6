using Newtonsoft.Json;
using System.Collections.Generic;

namespace RapidApiBookingCase.Dtos.DashboardDtos
{
    public class FootballDto
    {
        public Response response { get; set; }
    }
    public class Response
    {
        public List<Match> live { get; set; }
    }
    public class Match
    {
        public Team home { get; set; }
        public Team away { get; set; }
        public Status status { get; set; }
    }
    public class Team
    {
        public string name { get; set; }
        public int score { get; set; }
    }
    public class Status
    {
        public Time liveTime { get; set; }
    }
    public class Time
    {
        [JsonProperty("short")]
        public string Short { get; set; }
    }
}