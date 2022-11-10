using CommandModel;
using CommandModel.Event;
using System;
using System.Collections.Generic;
using System.Linq;

public class AvailableSlotsProjector: IEventListener
{
    private List<AvailableSlot> _availableSlots;

    public AvailableSlotsProjector()
    {
        _availableSlots = new List<AvailableSlot>();
    }

    public void When(IEvent evt)
    {
        if (evt is SlotWasScheduled scheduledEvent)
        {
            When(scheduledEvent);
        }
        if (evt is SlotWasBooked bookedEvent)
        {
            When(bookedEvent);
        }
    }

    public void When(SlotWasScheduled evt)
    {
        _availableSlots.Add(new AvailableSlot
        {
            SlotId = evt.SlotId,
            DoctorId = evt.DoctorId,
            StartTime = evt.StartDateTime,
            EndTime = evt.EndDateTime
        });    
    }

    public void When(SlotWasBooked evt)
    {
        var slot = _availableSlots.SingleOrDefault(x => x.SlotId == evt.SlotId);
        if(slot != null)
        {
            _availableSlots.Remove(slot);
        }
    }

    public List<AvailableSlot> GetAllAvailableSlotsForDay(DateTime day)
    {
        return _availableSlots.Where(s => s.StartTime.Date == day.Date).ToList();
    }
}