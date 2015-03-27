using UnityEngine;
using System.Collections;

public class TriggerWeapon : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		if(other.transform.tag == "Player") {
			other.transform.gameObject.GetComponentInChildren<PlayerWeaponManager>().EquipWeapon();
			Destroy(gameObject);
		}
	}
}
