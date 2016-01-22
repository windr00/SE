using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

public abstract class EventGenerator : MonoBehaviour
{
    public delegate void EventGenerated(UserEvent e);

    public event EventGenerated OnEventGenerated;
    public abstract void GenerateEvent(List<GameObject> gameObjects);

    public abstract byte[] SelfSerialize(UserEvent.EventType type, object content);

    public string gameObjectId { get; set; }

    void FixedUpdate()
    {
        Dictionary<string, EventGenerated> a = new Dictionary<string, EventGenerated>();
        GenerateEvent(World.GetInstance().GetAllGameObjects());
    }

}