using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour {

	private PlayerHealth playerHealth;

	public Animator DamageAnimator;

	// Use this for initialization
	void Start () {
		playerHealth = GetComponent<PlayerHealth>();
		playerHealth.OnHealthLost += DamageAnimation;
	}

	void DamageAnimation(GameObject g) {
		if (DamageAnimator != null) {
			DamageAnimator.SetTrigger("Hit");
		}
	}
}
