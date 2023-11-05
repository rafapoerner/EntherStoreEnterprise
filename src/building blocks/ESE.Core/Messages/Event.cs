﻿using MediatR;

namespace ESE.Core.Messages
{
    public class Event : Message, INotification
    {
        public DateTime TimeStamp { get; private set; }

        public Event()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
