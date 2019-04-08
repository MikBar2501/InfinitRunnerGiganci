using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public Text highScoreText;
	public Text coinText;
	public Text soundText;

	// Use this for initialization
	void Start () {
		int hs = 0;
		if(PlayerPrefs.HasKey("HighscoreValue"))
		{
			hs = PlayerPrefs.GetInt("HighscoreValue");
		}
		highScoreText.text = hs.ToString();
		
		int coin = 0;
		if(PlayerPrefs.HasKey("Coins"))
		{
			coin = PlayerPrefs.GetInt("Coins");
		}
		coinText.text = coin.ToString();

		if(SoundManager.instance.GetMuted()) {
			soundText.text = "Turn On Sound";
		} else {
			soundText.text = "Turn Off Sound";
		}


	}
	
	public void PlayButton()
	{
		SceneManager.LoadScene("MainScene");
		SoundManager.instance.PlayOnceClick();
	}

	public void SoundButton() {
		SoundManager.instance.ToggleMuted();
		if(SoundManager.instance.GetMuted()) {
			soundText.text = "Turn On Sound";
		} else {
			soundText.text = "Turn Off Sound";
		}
		SoundManager.instance.PlayOnceClick();
	}
}
