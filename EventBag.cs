using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
public class EventBag
{
    private List<UserEvent> eventList = new List<UserEvent>();

    private static EventBag _instance = null;

    public static ManualResetEvent nextTurnWait = new ManualResetEvent(false);
    public void Add(UserEvent e)
    {
        lock (eventList)
        {
            eventList.Add(e);   
        }
    }

    public List<UserEvent> GetAll()
    {
        Debug.Log("event count: " + eventList.Count);
        return eventList;
    }

	public void RemoveAll() {
		lock (eventList) {
			eventList.Clear();
		}
	}

    public static EventBag GetInstance()
    {

			if (_instance == null) {
				_instance = new EventBag ();
		}
        return _instance;
    }

    private EventBag()
    {
        nextTurnWait.Reset();
    }
}