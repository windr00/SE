using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;

public class World 

{
    private Dictionary<string, GameObject> gameObjectCollection = new Dictionary<string, GameObject>();
    private GameObject predefiendGameObject = null;

    private static World _instance = null;

    public GameObject GetPredefiendGO()
    {
        return predefiendGameObject;
    }

    public void AddGameObject(string goId, GameObject go)
    {
        gameObjectCollection.Add(goId, go);
    }
    

    public void RemoveGameObject(string goId)
    {
        gameObjectCollection.Remove(goId);
    }

    

    public List<GameObject> GetAllGameObjects()
    {
        return new List<GameObject>(gameObjectCollection.Values);
    }

	public List<string> GetAllGOIds() {
		return new List<string> (gameObjectCollection.Keys);
	}

    public GameObject GetGameObject(string goId)
    {
		if (goId.Equals ("0")) {
			return predefiendGameObject;
		}
        return gameObjectCollection[goId];
    }
    
 
    public static World GetInstance() 
    {
		lock (_instance) {
			if (_instance == null) {
				_instance = new World ();
			}
		}
        return _instance;
    }

    private World()
    {
        predefiendGameObject = GameObject.Find("Predefined");
    }

}
