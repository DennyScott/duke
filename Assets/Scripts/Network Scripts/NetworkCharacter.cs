using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkCharacter : Photon.MonoBehaviour {

	private Vector3 correctPlayerPos;
    private Quaternion correctPlayerRot;
 
	void Start() {
		if(photonView.isMine) {
			GetComponent<PlayerAnimations>().DamageAnimator = GameObject.FindGameObjectWithTag("DamageHud").GetComponent<Animator>();
			GetComponent<HUDManager>().HealthHud = GameObject.FindGameObjectWithTag("HealthHud").GetComponent<Text>();
		}
	}
    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, 0.1f);
        }
    }


	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        } else {
            // Network player, receive data
            correctPlayerPos = (Vector3) stream.ReceiveNext();
            correctPlayerRot = (Quaternion) stream.ReceiveNext();
        }
	}
}
