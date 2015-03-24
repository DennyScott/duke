using UnityEngine;
using System.Collections;

public class PlayerWeaponController : MonoBehaviour {

	public GameObject weapon;

	// Use this for initialization
	void Start () {
 			
	}

	public void EquipWeapon() {
		weapon.SetActive(true);
	}
}
