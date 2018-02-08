using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AppUtility
{
	public class ModalButtonScript : MonoBehaviour {

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
}
