using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventReporter {

    public static List<UserEvent> ReportEvent()
    {
        var ret = new List<UserEvent>(EventBag.GetInstance().GetAll());
        if (ret.Count != 0)
        {
            EventBag.GetInstance().RemoveAll();
        }
        EventBag.nextTurnWait.Set();
        return ret;
    }
}
