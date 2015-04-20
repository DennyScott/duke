using UnityEngine;
using System.Collections;

public class Weapon : Grunt {

	public AudioClip ShootSound;
	public AudioClip ReloadSound;
	public GameObject BulletEffect;

	public void FireGun(AudioSource source, PhotonView view) {
		source.PlayOneShot(ShootSound);
		var x = Screen.width / 2;
		var y = Screen.height / 2;
		RaycastHit hit;

		var ray = GetComponentInChildren<Camera>().ScreenPointToRay(new Vector3(x, y));
		view.RPC("ShootGunSound", PhotonTargets.All);

		if (!Physics.Raycast(ray, out hit, 500)) {
			return;
		}

		if (hit.collider.tag == "PlayerBody") {
			hit.collider.transform.root.GetComponent<PhotonView>().RPC("RemoveHealth", PhotonTargets.AllBuffered, 10);
		}

		if (hit.collider.tag == "World") {
			var g = (GameObject)Instantiate(BulletEffect, hit.point, new Quaternion());
			StartCoroutine("KillSmoke", g);
		}
	}

	IEnumerator KillSmoke(GameObject g) {
		yield return new WaitForSeconds(0.7f);
		Destroy(g);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
