using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FightSceneManager : MonoBehaviour {

	public RectTransform player1HP;
	public RectTransform player2HP;

	public float player1Percent = 1;
	public float player2Percent = 1;

	public string player1Name = "test1";
	public string player2Name = "test2";

	public Text player1UIName;
	public Text player2UIName;

	public Text winner;
	public string playerWin = "test";

	public Button restartButton;

	public bool gameFinished = false;

	public Animator gameOverBtn;


	// Use this for initialization
	void Start () {
		player1UIName.text = player1Name;
		player2UIName.text = player2Name;

		restartButton.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		player1HP.pivot.Set ( 1/player1Percent, 0.5f);
		player2HP.pivot.Set ( 1/player2Percent, 0.5f);

		if (gameFinished) {
			gameOverBtn.SetTrigger("play");
			winner.text = playerWin + " WINS!!!";
			restartButton.gameObject.SetActive(true);
		}
	}

	public void RestartGame()
	{
		Application.LoadLevel ("MainMenu");
	}
}
