using UnityEngine;
using System.Collections;

public class PlayerSpawning : MonoBehaviour {

    public System.Action OnSpawnAction;
    private Transform _transform ;
    private PlayerHealth _health;

	// Use this for initialization
	void Start () {
	    _health = GetComponent<PlayerHealth>();
	    _transform = gameObject.transform;
	    _health.OnDeathAction += OnSpawn;
        OnSpawn();
	}

    void OnSpawn() {
        var spawnTrans = SpawnPoints.GetRandomSpawnPoint();
        _transform.position = spawnTrans.position;
        _transform.rotation = spawnTrans.rotation;
        _health.ResetHealth();
        if (OnSpawnAction != null) {
            OnSpawnAction();
        }
    }

    void OnDisable() {
        OnSpawnAction = null;
        if (_health.OnDeathAction != null) {
            _health.OnDeathAction -= OnSpawn;
        }
    }
}
