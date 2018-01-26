using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnClickOK(){
		this.CloseProc ();
	}

	public void CloseProc(){
		// 自分自身を削除する
		string name = transform.name;
		GameObject.Destroy (gameObject.transform.parent.gameObject);
	}

	public void OnClickExit(){
//		Application.runInBackground = false;
		Application.Quit();
	}

	public void OnClickCancel(){
		// 自分自身を削除する
		string name = transform.name;
		GameObject.Destroy (gameObject.transform.parent.gameObject);
	}
}
