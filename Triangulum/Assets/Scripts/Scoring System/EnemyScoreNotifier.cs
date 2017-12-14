using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScoreNotifier : MonoBehaviour {

    private float displayTime = 1;
    private float moveDistance = 5;

    private Text scoreText;

	void OnEnable ()
    {
        scoreText = transform.GetChild(0).GetComponent<Text>();
        StartCoroutine(moveAndFade());
	}

    public void setScoreValue(int score)
    {
        scoreText.text = "+" + score.ToString();
    }

    IEnumerator moveAndFade()
    {
        Color color = scoreText.color;
        color.a = 1;
        scoreText.color = color;

        float delayTime = displayTime / 100;
        float moveDelta = moveDistance / 100;

        for (float i = 100; i > 0; i--)
        {
            color.a = i / 100;
            scoreText.color = color;
            Vector3 newPos = transform.localPosition;
            newPos.y += moveDelta;
            transform.localPosition = newPos;
            yield return new WaitForSeconds(delayTime);
        }

        gameObject.SetActive(false);
    }
}
