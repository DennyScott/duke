using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : Photon.MonoBehaviour {
	public int PlayersHealth = 100;

    public System.Action OnDeathAction;

	public Animator DamageAnimation;
	public Text healthHUD;

	public void AddHealth(int heal) {
		PlayersHealth += heal;
		healthHUD.text = PlayersHealth + "";
	}

	public void ResetHealth() {
		PlayersHealth = 100;
		healthHUD.text = PlayersHealth + "";
	}

	public void RemoveHealth(int damage) {
		PlayersHealth -= damage;
		if(PlayersHealth <= 0) {
			OnDeath();
		}
		if(DamageAnimation != null){
			DamageAnimation.SetTrigger("Hit");
		}
		healthHUD.text = PlayersHealth + "";

	}

	void OnDeath() {
	    if (OnDeathAction != null) {
	        OnDeathAction();
	    }
        MatchSettings.TriggerDeath();
	}

    void OnDisable() {
        OnDeathAction = null;
    }

	void ResetPosition() {
	    
	}
}
