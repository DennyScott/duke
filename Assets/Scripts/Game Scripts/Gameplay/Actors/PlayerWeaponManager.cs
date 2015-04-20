using System;
using UnityEngine;
using System.Collections;

public class PlayerWeaponManager : Manager {
	#region States
	enum States {Empty, Pistol};
	private States _currentState = States.Empty;
	#endregion

	#region Delegates and Events
	public System.Action<GameObject> OnFire;
	public System.Action<GameObject> OnWeaponPickup;
	public System.Func<GameObject, bool> CanFire;
	#endregion

	#region Public Variables
	public GameObject InHandWeapon;
	#endregion

	#region Private Variables
	private PhotonView _view;
	private AudioSource _source;
	private Weapon _weaponData;
	#endregion

	#region Standard Methods
	void Awake() {
		_source = GetComponent<AudioSource>();
	    _view = GetComponent<PhotonView>();
	}

	void Start() {
		OnWeaponPickup += HandleWeaponPickUp;
	}

	void Update() {
	    Shoot();
	}

	#endregion

	#region Private Methods
	void Shoot() {
        if (!Input.GetButtonDown("Fire1")) {
            return;
        }

        if (_currentState != States.Empty) {
            TriggerFireGun();
        }
    }

	void HandleWeaponPickUp(GameObject g) {
		_source.PlayOneShot(_weaponData.ReloadSound);
		InHandWeapon.SetActive(true);
		_currentState = States.Pistol;
	}
	#endregion

	#region Public Methods
	public void PickUpWeapon(Weapon weapon) {
	    _weaponData = weapon;
		TriggerWeaponPickUp();
	}
	#endregion

	#region Event Triggers
	void TriggerWeaponPickUp() {
		if (OnWeaponPickup != null) {
			OnWeaponPickup(gameObject);
		}
	}


	void TriggerFireGun() {
		if (CanFire != null && !CanFire(gameObject)) {
			return;
		}
		_weaponData.FireGun(_source, _view);
		if (OnFire != null) {
			OnFire(gameObject);
		}
	}
	#endregion

}
