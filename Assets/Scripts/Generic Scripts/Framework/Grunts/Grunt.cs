using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Base Component. All Components should inherit from this class.
/// </summary>
public abstract class Grunt : EventEmitter, IGrunt {
	private EventDispatcher localDispatcher = new EventDispatcher();

	protected void Publish(string key, GameObject g){
       localDispatcher.Publish(key, g); 
	}
	protected bool PublishBool(string key, GameObject g) {
        return localDispatcher.PublishBool(key, g);
	}

	public void Subscribe(string key, Action<GameObject> sub) {
        localDispatcher.Subscribe(key, sub);
	}
	public void SubscribeBool(string key, Func<GameObject, bool> sub){
        localDispatcher.SubscribeBool(key, sub);
	}

    public void Unsubscribe(string key, Action<GameObject> sub) {
        localDispatcher.Unsubscribe(key, sub);
    }
    public void UnsubscribeBool(string key, Func<GameObject, bool> sub) {
        localDispatcher.UnsubscribeBool(key, sub);
    }

    void OnDisable() {
    	localDispatcher.ClearAll();	
    }


}
