using System;
using DoctorBooker.Infrastructure;

namespace DoctorBooker.CommandModel
{
    public struct ScheduleSlot : ICommand
    {
        public readonly int DoctorId;
        public readonly DateTime StartDateTime;
        public readonly DateTime EndDateTime;

        public ScheduleSlot(int doctorId, DateTime start, DateTime end)
        {
            DoctorId = doctorId;
            StartDateTime = start;
            EndDateTime = end;
        }
    }
}