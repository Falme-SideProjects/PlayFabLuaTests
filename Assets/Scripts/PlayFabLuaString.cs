using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using MoonSharp.Interpreter;
using PlayFab.ClientModels;

public class PlayFabLuaString : MonoBehaviour
{
	public TextMeshProUGUI text;

	string scriptCode = "";

	void Start()
    {
		PlayFabClientAPI.LoginWithCustomID(
			new LoginWithCustomIDRequest()
			{
				CustomId = "Player",
				CreateAccount = true
			}, r =>
			{
				Debug.Log(r.PlayFabId);

				PlayFabClientAPI.GetTitleData(
					new GetTitleDataRequest()
					{
						Keys = new List<string> { "Code" }
					}, res =>
					{
						scriptCode = res.Data["Code"];
						CallPlayFabRequest();
					}, err =>
					{
						Debug.Log(err.ErrorMessage);
					}
				);

			}, e =>
			{
				Debug.Log(e.ErrorMessage);

			}
		);

	}

	private void CallPlayFabRequest()
	{
		CallLuascript();
	}

	private void CallLuascript()
	{
		
		Script script = new Script();
		script.DoString(scriptCode);

		DynValue res = script.Call(script.Globals["ChangeNumber"], 6);

		text.text = res.Number.ToString();
	}

	/*public int ChangeNumber(int number)
	{
		return number * 2;
	}*/
    
}
