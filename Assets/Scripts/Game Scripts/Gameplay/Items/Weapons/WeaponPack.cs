using UnityEngine;
using System.Collections;

public class WeaponPack : MonoBehaviour {
	private Weapon weapon;
	public Weapon[] weapons;

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			other.gameObject.GetComponentInChildren<PlayerWeaponManager>().PickUpWeapon(weapon);
			Destroy(gameObject);
		}
	}
}
