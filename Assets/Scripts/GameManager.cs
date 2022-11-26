using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const int COIN_SCORE_AMOUNT = 5;

    public static GameManager Instance { set; get; }

    private bool isGamestarted = false;
    private PlayerControllerII player;

    // UI and UI Fields
    public Text scoreText, coinText, modifierText;
    private float score, coinScore, modifierScore;
    private int lastScore;

    private void Awake()
    {
        Instance = this;
        modifierScore = 1.0f;
        scoreText.text = score.ToString("0");
        modifierText.text = "x" + modifierScore.ToString("0.0");
        coinText.text = coinScore.ToString("0");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerII>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MobileInput.Instance.Tap && !isGamestarted)
        {
            isGamestarted = true;
            player.StartRunning();
        }

        if (isGamestarted)
        {
            // Read score and add up
            score += (Time.deltaTime * modifierScore);

            if (lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = score.ToString("0");
            }
        }
    }

    private void GetCoin()
    {
        coinScore += COIN_SCORE_AMOUNT;
        scoreText.text = score.ToString("0");
    }

    

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }
}
