﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EventBag
{
    private List<UserEvent> eventList = new List<UserEvent>();

    private static EventBag _instance = null;

    public void Add(UserEvent e)
    {
        lock (eventList)
        {
            eventList.Add(e);   
        }
    }

    public List<UserEvent> GetAll()
    {
        return eventList;
    }

    public static EventBag GetInstance()
    {
        return _instance;
    }

    private EventBag()
    {
        _instance = this;
    }
}