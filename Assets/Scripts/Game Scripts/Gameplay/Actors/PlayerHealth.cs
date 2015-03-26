using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int playersHealth = 100;
	public GameObject damageTaken;

	private Animator damageAnimation;

	void Awake() {
		damageAnimation = damageTaken.GetComponent<Animator>();
	}


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
