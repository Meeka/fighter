using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FightSceneManager : MonoBehaviour {

	public Character player1;
	public Character player2;

    public RectTransform player1HP;
    public RectTransform player2HP;
    public RectTransform player1HPDelay;
    public RectTransform player2HPDelay;

	public float player1Percent = 1;
	public float player2Percent = 1;
    public float player1DelayPercent = 1;
    public float player2DelayPercent = 1;
    int player1HitDelayCounter = 0;
    int player2HitDelayCounter = 0;

	public string player1Name = "test1";
	public string player2Name = "test2";

	public Text player1UIName;
	public Text player2UIName;

	public Text winner;
	public string playerWin = "test";

	public Button restartButton;

	public bool gameFinished = false;

	public Animator gameOverBtn;

	float timer;
	public Text timerText;

    public Color FullHealth;
    public Color Dead;

	// Use this for initialization
	void Start () {
		player1UIName.text = player1Name;
		player2UIName.text = player2Name;

		restartButton.gameObject.SetActive(false);

		timer = 90;
	}
	
	// Update is called once per frame
	void Update () {
		float currentPercent = Mathf.Clamp01(player1.HP / player1.startHP);
        if (currentPercent != player1Percent)
        {
            player1HitDelayCounter++;
            StartCoroutine("SetDelayPercentPlayer1", currentPercent);
        }
        player1Percent = currentPercent;
        player1HP.GetComponent<Image>().color = Color.Lerp(Dead, FullHealth, player1Percent);

        currentPercent = Mathf.Clamp01(player2.HP / player2.startHP);
        if (currentPercent != player2Percent)
        {
            player2HitDelayCounter++;
            StartCoroutine("SetDelayPercentPlayer2", currentPercent);
        }
        player2Percent = currentPercent;
        player2HP.GetComponent<Image>().color = Color.Lerp(Dead, FullHealth, player2Percent);


        player1HP.pivot = new Vector2(1 - player1Percent, 0.5f);
        if (player1HitDelayCounter == 0)
        {
            player1DelayPercent = Mathf.Max(player1DelayPercent - Time.deltaTime, player1Percent);
            player1HPDelay.pivot = new Vector2(1 - player1DelayPercent, 0.5f);
        }

        player2HP.pivot = new Vector2(1 - player2Percent, 0.5f);
        if (player2HitDelayCounter == 0)
        {
            player2DelayPercent = Mathf.Max(player2DelayPercent - Time.deltaTime, player2Percent);
            player2HPDelay.pivot = new Vector2(1 - player2DelayPercent, 0.5f);
        }

		if (player1Percent <= 0 || (timer <= 0 && player2Percent > player1Percent)) {
			gameOverBtn.SetTrigger ("play");
			winner.text = player2Name + " WINS!!!";
			restartButton.gameObject.SetActive (true);
		} else if (player2Percent <= 0 || (timer <= 0 && player2Percent < player1Percent)) {
			gameOverBtn.SetTrigger ("play");
			winner.text = player1Name + " WINS!!!";
			restartButton.gameObject.SetActive (true);

		} else if (timer <= 0 && player2Percent == player1Percent) {
			gameOverBtn.SetTrigger ("play");
			winner.text = "DRAW!!!";
			restartButton.gameObject.SetActive (true);
		}

		timer -= Time.deltaTime;

		timerText.text = Mathf.Ceil (Mathf.Max(0, timer)).ToString();

		if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();
	}

    IEnumerator SetDelayPercentPlayer1(float value)
    {
        yield return new WaitForSeconds(1);
        player1HitDelayCounter--;
    }
    IEnumerator SetDelayPercentPlayer2(float value)
    {
        yield return new WaitForSeconds(1);
        player2HitDelayCounter--;
    }

	public void RestartGame()
	{
		Application.LoadLevel ("MainMenu");
	}
}
