using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoints : MonoBehaviour {

    private static List<GameObject> _spawnPoints = new List<GameObject>();
 
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
    }

    /// <summary>
    /// Returns a random spawn point to generate the player at
    /// </summary>
    /// <returns>A transform to spawn the player at</returns>
    public static Transform GetRandomSpawnPoint() {
        MatchSettings.SpawnIndex = MatchSettings.SpawnIndex + 1 >= _spawnPoints.Count ? 0 : MatchSettings.SpawnIndex + 1;  //If the index is higher then the list, it will reset back to 0
        return _spawnPoints[MatchSettings.SpawnIndex].transform;
    }
}
