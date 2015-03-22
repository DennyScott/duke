using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown("space")) {
			Debug.Log("Void Size " + GlobalDispatcher.VoidSize);
			Debug.Log("Bool Size " + GlobalDispatcher.BoolSize);
		}
	}
}
