using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyMain : BaseButton {

	protected override void OnClickKey(Text inputKey)
	{
		// 渡された値で処理を分岐
		int num;
		if (int.TryParse (inputKey.text, out num)) {
			// 数値入力
			// 念のための上下限チェック
			if (num >= 0 && num <= 9) {
				this.GetComponent<ProcMain> ().NumberProc (inputKey);
			} else {
				throw new System.Exception ("An impossible number was entered!!");
			}
		} else {
			// 数値以外のボタン入力
			this.GetComponent<ProcMain>().FuncProc (inputKey);
		}
	}
}
