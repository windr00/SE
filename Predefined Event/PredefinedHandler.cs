using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PredefinedHandler : EventHandler
{
    public override object SelfDeserialize(UserEvent.EventType type, byte[] body)
    {

    }

    public override void Handle(UserEvent e)
    {
        switch (e.type)
        {
            case UserEvent.EventType.LA:
                {
                    GameObjectLoader.LoadGameObject(e.rawContent as string);
                    break;
                }
            case UserEvent.EventType.GA:
                {
                    
                    break;
                }
            case UserEvent.EventType.GR:
                {

                    break;
                }
            default:
                {
                    return;
                }
        }
    }
}