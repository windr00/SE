using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventReporter {

    public static List<UserEvent> ReportEvent()
    {
        return EventBag.GetInstance().GetAll();
    }
}
