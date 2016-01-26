using System;
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

	public void RemoveAll() {
		lock (eventList) {
			eventList.Clear();
		}
	}

    public static EventBag GetInstance()
    {
		lock (_instance) {
			if (_instance == null) {
				_instance = new EventBag ();
			}
		}
        return _instance;
    }

    private EventBag()
    {
    }
}