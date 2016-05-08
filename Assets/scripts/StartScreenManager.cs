using UnityEngine;
using System.Collections;

public class StartScreenManager : MonoBehaviour {

	public void StartGame() {
		Application.LoadLevel("main");
	}

	void Update()
	{
		if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();
	}
}
