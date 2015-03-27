using UnityEngine;
using System.Collections;

public class PlayerHealth : Photon.MonoBehaviour {
	public int playersHealth = 100;

	public Animator damageAnimation;

	public void AddHealth(int heal) {
		playersHealth += heal;
	}

	public void RemoveHealth(int damage) {
		Debug.Log("Player Hit with " + damage + " damage");
		Debug.Log("Total Health Remaining " + playersHealth);
		playersHealth -= damage;
		if(playersHealth <= 0) {
			Debug.Log("Player Died");
		}
		if(damageAnimation != null){
			damageAnimation.SetTrigger("Hit");
		}

	}
}
