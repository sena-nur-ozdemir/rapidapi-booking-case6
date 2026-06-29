using System;
using System.Collections.Generic;

namespace RapidApiBookingCase.Dtos.DashboardDtos
{
    public class NewsDto
    {
        public List<NewsData> data { get; set; }
    }
    public class NewsData
    {
        public string title { get; set; }
        public string link { get; set; }
        public string snippet { get; set; }
        public string photo_url { get; set; }
        public string thumbnail_url { get; set; }
        public string source_name { get; set; }
        public DateTime published_datetime_utc { get; set; }
    }
}