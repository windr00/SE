using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GOCollection
{

    public static void AddGameObjectWithClientId(string clientId, string goId, string assetName)
    {
        var go = GameObject.Instantiate(GameObjectLoader.LoadGameObject(assetName)) as GameObject;
        World.GetInstance().AddGameObjectWithClientId(clientId, goId, go);
        var eventGenerator = go.GetComponent<EventGenerator>();
        if (eventGenerator != null)
        {
            eventGenerator.OnEventGenerated += EventCollection.OnEventTrigger;
        }
    }

    public static void AddGameObjectWithoutClientId(string goId, string assetName)
    {
        var go = GameObject.Instantiate(GameObjectLoader.LoadGameObject(assetName)) as GameObject;
        World.GetInstance().AddGameObjectWithoutClientId(goId, go);
        var eventGenerator = go.GetComponent<EventGenerator>();
        if (eventGenerator != null)
        {
            eventGenerator.OnEventGenerated += EventCollection.OnEventTrigger;
            
        }
    }


    public static void DeleteGameObject(string id)
    {
        World.GetInstance().RemoveGameObject(id);
    }
}
