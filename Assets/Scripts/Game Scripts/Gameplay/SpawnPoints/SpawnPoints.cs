using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoints : MonoBehaviour {

    private static List<GameObject> _spawnPoints = new List<GameObject>();

    private static int _lastIndex;
 
	// Use this for initialization
	void Awake () {
	    RandomMatchmaker.JoinedRoom += HandleOnJoinRoom;
	}

    void HandleOnJoinRoom(GameObject player) {
        CollectSpawnPoints();
    }

    /// <summary>
    /// Collects all spawn points in the level
    /// </summary>
    public void CollectSpawnPoints() {
        //Grabs all spawn points that are a child of this game object
        foreach (var child in GetComponentsInChildren<SpawnPoint>()) {
            _spawnPoints.Add(child.gameObject);
        }
        Debug.Log(_spawnPoints.Count);
        _lastIndex = _spawnPoints.Count;
    }

    /// <summary>
    /// Returns a random spawn point to generate the player at
    /// </summary>
    /// <returns>A transform to spawn the player at</returns>
    public static Transform GetRandomSpawnPoint() {
        _lastIndex = _lastIndex + 1 >= _spawnPoints.Count ? 0 : _lastIndex + 1;  //If the index is higher then the list, it will reset back to 0
        return _spawnPoints[_lastIndex].transform;
    }
}
