using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcMain : MonoBehaviour {
	private string inputGenDisp = "/MainCanvas/FormuraPanel/Gen";
	private string inputNumDisp = "/MainCanvas/FormuraPanel/Number";
	private string genResultDisp = "/MainCanvas/ResultPanel/Gen Result";
	private string eraResultDisp = "/MainCanvas/ResultPanel/Era Result";
	private string etoResultDisp = "/MainCanvas/ResultPanel/Eto Result";
	private string ageResultDisp = "/MainCanvas/ResultPanel/Age Result";

	GameObject inputGenDispObj, inputNumDispObj;
	Text inputGen, inputNum, genResult, eraResult, etoResult, ageResult; 

	private int ADYear;

	// Use this for initialization
	void Start () {
		//開始時に表示エリアを取得する
		GameObject obj = GameObject.Find (inputGenDisp);
		inputGen = obj.GetComponent<Text> ();
		obj = GameObject.Find (inputNumDisp);
		inputNum = obj.GetComponent<Text> ();
		obj = GameObject.Find (genResultDisp);
		genResult = obj.GetComponent<Text> ();
		obj = GameObject.Find (eraResultDisp);
		eraResult = obj.GetComponent<Text> ();
		obj = GameObject.Find (etoResultDisp);
		etoResult = obj.GetComponent<Text> ();
		obj = GameObject.Find (ageResultDisp);
		ageResult = obj.GetComponent<Text> ();

		// クリアしておく
		initialDisp();
	}

	void initialDisp() {
		// クリアしておく
		inputGen.text  = "西暦";	//デフォルトは西暦
		inputNum.text  = "";
		genResult.text = "";
		eraResult.text = "";
		etoResult.text = "";
		ageResult.text = "";
		ADYear = 0;
	}

	// Update is called once per frame
	void Update () {
		
	}

	// 数字キー入力時の処理
	public void NumberProc(Text num){
		int digitLimit = 4;	// 西暦の時の桁数
		if (inputGen.text != "西暦") {
			digitLimit = 2;
		}
		// 桁数を確認して余裕があるなら表示に加える。桁がいっぱいの時は入力を無視する
		if (inputNum.text.Length < digitLimit) {
			// 表示に加える
			inputNum.text += num.text;
		} else {
			Debug.Log ("Digit Overflow");
		}
	}

	// 数字以外のキー入力時の処理
	public void FuncProc(Text key){
		if (key.text == "AC") {
			// 入出力表示をクリアする
			initialDisp ();
		} else if(key.text == "WiKi") {
			if (ADYear != 0) {
				string url = "https://ja.wikipedia.org/wiki/" + ADYear + "%E5%B9%B4";
				// 外部ブラウザを使ってWiKiを表示する
				Application.OpenURL(url);
			}
		} else if(key.text == "＝") {
			// 変換する
			CalcAndDispProc ();
		} else {
			//上記以外は西暦か元号キー
			genCheckAndDisp(key);
		}
	}

	// 現在の選択が西暦か否かを返す
	//  西暦の時:true
	bool isAD(){
		return inputGen.text == "西暦";
	}

	// 西暦あるいは元号キー押下時の処理
	void genCheckAndDisp(Text key){
		if (key.text != inputGen.text) {
			// 現在の表示と異なる場合はクリアして表示し直す
			initialDisp ();
			inputGen.text = key.text;
		}
	}

	// 「＝」キーを押下された時の処理。変換処理を行い、結果を表示する
	void CalcAndDispProc(){
		int num, ret = 0;
		// ゼロの時、何も入っていない時は何もしない
		if(int.TryParse(inputNum.text, out num)) {
			if(num == 0){
				return;
			}
		} else {
			return;
		}
		if (isAD ()) {
			// 西暦から和暦へ
			ADtoERA (num);
		} else {
			// 和暦から西暦へ
			ret = ERAtoAD (num);
			resultADDisp (ret);
		}
	}

	// 西暦から和暦への変換を行う
	void ADtoERA(int num){
		string era, gen;
		int year;
		ADYear = num;
		// 西暦から和暦への変換を行う
		if(num >= 1868){
			// 明治以降
			if (num >= 2019) {
				gen = "現代";
				era = "新規";
				year = num - 2018;
			} else if (num >= 1989) {
				gen = "現代";
				era = "平成";
				year = num - 1988;
			} else if (num >= 1926) {
				// 第二次世界大戦以前を近代、それ以降を現代として表示する
				if (num <= 1939) {
					gen = "近代";
				} else {
					gen = "現代";
				}
				era = "昭和";
				year = num - 1925;
			} else if (num >= 1912) {
				gen = "近代";
				era = "大正";
				year = num - 1911;
			} else {
				gen = "近代";
				era = "明治";
				year = num - 1867;
			}
		} else {
			// 明治以前
			int temp;
			// 未設定ルートがあるので、gen,era,yearを暫定値で初期化しておく
			gen = ProcStatics.periodsTbl [0, ProcStatics.ofsetGEN];
			era = ProcStatics.periodsTbl [0, ProcStatics.ofsetERA];
			year = 1;

			// テーブルを走査して入力した西暦とマッチする時代を検索する
			for (int i = 0; i < ProcStatics.periodsTbl.Length; i++) {
				if(int.TryParse(ProcStatics.periodsTbl[i, ProcStatics.ofsetEOP], out temp)){
					if(num < temp){
						//時代名などをゴニョゴニョする
						gen = ProcStatics.periodsTbl [i, ProcStatics.ofsetGEN];
						era = ProcStatics.periodsTbl [i, ProcStatics.ofsetERA];
						if (int.TryParse (ProcStatics.periodsTbl [i, ProcStatics.ofsetSOP], out temp)) {
							// 正常に数値化できたら計算する
							year = num - temp + 1;
						} else {
							// 正常に数値化できなかったらとりあえず、99年にする
							year = 99;
						}
						// for文から抜ける
						break;
					}
				} else {
					throw new System.Exception ("Sorry...  our Table error");
				}
			}
		}
		resultDisp (year, gen, era);
		etoAndAgeDisp (ADYear);
	}

	// 和暦から西暦への変換を行う
	int ERAtoAD(int num){
		// 和暦から西暦への変換を行う
		switch (inputGen.text) {
			case "明治":
				ADYear = 1900 + num - 33;
				break;
			case "大正":
				ADYear = 1900 + num + 11;
				break;
			case "昭和":
				ADYear = 1900 + num + 25;
				break;
			case "平成":
				ADYear = 2000 + num - 12;
				break;
			case "新規":
				ADYear = 2000 + num + 18;
				break;
			default:
				throw new System.Exception ("An impossible number was entered!!");
		}
		return ADYear;
	}

	void resultADDisp (int num){
		// genResult, eraResult を決定して入力する
		string gen, era;
		if (num == 0) {
			return;
		}
		// 和暦から西暦への変換
		// 第二次世界大戦以前を近代、それ以降を現代として表示する
		if (num <= 1939) {
			gen = "近代";
		} else {
			gen = "現代";
		}
		era = "西暦";
		resultDisp (num, gen, era);
		etoAndAgeDisp (ADYear);
	}

	void resultDisp(int num, string gen, string era){
		genResult.text = gen;
		eraResult.text = era + "  " + num.ToString () + " 年";
	}

	void etoAndAgeDisp(int year){
		// 取得する値: 年
		int now = System.DateTime.Now.Year;
		int age = now - year;
		int ofset = year % 12;
		etoResult.text = ProcStatics.etoTable [ofset];
		if (age >= 0) {
			ageResult.text = "満" + age.ToString () + "  歳";
		} else {
			ageResult.text = "あと  " + Mathf.Abs((float)age).ToString() + "  年";
		}
	}
}
