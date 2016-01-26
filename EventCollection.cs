using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EventCollection
{


    public static void OnEventTrigger(UserEvent e)
    {
        EventBag.nextTurnWait.WaitOne();
        Debug.Log("event added : type: " + e.type);
        EventBag.GetInstance().Add(e);
    }

}
