using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;

public class World 

{
    private Dictionary<string, GameObject> gameObjectCollection = new Dictionary<string, GameObject>();
    private Dictionary<string, string> clientIdToGameObjectIdCollection = new Dictionary<string, string>();
    private GameObject predefiendGameObject = null;

    private static World _instance = null;

    public GameObject GetPredefiendGO()
    {
        return predefiendGameObject;
    }

    public void AddGameObjectWithoutClientId(string goId, GameObject go)
    {
        gameObjectCollection.Add(goId, go);
    }

    public void AddGameObjectWithClientId(string clientId, string goId, GameObject go)
    {
        clientIdToGameObjectIdCollection.Add(clientId, goId);
        gameObjectCollection.Add(goId, go);
    }

    public void RemoveGameObjectByGOId(string goId)
    {
        gameObjectCollection.Remove(goId);
    }

    public void RemoveGameObjectByClientId(string clientId)
    {
        var goid = clientIdToGameObjectIdCollection[clientId];
        gameObjectCollection.Remove(goid);
        clientIdToGameObjectIdCollection.Remove(clientId);
    }

    public List<GameObject> GetAllGameObjects()
    {
        return new List<GameObject>(gameObjectCollection.Values);
    }

    public GameObject GetGameObjectByGOId(string goId)
    {
        return gameObjectCollection[goId];
    }

    public GameObject GetGameObjectByClientId(string clientId)
    {
        return gameObjectCollection[clientIdToGameObjectIdCollection[clientId]];
    }
    
 
    public static World GetInstance() 
    {
        if (_instance == null) 
        {
            _instance = new World();
        }
        return _instance;
    }

    private World()
    {
        predefiendGameObject = GameObject.Find("Predefined");
    }

}
