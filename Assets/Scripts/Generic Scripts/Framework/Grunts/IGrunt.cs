using UnityEngine;
using System.Collections;
using System;

public interface IGrunt {
	
	void Subscribe(string key, Action<GameObject> sub);
	void SubscribeBool(string key, Func<GameObject, bool> sub);

	void Unsubscribe(string key, Action<GameObject> sub);
	void UnsubscribeBool(string key, Func<GameObject, bool> sub);
}
