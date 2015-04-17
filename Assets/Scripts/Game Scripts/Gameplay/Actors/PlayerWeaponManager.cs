using UnityEngine;
using System.Collections;

public class PlayerWeaponManager : MonoBehaviour {
	enum States {Empty, Pistol};
	private States _currentState = States.Empty;


	public GameObject Weapon;
	public AudioClip ShootSound;
	public AudioClip ReloadSound;

    private PhotonView _view;
	private AudioSource _source;

	void Awake() {
		_source = GetComponent<AudioSource>();
	    _view = GetComponent<PhotonView>();
	}

	void Update() {
	    Shoot();
	}

    void Shoot() {
        if (!Input.GetButtonDown("Fire1")) {
            return;
        }

        if (_currentState != States.Empty) {
            FireGun();
        }
    }

    public void EquipWeapon() {
		_source.PlayOneShot(ReloadSound);
		Weapon.SetActive(true);
		_currentState = States.Pistol;
	}


	private void FireGun() {
		_source.PlayOneShot(ShootSound);
		var x = Screen.width / 2;
        var y = Screen.height / 2;
 		RaycastHit hit;

        var ray = GetComponentInChildren<Camera>().ScreenPointToRay(new Vector3(x, y));
        _view.RPC("ShootGunSound", PhotonTargets.All);

	    if (!Physics.Raycast(ray, out hit, 500)) {
	        return;
	    }
	    
        if(hit.collider.tag == "PlayerBody") {
	        hit.collider.transform.root.GetComponent<PhotonView>().RPC("RemoveHealth", PhotonTargets.AllBuffered, 10);
	    }
	}

}
