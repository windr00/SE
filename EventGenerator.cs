using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

public abstract class EventGenerator : MonoBehaviour
{
    public delegate void EventGenerated(UserEvent e);

    private event EventGenerated OnEventGenerated;
    public abstract void GenerateEvent(object boxed);

    public abstract byte[] SelfSerialize(UserEvent.EventType type, object content);

    public string gameObjectId { get; set; }

	public void AddListener(EventGenerated call) {
		OnEventGenerated += call;
	}

	protected void BroadCastEvent(UserEvent e) {
		if (OnEventGenerated != null) {
			OnEventGenerated(e);
		}
	}

    void FixedUpdate()
    {
        GenerateEvent(World.GetInstance().GetAllGameObjects());
    }

}