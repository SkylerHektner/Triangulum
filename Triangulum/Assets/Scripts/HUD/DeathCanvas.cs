using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeathCanvas : MonoBehaviour {

    private int score;
    private int money;

    private Text scoreText;
    private Text moneyText;

    public float delayBeforeAnimation = 2f;
    public float transferAnimDuration = 1f;
    public int transfersPerDuration = 100;

    void Start()
    {
        // get the score and money at the end of the round
        score = ScoreManager.Instance.score;
        money = upgradeLoader.data.Player_TaxPayerDollars;

        // add the score the money right away so the player can skip early
        upgradeLoader.data.Player_TaxPayerDollars += score;
        upgradeLoader.Instance.SaveData();

        // Get the scoreText and moneyText
        scoreText = transform.Find("scoreText").gameObject.GetComponent<Text>();
        moneyText = transform.Find("moneyText").gameObject.GetComponent<Text>();

        // set score and money texts
        moneyText.text = "Tax Payer Dollars: " + money.ToString();
        scoreText.text = "Score: " + score.ToString();

        // start the animation co-routine
        StartCoroutine(moneyTransferAnim());
    }

    IEnumerator moneyTransferAnim()
    {
        yield return new WaitForSeconds(delayBeforeAnimation);

        float m = money;
        float s = score;
        float s_div = s;
        for (int i = 0; i < transfersPerDuration; i++)
        {
            m += s_div / transfersPerDuration;
            s -= s_div / transfersPerDuration;

            moneyText.text = "Tax Payer Dollars: " + Mathf.RoundToInt(m).ToString();
            scoreText.text = "Score: " + Mathf.RoundToInt(s).ToString();

            yield return new WaitForSeconds(transferAnimDuration / transfersPerDuration);
        }

        moneyText.text = "Tax Payer Dollars: " + upgradeLoader.data.Player_TaxPayerDollars.ToString();
        scoreText.text = "Score: 0";
    }

}
