using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UserEvent {
    public enum EventType
    {
        LA,
        UPA,
        GA,
        GR,
        CTR,
        CMD,
        ST,
        ENV
    }

    public string sponsorId { get; set; }

    public List<string> targetIdList { get; set; }

    public EventType type { get; set; }

    public object rawContent { get; set; }


}

