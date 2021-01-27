using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.SceneManagement;

public class GameController : DefaultTrackableEventHandler
{
    public static GameController instance;
    public GameObject startPanel;
    public int playerScore = 0;
    public Text scoreCount;
    public Text livesCount;
    public int round = 0;
    //public GameObject roundText;
    public Text roundTextGoal;
    public Text roundNumber;
    public int shots = 3;
    private int lives = 2;
    public GameObject[] shells;
    public Text gameOverScore;
    
    //Gameobjects to hide or show
    public GameObject UIscoreText;
    public GameObject UIlivesText;
    public GameObject UIcrossHair;
    public GameObject UIfireButton;
    public GameObject UIdog;
    public GameObject UIroundText;
    public GameObject UIgameOverPanel;
    public GameObject UIgun;
    AudioSource audio;
    public AudioClip[] clips;

    //Rules
    private int targetScore = 1;
    public int roundScore = 0;
    private int scoreIncrease= 1;
    private bool playerStart = false;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        UIroundText.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerScore = int.Parse(scoreCount.text);
        showStartPanel();
        audio = GetComponent<AudioSource>();
        livesCount.text = lives.ToString();
    }

    private void playSoundFX(int sound)
    {
        audio.clip = clips[sound];
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(UIroundText.ActiveSelf)
        {
            Debug.Log(print("Is Active!!!!!!"));
        }*/
        if(DefaultTrackableEventHandler.trueFalse==true)
            {
                hideStartPanel();
                showHiddenItems();
                if(playerStart==false)
                    {
                        StartCoroutine(playRound());
                    }
                playerStart = true;
            }
            else
            {
                showStartPanel();
                hideHiddenItems();
            }
            if(roundScore == targetScore)
            {
                //play cheer sound, start new rouund
                playSoundFX(0);
                StartCoroutine(newRound());

                //reset the roundScore to 0 target score increases by 2
                roundScore = 0;
                targetScore = targetScore + scoreIncrease;
            }
            if(shots == 0)
            {
                shells[0].SetActive(false);
                StartCoroutine(loseLife());
                shots = 3;
            }

            scoreCount.text = playerScore.ToString();
        }

    public void showHiddenItems()
    {
        UIscoreText.SetActive(true);
        UIlivesText.SetActive(true);
        UIcrossHair.SetActive(true);
        UIfireButton.SetActive(true);
        UIdog.SetActive(true);
        //UIroundText.SetActive(true);
        showShells();
        UIgun.SetActive(true);
    }

    public void hideHiddenItems()
    {
        UIscoreText.SetActive(false);
        UIlivesText.SetActive(false);
        UIcrossHair.SetActive(false);
        UIfireButton.SetActive(false);
        //UIdog.SetActive(false);
        //UIroundText.SetActive(false);
        UIgun.SetActive(false);
    }

    public IEnumerator playRound()
    {
        yield return new WaitForSeconds(1f);
        roundTextGoal.text = "Shoot "+targetScore+" Ducks!";
        UIroundText.SetActive(true);
        playSoundFX(0);
        StartCoroutine(hideRound());
    }
    private IEnumerator hideRound()
    {
        yield return new WaitForSeconds(3f);
        UIroundText.SetActive(false);
    }

    //Comments todo
    private IEnumerator newRound()
    {
        yield return new WaitForSeconds(3);
        ++round;
        UIroundText.SetActive(true);
        roundNumber.text = round.ToString();
        roundTextGoal.text = "Shoot "+targetScore+" Ducks!";
        roundNumber.text = round.ToString();
        StartCoroutine(hideRound());
    }

    //Show the start panel
    void showStartPanel()
    {
        startPanel.SetActive(true);
    }

    //Hide the start panel
    void hideStartPanel()
    {
        startPanel.SetActive(false);
    }

    //When called lives will reach and remain at 0, disable the fire button set the gameover panel and players score
    private IEnumerator loseLife()
    {
        
        lives--;
        if(lives == 0)
        {
            UIfireButton.SetActive(false);
            playSoundFX(1);
            UIgameOverPanel.SetActive(true);
            gameOverScore.text = playerScore.ToString();
            lives = 0;
        }
        else
        {
            UIfireButton.SetActive(true);
            playSoundFX(2);
            UIdog.SetActive(false);
            yield return new WaitForSeconds(3);
            UIdog.SetActive(false);
            UIfireButton.SetActive(true);
            shots = 3;


        }
        yield return new WaitForSeconds(0);
        livesCount.text = lives.ToString();
    }

    public void showShells()
    {
        if(shots == 3)
        {
            shells[0].SetActive(true);
            shells[1].SetActive(true);
            shells[2].SetActive(true);
        }
        else if(shots == 2)
        {
            shells[0].SetActive(true);
            shells[1].SetActive(true);
            shells[2].SetActive(false);
        }
        else if(shots == 1)
        {
            shells[0].SetActive(true);
            shells[1].SetActive(false);
            shells[2].SetActive(false);
        }
    }

    //Quits the Game
    public void Quit()
    {
        //Application.Quit();
        SceneManager.LoadScene("NewIntro");
    }

    public void Restart()
    {
        hideHiddenItems();
        lives = 2;
        livesCount.text = lives.ToString();
        playerScore = 0;
        scoreCount.text = playerScore.ToString();
        targetScore = 1;
        roundScore = 0;
        gameOverScore.text = "0";
        round = 1;
        roundNumber.text = round.ToString();
        UIgameOverPanel.SetActive(false);
        StartCoroutine(playRound());
    }
}
