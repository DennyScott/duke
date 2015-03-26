using UnityEngine;
using System.Collections;

public class TriggerWeapon : MonoBehaviour {
	public GameObject weapon;
	public GameObject player;

	void OnTriggerEnter(Collider other) {
		player.GetComponent<PlayerWeaponManager>().EquipWeapon();
		Destroy(gameObject);
	}
}
