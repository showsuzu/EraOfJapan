using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalScript : MonoBehaviour {

	void Awake(){
		transform.SetParent (GameObject.Find ("MainCanvas").transform, false);
		transform.localScale = Vector2.one;
		transform.localPosition = Vector2.zero;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
