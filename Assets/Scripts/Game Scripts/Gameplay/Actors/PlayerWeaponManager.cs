﻿using UnityEngine;
using System.Collections;

public class PlayerWeaponManager : MonoBehaviour {
	enum States {EMPTY, PISTOL};
	private States currentState = States.EMPTY;


	public GameObject weapon;
	public AudioClip shootSound;
	public AudioClip reloadSound;

	private AudioSource source;

	void Awake() {
		source = GetComponent<AudioSource>();
	}

	void Update() {
	    Shoot();
	}

    void Shoot() {
        if (!Input.GetButtonDown("Fire1")) {
            return;
        }

        if (currentState != States.EMPTY) {
            FireGun();
        }
    }

    public void EquipWeapon() {
		source.PlayOneShot(reloadSound);
		weapon.SetActive(true);
		currentState = States.PISTOL;
	}


	private void FireGun() {
		source.PlayOneShot(shootSound);
		var x = Screen.width / 2;
        var y = Screen.height / 2;
 		RaycastHit hit;

        var ray = GetComponentInChildren<Camera>().ScreenPointToRay(new Vector3(x, y));

        if(Physics.Raycast(ray, out hit, 500)) {
			Debug.Log (hit.collider.tag == "Player");
        	if(hit.collider.tag == "PlayerBody") {
				hit.collider.transform.root.GetComponent<PhotonView>().RPC("RemoveHealth", PhotonTargets.AllBuffered, 10);
        	}
        }
	}

}
