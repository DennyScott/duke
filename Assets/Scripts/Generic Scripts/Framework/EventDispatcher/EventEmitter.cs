using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// EventEmitter is used to attach Scripts to the Global PubSub System. If a script
/// inherits from this script, the class can make publish and subscription calls.
/// </summary>
public abstract class EventEmitter : MonoBehaviour {
	private List<KeyValuePair<string, Action<GameObject>>> actionEvents = new List<KeyValuePair<string, Action<GameObject>>>();
	private List<KeyValuePair<string, Func<GameObject, bool>>> funcEvents = new List<KeyValuePair<string, Func<GameObject, bool>>>();


	/// <summary>
	/// Subscribe to an event in the pub sub dictionaries. This will attach
	/// to the dictionary containing only BoolActions, which return bool values
	/// on execution.
	/// </summary>
	/// <param name="key">Name of Value in Dictionary</param>
	/// <param name="sub">Subscription of methods to Key</param>
	public void GlobalSubscribeBool(string key, Func<GameObject, bool> b){
		funcEvents.Add(new KeyValuePair<string, Func<GameObject, bool>>(key, b));
		GlobalDispatcher.SubscribeBool(key, b);
	}

	/// <summary>
	/// Subscribe to an event in the pub sub dictionaries. This will attach
	/// to the dictionary containing only VoidActions, which do not return values
	/// on execution.
	/// </summary>
	/// <param name="key">Name of Value in Dictionary</param>
	/// <param name="sub">Subscription of methods to Key</param>
	public void GlobalSubscribe(string key, Action<GameObject> sub) {
		actionEvents.Add(new KeyValuePair<string, Action<GameObject>>(key, sub));
		GlobalDispatcher.Subscribe(key, sub);
	}

	/// <summary>
	/// Publish an Event. This will find the dictionary containing only VoidActions
	/// and then find the event with a key attached named the same as the passed string.
	/// </summary>	
	/// <param name="key">Name of the Event in Dictionary</param>
	/// <param name="g">Game Object where the Event took place </param>
	public void GlobalPublish(string key, GameObject g) {
		GlobalDispatcher.Publish(key, g);
	}

	/// <summary>
	/// Publish an Event. This will find the dictionary containing only BoolActions
	/// and then find the event with a key attached named the same as the passed string.
	///
	/// This publish will be used if we are connected to some sort of "CanMove"-like 
	/// event, where we want to determine if it's possible to enact an event.
	/// </summary>	
	/// <param name="key">Name of the Event in Dictionary</param>
	/// <param name="g">Game Object where the Event took place </param>
	/// <param name="returnValue">Boolean returned from the publish event</param>
	public bool GlobalPublishBool(string key, GameObject g) {
		return GlobalDispatcher.PublishBool(key, g);
	}

	/// <summary>
	/// Unsubscribe an Action Event from the Global pub sub.
	/// This will check if the action event exists, and if it does,
	/// remove it from the local memory, and then remove it from the global
	/// pub sub.
	/// </summary>
	/// <param name="key">key Subscrbed to</param>
	/// <param name="sub">sub Method to be called</param>
	public void GlobalUnsubscribe(string key, Action<GameObject> sub) {
		int index = actionEvents.FindIndex(pair => pair.Key == key && pair.Value == sub);
		if(index != -1) {
			actionEvents.RemoveAt(index);
			UnsubscribeDispatcher(key, sub);
		}
	}

	/// <summary>
	/// Unsubscribe a Func Event from the Global pub sub.
	/// This will check if the Func event exists, and if it does,
	/// remove it from the local memory, and then remove it from the global
	/// pub sub.
	/// </summary>
	/// <param name="key">key Subscrbed to</param>
	/// <param name="sub">sub Method to be called</param>
	public void GlobalUnsubscribeBool(string key, Func<GameObject, bool> sub) {
		int index = funcEvents.FindIndex(pair => pair.Key == key && pair.Value == sub);
		if(index != -1) {
			funcEvents.RemoveAt(index);
			UnsubscribeBoolDispatcher(key, sub);
		}
	}

	/// <summary>Call Unsubscribe in the Event Dispatcher</summary>
	/// <param name="key">key Subscribed to</param>
	/// <param name="sub">sub Method to be called</param>
	void UnsubscribeDispatcher(string key, Action<GameObject> sub) {
		GlobalDispatcher.Unsubscribe(key, sub);
	}

	/// <summary>Call Ubsubscribe in the Event Dispatcher</summary>
	/// <param name="key">key Subscribed to</param>
	/// <param name="sub">sub Method to be called</param>
	void UnsubscribeBoolDispatcher(string key, Func<GameObject, bool> sub) {
		GlobalDispatcher.UnsubscribeBool(key, sub);
	}

	/// <summary>Unsubscribe all Pubs Subs When Object is Disabled.</summary>
	void OnDisable() {

		//Clear all Action Events
		 foreach(KeyValuePair<string, Action<GameObject>> pair in actionEvents) {
		 	UnsubscribeDispatcher(pair.Key, pair.Value);
		 }

		 actionEvents.Clear();

		 //Clear all Func Events
		 foreach(KeyValuePair<string, Func<GameObject, bool>> pair in funcEvents) {
		 	UnsubscribeBoolDispatcher(pair.Key, pair.Value);
		 }

		 funcEvents.Clear();

		 Debug.Log("Cleared Event Emitter");
	}
}
