using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	public float worldScrollingSpeed;

	public Text scoreText;

	private float score;

	public GameObject obstacle;

	//public float obstacleSpawnRate;
	//public float maxObstacleSpawnHeight, minObstacleSpawnHeight;
	//public float obstacleSpawnPositionX;
	public bool inGame;
	public GameObject resetButton;
	private int coins;
	public Text coinsText;

	public bool isImmortal;
	public float immortalityTime;
	public float immortalitySpeedBoost;

	public bool magnetActive;
	public float magnetSpeed;
	public float magnetDistance;
	public float magnetTime;

	public void MagnetCollected() {
		if(magnetActive) {CancelInvoke("CancelMagnet");}

		magnetActive = true;
		Invoke("CancelMagnet", immortalityTime);
	}

	private void CancelMagnet() {
		magnetActive = false;
		
	}

	public void ImmortalityCollected() {
		if(isImmortal) {CancelInvoke("CancelImmortality");}

		isImmortal = true;
		worldScrollingSpeed += immortalitySpeedBoost;
		Invoke("CancelImmortality", immortalityTime);
	}

	private void CancelImmortality() {
		worldScrollingSpeed -= immortalitySpeedBoost;
		isImmortal = false;
		
	}
		

	// Use this for initialization
	void Start () {
		instance = this;
		if(PlayerPrefs.HasKey("Coins")) {
			coins = PlayerPrefs.GetInt("Coins");
		}
		else {
			coins = 0;
			PlayerPrefs.SetInt("Coins", 0);
		}
		InitializeGame();
		UpdateScreenScore();
	}
	
	private void FixedUpdate() {
		if(!GameManager.instance.inGame) {
			return;
		} else {
			score += worldScrollingSpeed;
			UpdateScreenScore();
		}		
	}

	void UpdateScreenScore() {
		scoreText.text = score.ToString("0");
		coinsText.text = coins.ToString();
	}

	/*void SpawnObstacle() {
		var spawnPosition = new Vector3(obstacleSpawnPositionX, Random.Range(minObstacleSpawnHeight, maxObstacleSpawnHeight), 0f);
		Instantiate(obstacle, spawnPosition, Quaternion.identity);
	}*/

	void InitializeGame() {
		inGame = true;
		//InvokeRepeating("SpawnObstacle", obstacleSpawnRate, obstacleSpawnRate);
		resetButton.SetActive(false);
	}

	public void GameOver() {
		inGame = false;
		CancelInvoke("SpawnObstacle");
		resetButton.SetActive(true);
	}

	public void RestartGame() {
		SceneManager.LoadScene(0);
	}

	public void CoinCollected(int value = 1) {
		coins += value;
		PlayerPrefs.SetInt("Coins", coins);
		PlayerPrefs.SetInt("HighscoreValue", (int)score);
		UpdateScreenScore();
	}

}
