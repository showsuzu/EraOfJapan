using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour {

	public BaseButton button;	// 処理するゲームオブジェクトをUnity のInspectorで指定する
								// ゲームオブジェクトは、BaseButtonクラスを継承しOnClickをオーバーライドしている必要がある

	public void OnClick(Text inputKey){
		if (button == null)
		{
			// 処理するオブジェクトがなければ何もできない
			throw new System.Exception("Button instance is null!!");
		}
		// 自身のオブジェクト名を渡す
		// button.OnClickKey(this.gameObject.name);
		// オブジェクト名ではなく、ボタンにパラメータを付与して入力するようにする
		button.OnClickKey(inputKey);
	}

	protected virtual void OnClickKey(Text Key)
	{
		// 呼ばれることはない
		Debug.Log("Base Button");
	}
}
