using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerSounds : MonoBehaviour {

    private PhotonView _view;
    private AudioSource _audioSource;
    private FirstPersonController fpc;

    public AudioClip[] walkSound;
    public AudioClip ShootSound;

	// Use this for initialization
	void Start () {
        _view = GetComponent<PhotonView>();
        _audioSource = GetComponent<AudioSource>();
        fpc = GetComponent<FirstPersonController>();
        fpc.OnWalkSound += HandleOnWalkSound;
	}

    void HandleOnWalkSound(int footStepIndex) {
        _view.RPC("WalkSound", PhotonTargets.All, footStepIndex);
    }
	
    [RPC]
    public void WalkSound(int i) {
        _audioSource.PlayOneShot(walkSound[i]);
    }

    [RPC]
    public void ShootGunSound() {
        _audioSource.PlayOneShot(ShootSound);
    }
}
