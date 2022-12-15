using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private const int COIN_SCORE_AMOUNT = 5;

    public GameObject gamePlaypanel;
    public GameObject levelPanel;
    public GameObject paueTestPanel;

    // supposed to be on the OnClickEvents script 
    public GameObject greetingMesPanel;
    public GameObject startPanel;
    public bool isMessageActive;


    public static GameManager Instance { set; get; }

    public bool IsDead { set; get; }

    public bool isGamestarted = false;
    private PlayerControllerII player;
    private OnClickEvents message;
    private Animator playerAnim;

    // UI and UI Fields
    public Text scoreText, coinText, modifierText;
    private float score, coinScore, modifierScore;
    private int lastScore;

    //DeATHmENU 
    //public Animator deathMenuAnim;
    public Text deadscoreText, deadcoinText;

    private void Awake()
    {
        // FindObjectOfType<AudioManager>().Play("Music");
        startPanel.SetActive(true);
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
        // supposed to be on the OnClickEvents script 
        isMessageActive = false;

        // animator
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MobileInput.Instance.Tap && !isGamestarted && isMessageActive)
        {

            /* if(EventSystem.current.IsPointerOverGameObject())
                 return;
 */
            isGamestarted = true;
            gamePlaypanel.SetActive(true);
            greetingMesPanel.SetActive(false);

            player.StartRunning();
            FindObjectOfType<ScenerySpawner>().IsScrolling = true;

        }

        if (isGamestarted && !IsDead)
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

    public void GetCoin()
    {
        coinScore++;
        coinText.text = coinScore.ToString("0");
        score += COIN_SCORE_AMOUNT;
        scoreText.text = score.ToString("0");
    }

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }

    public void OnPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        levelPanel.SetActive(false);
        gamePlaypanel.SetActive(true);
        paueTestPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnDeath()
    {
        IsDead = true;
        FindObjectOfType<ScenerySpawner>().IsScrolling = false;
        // pause game 
        Time.timeScale = 0;
        deadscoreText.text = score.ToString("0");
        deadcoinText.text = coinScore.ToString("0");
        levelPanel.SetActive(true);
        gamePlaypanel.SetActive(false);

    }

    // supposed to be on the OnClickEvents script 
    public void Message()
    {
        greetingMesPanel.SetActive(true);
        startPanel.SetActive(false);
        isMessageActive = true;
    }
}