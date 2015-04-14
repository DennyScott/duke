using UnityEngine;
using System.Collections;

public class PlayerHealth : Photon.MonoBehaviour {
	public int PlayersHealth = 100;

    public System.Action OnDeathAction;

	public Animator DamageAnimation;

	public void AddHealth(int heal) {
		PlayersHealth += heal;
	}

	public void ResetHealth() {
		PlayersHealth = 100;
	}

	public void RemoveHealth(int damage) {
		PlayersHealth -= damage;
		if(PlayersHealth <= 0) {
			OnDeath();
		}
		if(DamageAnimation != null){
			DamageAnimation.SetTrigger("Hit");
		}

	}

	void OnDeath() {
	    if (OnDeathAction != null) {
	        OnDeathAction();
	    }
	}

    void OnDisable() {
        OnDeathAction = null;
    }

	void ResetPosition() {
	    
	}
}
