using UnityEngine;
using System.Collections;

public class ComponentExample : Grunt {

	// Use this for initialization
	void Start () {
		GlobalPublish("ComponentStarted", gameObject);
		GlobalPublishBool("ComponentStarted", gameObject);
		Subscribe("Test", Test);
		Publish("Test", gameObject);
	}
	
	void Test(GameObject g) {
		Debug.Log("We worked in local dispatcher!");
	}


	
}
