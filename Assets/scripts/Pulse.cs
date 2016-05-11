using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pulse : MonoBehaviour {

    Text text;
    bool up;
    float alpha = 1;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (up)
            alpha += Time.deltaTime;
        else
            alpha -= Time.deltaTime;
        
        if (alpha >= 1 || alpha <= 0.1f)
        {
            alpha = Mathf.Clamp(alpha, 0.1f, 1);
            up = !up;
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
	}
}
