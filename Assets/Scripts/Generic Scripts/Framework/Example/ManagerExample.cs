using UnityEngine;

public class ManagerExample : Manager {

	void Awake() {
		// VoidAction events;
		// events = ComponentHandler;
		GlobalSubscribe("ComponentStarted", ComponentHandler);	
		GlobalSubscribeBool("ComponentStarted", SecondTest);
	}

	void ComponentHandler(GameObject g) {
		Debug.Log("Hello");
	}

	bool SecondTest(GameObject g) {
		Debug.Log("Second One");
		return true;
	}

	void Update() {
		if(Input.GetKeyDown("space")) {
			GlobalUnsubscribe("ComponentStarted", ComponentHandler);
			GlobalUnsubscribeBool("ComponentStarted", SecondTest);
		}
	}
}