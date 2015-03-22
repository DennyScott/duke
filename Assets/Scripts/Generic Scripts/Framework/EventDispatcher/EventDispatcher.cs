using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Used to connect publishers and subscriptions in a game.  All thats needed for subscribers is the key their waiting for publishing from,
/// as well as the handler they want to run when a publish is triggered.  A Publishers will need a key for what subscibers should subscribe to, and a gameObject that 
/// is associated with the publish.
/// </summary>
public class EventDispatcher {
	private Dictionary<string, Action<GameObject>> voidSubscriptions = new Dictionary<string, Action<GameObject>>();  //Used for the void actions
	private Dictionary<string, Func<GameObject, bool>> boolSubscriptions = new Dictionary<string, Func<GameObject, bool>>();  //Used for the boolean actions

	/// <summary>
	/// Publish the specified key, and gameobject  to subscribed handlers, and then pass the return value back.
	/// </summary>
	/// <param name="key">Key to check subscriptions for</param>
	/// <param name="g">The gameobject component to pass to the subscribers.</param>
	/// <param name="returnValue">Return value of the boolean methods.</param>
	public bool PublishBool(string key, GameObject g) {
		//make true so that is there is no subscription, its automatically true
		bool returnValue = true;
		//Check to make sure the key exists
		if(boolSubscriptions.ContainsKey(key)) {
			//Check to see if there is a subscription by the given key
			if (boolSubscriptions[key] != null) {
				//if there is a subscription, cycle through each boolAction in the list
				returnValue = DoesBoolExist(boolSubscriptions[key], g);
			}
		}
		return returnValue;
	}

	/// <summary>
	/// Check each of the booleans in a BoolAction. If any of the returns are false,
	/// return false. If none of them are false, we can return true.
	/// </summary>
	/// <param name="actions">Event storage of methods.</param>
	private bool DoesBoolExist(Func<GameObject, bool> actions, GameObject g) {
		foreach(Func<GameObject, bool> bAction in actions.GetInvocationList()) {
			//If the return value of the action is ever false, return value must be false
			if (bAction(g) == false) {
				return false;
			}
		}
		return true;
	}

	/// <summary>
	/// Publish the specified key and gameobject to subscribed handlers.
	/// </summary>
	/// <param name="key">Key to publish</param>
	/// <param name="g">The gameobject component to pass to the event handlers.</param>
	public void Publish(string key, GameObject g) {
		//If the subscription exists
		if(voidSubscriptions.ContainsKey(key)) {
			//Make sure the delegate is not null
			if(voidSubscriptions[key] != null) {
				//If not null, call all the associated methods
				voidSubscriptions[key](g);
			}
		}
	}


	/// <summary>
	/// Subscribe the specified key with the given method.
	/// </summary>
	/// <param name="key">Key to subscribe to</param>
	/// <param name="d">The void action to run when published</param>
	public void Subscribe(string key, Action<GameObject> d) {
		//If the subscription already contains the key, 
		//add this delegate to the existing delegate under the given key
		if(voidSubscriptions.ContainsKey(key)) {
			//Add delegate to existing delegate
			voidSubscriptions[key] += d;
		} else {
			//Create a new entry in the subscription dictonary
			voidSubscriptions.Add(key, d);
		}
	}

	/// <summary>
	/// Subscribe the specified key with the given method
	/// </summary>
	/// <param name="key">Key to subscribe to</param>
	/// <param name="d">boolean action to run on publish</param>
	public void SubscribeBool(string key, Func<GameObject, bool> d) {
		//If the subscription already contains the key, 
		//add this delegate to the existing delegate under the given key
		if(boolSubscriptions.ContainsKey(key)) {
			//Add delegate to existing delegate
			boolSubscriptions[key] += d;
		} else {
			//Create a new entry in the subscription dictonary
			boolSubscriptions.Add(key, d);
		}
	}

	/// <summary>
	/// Get the current number of keys in the VoidSubscriptions. This is used primarily for Debugging
	/// </summary>
	/// <value>Current Number of keys in the Void Subscriptions</value>
	public int VoidSize {
		get { return voidSubscriptions.Count;}
	}

	/// <summary>
	/// Get the current number of keys in the BoolSubscriptions. This is used primarily for Debugging
	/// </summary>
	/// <value>Current Number of keys in the Bool Subscriptions</value>
	public int BoolSize {
		get { return boolSubscriptions.Count; }
	}

	/// <summary>
	/// Unsubscribe an Action Event from the Global Pub Sub. Check if this delegate
	/// exists. If it does, remove the method subscription. Then check if there
	/// are any methods left to this delegate.
	/// </summary>
	/// <param name="key">key Subscribed to</param>
	/// <param name="d">d Method to check for</param>
	public void Unsubscribe(string key, Action<GameObject> d) {
		if(voidSubscriptions.ContainsKey(key)) {
			if(voidSubscriptions[key] != null){
				voidSubscriptions[key] -= d;
				ClearActionCheck(key); //Remove delegate if no methods inside
			}
		}
	}

	/// <summary>
	/// Unsubscribe a Func Event from the Global Pub Sub. Check if this delegate
	/// exists. If it does, remove the method subscription. Then check if there
	/// are any methods left to this delegate.
	/// </summary>
	/// <param name="key">key Subscribed to</param>
	/// <param name="d">d Method to check for</param>
	public void UnsubscribeBool(String key, Func<GameObject, bool> d) {
		if(boolSubscriptions.ContainsKey(key)) {
			if(boolSubscriptions != null) {
				boolSubscriptions[key] -= d;
				ClearFuncCheck(key); //Remove Delegate if no methods inside
			}
		}
	}

	public void ClearActions() {
		List<string> keys = new List<string>(voidSubscriptions.Keys);
		foreach(var key in keys) {
			if(voidSubscriptions[key] != null) {
				voidSubscriptions[key] = null;
			}
		}

		voidSubscriptions.Clear();
	}

	public void ClearFuncs() {
		List<string> keys = new List<string>(boolSubscriptions.Keys);
		foreach(var key in keys) {
			if(boolSubscriptions != null) {
				boolSubscriptions[key] = null;
			}
		}

		boolSubscriptions.Clear();
	}

	public void ClearAll() {
		ClearActions();
		ClearFuncs();
	}

	/// <summary>
	/// Search the boolSubscriptions for a key. If the key exists, but the 
	/// delegate has no method attached, remove the delegate and key.
	/// </summary>
	/// <param name="key">key Key to search with</param>
	void ClearFuncCheck(string key) {
		if (boolSubscriptions[key] == null) {
			boolSubscriptions.Remove(key);
		}
	}

	/// <summary>
	/// Search the voidSubscriptions for a key. If the key exists, but the 
	/// delegate has no method attached, remove the delegate and key.
	/// </summary>
	/// <param name="key">key Key to search with</param>
	void ClearActionCheck(string key) {
		if (voidSubscriptions[key] == null) {
			voidSubscriptions.Remove(key);
		}
	}

}


