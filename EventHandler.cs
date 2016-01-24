using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public abstract class EventHandler : MonoBehaviour
{
    public abstract object SelfDeserialize(UserEvent.EventType type, byte[] body);

    public abstract void Handle(UserEvent e);
}
