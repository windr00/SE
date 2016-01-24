using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GOCollection
{


    public static void AddGameObject(string goId, string assetName)
    {
        var go = GameObject.Instantiate(GameObjectLoader.LoadGameObject(assetName)) as GameObject;
        World.GetInstance().AddGameObject(goId, go);
        var eventGenerator = go.GetComponent<EventGenerator>();
		go.GetComponent<State> ().isInSimulator = true;
        if (eventGenerator != null)
        {
            eventGenerator.AddListener(EventCollection.OnEventTrigger);
            
        }
    }



	public static void DeleteGameObjectByGOId(string goId) {
		World.GetInstance ().RemoveGameObject (goId);
	}
}
