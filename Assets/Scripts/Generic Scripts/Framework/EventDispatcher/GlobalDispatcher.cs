using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Used to connect publishers and subscriptions in a game.  All thats needed for subscribers is the key their waiting for publishing from,
/// as well as the handler they want to run when a publish is triggered.  A Publishers will need a key for what subscibers should subscribe to, and a gameObject that 
/// is associated with the publish.
/// </summary>
public static class GlobalDispatcher {
	private static EventDispatcher globalDispatcher = new EventDispatcher();

	/// <summary>
	/// Publish the specified key, and gameobject  to subscribed handlers, and then pass the return value back.
	/// </summary>
	/// <param name="key">Key to check subscriptions for</param>
	/// <param name="g">The gameobject component to pass to the subscribers.</param>
	/// <param name="returnValue">Return value of the boolean methods.</param>
	public static bool PublishBool(string key, GameObject g) {
		return globalDispatcher.PublishBool(key, g);
	}

	/// <summary>
	/// Publish the specified key and gameobject to subscribed handlers.
	/// </summary>
	/// <param name="key">Key to publish</param>
	/// <param name="g">The gameobject component to pass to the event handlers.</param>
	public static void Publish(string key, GameObject g) {
		globalDispatcher.Publish(key, g);
	}


	/// <summary>
	/// Subscribe the specified key with the given method.
	/// </summary>
	/// <param name="key">Key to subscribe to</param>
	/// <param name="d">The void action to run when published</param>
	public static void Subscribe(string key, Action<GameObject> d) {
		globalDispatcher.Subscribe(key, d);
	}

	/// <summary>
	/// Subscribe the specified key with the given method
	/// </summary>
	/// <param name="key">Key to subscribe to</param>
	/// <param name="d">boolean action to run on publish</param>
	public static void SubscribeBool(string key, Func<GameObject, bool> d) {
		globalDispatcher.SubscribeBool(key, d);
	}

	/// <summary>
	/// Get the current number of keys in the VoidSubscriptions. This is used primarily for Debugging
	/// </summary>
	/// <value>Current Number of keys in the Void Subscriptions</value>
	public static int VoidSize {
		get { return globalDispatcher.VoidSize; }
	}

	/// <summary>
	/// Get the current number of keys in the BoolSubscriptions. This is used primarily for Debugging
	/// </summary>
	/// <value>Current Number of keys in the Bool Subscriptions</value>
	public static int BoolSize {
		get{ return globalDispatcher.BoolSize; }
	}

	/// <summary>
	/// Unsubscribe an Action Event from the Global Pub Sub. Check if this delegate
	/// exists. If it does, remove the method subscription. Then check if there
	/// are any methods left to this delegate.
	/// </summary>
	/// <param name="key">key Subscribed to</param>
	/// <param name="d">d Method to check for</param>
	public static void Unsubscribe(string key, Action<GameObject> d) {
		globalDispatcher.Unsubscribe(key, d);
	}

	/// <summary>
	/// Unsubscribe a Func Event from the Global Pub Sub. Check if this delegate
	/// exists. If it does, remove the method subscription. Then check if there
	/// are any methods left to this delegate.
	/// </summary>
	/// <param name="key">key Subscribed to</param>
	/// <param name="d">d Method to check for</param>
	public static void UnsubscribeBool(String key, Func<GameObject, bool> d) {
		globalDispatcher.UnsubscribeBool(key, d);
	}

}


