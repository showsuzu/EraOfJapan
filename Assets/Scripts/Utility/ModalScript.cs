using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AppUtility
{
	public class ModalScript : MonoBehaviour {

		void Awake(){
			transform.SetParent (GameObject.Find ("MainCanvas").transform, false);
			transform.localScale = Vector2.one;
			transform.localPosition = Vector2.zero;
		}
	}
}
