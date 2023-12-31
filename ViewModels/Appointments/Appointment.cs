﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace CallAssistant.ViewModels.Appointments
{
    [JsonObject]
    public class Appointment
    {
        [JsonProperty("startTime")]
        public DateTime StartTime { get; set; }

        [JsonProperty("endTime")]
        public DateTime EndTime { get; set; }

        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public static class AppointmentSeed
    {
        public static List<Appointment> SeedAppointmnets()
        {
            return new List<Appointment>() {
                new Appointment() {
                    EndTime = DateTime.Now.Date + new TimeSpan(10,30,0),
                    StartTime = DateTime.Now.Date + new TimeSpan(10,0,0),
                    Name = "Check Up",
                    Provider = "Dr. John Smith"
                },
                new Appointment() {
                    EndTime = DateTime.Now.Date + new TimeSpan(14,0,0),
                    StartTime = DateTime.Now.Date + new TimeSpan(14,45,0),
                    Name = "Specialist Consultation",
                    Provider = "Dr. Emit Brown"
                },
                new Appointment() {
                    EndTime = DateTime.Now.AddDays(1).Date + new TimeSpan(10,0,0),
                    StartTime = DateTime.Now.AddDays(1).Date + new TimeSpan(10,45,0),
                    Name = "Exam",
                    Provider = "Dr. John Smith"
                },
                new Appointment() {
                    EndTime = DateTime.Now.AddDays(1).Date + new TimeSpan(15,0,0),
                    StartTime = DateTime.Now.AddDays(1).Date + new TimeSpan(16,45,0),
                    Name = "Ortho",
                    Provider = "Dr. ORiley"
                },
            };
        }
    }
}
