using UnityEngine;
using System.Collections;

public class FightSceneManager : MonoBehaviour {

	public RectTransform player1HP;
	public RectTransform player2HP;

	public float player1Percent = 1;
	public float player2Percent = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		player1HP.pivot.Set ( 1/player1Percent, 0.5f);
		player2HP.pivot.Set ( 1/player2Percent, 0.5f);
	}
}
