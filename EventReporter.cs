using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventReporter {

    public static List<UserEvent> ReportEvent()
    {
        var ret = EventBag.GetInstance().GetAll();
		EventBag.GetInstance ().RemoveAll ();
		return ret;
    }
}
