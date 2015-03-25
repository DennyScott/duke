using UnityEngine;
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

	// Use this for initialization
	void Start () {
 			
	}

	void Update() {
		if(Input.GetButtonDown("Fire1")) {
			if(currentState != States.EMPTY){
				FireGun();
			}
		}
	}

	public void EquipWeapon() {
		source.PlayOneShot(reloadSound);
		weapon.SetActive(true);
		currentState = States.PISTOL;
	}


	private void FireGun() {
		source.PlayOneShot(shootSound);
		 int x = Screen.width / 2;
        int y = Screen.height / 2;
 		RaycastHit hit;

        Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));

        if(Physics.Raycast(ray, out hit, 500)) {
        	if(hit.collider.tag == "Player") {
        		GameObject otherPlayer = hit.transform.gameObject;
        		otherPlayer.GetComponent<PlayerHealth>().RemoveHealth(10);
        	}
        }
	}
}
