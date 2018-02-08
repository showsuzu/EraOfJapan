using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AppUtility
{
	public class ModalUtility : MonoBehaviour {

		public static string PLACE = "Prefabs/Utility/";
		public static string ModalPrefab = "1BTNModalWindow";

		public enum MODALTYPE{
			EXIT_MODAL,
			BTN1_MODAL,
			BTN2_MODAL
		};

		static string[] ModalTBL = {
			"ExitConfirmationWindow",
			"1BTNModalWindow",
			"1BTNModalWindow"
		};

		// 与えられた表示内容でモーダルウィンドウを表示する
		public static void ModalDisp(string title, string explain, string btn, int type_num){
			string prefabStr = ModalTBL [type_num];
			if(GameObject.Find(prefabStr) == null){
				GameObject prefab = (GameObject)Resources.Load (PLACE + prefabStr);
				GameObject obj = GameObject.Instantiate(prefab);
				obj.name = prefabStr;
				// 表示文言設定
				GameObject.Find ("ModalTitle").GetComponent<Text> ().text = title;
				GameObject.Find ("ModalExplain").GetComponent<Text> ().text = explain;
				if(type_num == (int)MODALTYPE.BTN1_MODAL){
					Text txtObj = GameObject.Find ("CloseButton").GetComponentInChildren<Text> ();
					txtObj.text = btn;
				}
			}
		}
	}
}
