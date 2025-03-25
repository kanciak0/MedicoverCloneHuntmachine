﻿using System;

namespace MedicoverClone.Domain
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime Date { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; } 
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
    public enum AppointmentStatus
    {
        Pending,
        Completed,
        Canceled,
    }
}
