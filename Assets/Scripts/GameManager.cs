using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] int timeToEnd;
    bool gamePaused = false;
    bool endGame = false;
    bool win = false;
    public int points = 0;

    public int redKey = 0;
    public int greenKey = 0;
    public int goldKey = 0;

    AudioSource audioSource;
    public AudioClip resumeClip;
    public AudioClip pauseClip;
    public AudioClip winClip;
    public AudioClip loseClip;

    public Text timeText;
    public Text goldKeyText;
    public Text redKeyText;
    public Text greenKeyText;
    public Text crystalText;
    public Image snowFlake;
    public GameObject infoPanel;
    public Text pauseEnd;
    public Text reloadInfo;
    public Text useInfo;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        if (timeToEnd <= 0)
        {
            timeToEnd = 100;
        }

        snowFlake.enabled = false;
        timeText.text = timeToEnd.ToString();
        infoPanel.SetActive(false);
        pauseEnd.text = "Pause";
        reloadInfo.text = "";
        SetUseInfo("");

        audioSource = GetComponent<AudioSource>();

        Debug.Log("Time: " + timeToEnd + " s");
        InvokeRepeating("Stopper", 2, 1);
    }

    public void PlayClip(AudioClip playClip)
    {
        audioSource.clip = playClip;
        audioSource.Play();
    }
    
    void Update()
    {
        PauseCheck();
    }
    void Stopper()
    {
        timeToEnd--;
        timeText.text = timeToEnd.ToString();
        snowFlake.enabled = false;
        Debug.Log("Time: " + timeToEnd + " s");
        if (timeToEnd <= 0)
        {
            timeToEnd = 0;
            endGame = true;
        }
        if (endGame)
        {
            EndGame();
        }
    }
    public void PauseGame()
    {
        PlayClip(pauseClip);
        infoPanel.SetActive(true);
        Debug.Log("Pause Game");
        Time.timeScale = 0f;
        gamePaused = true;
    }
    public void ResumeGame()
    {
        PlayClip(resumeClip);
        infoPanel.SetActive(false);
        Debug.Log("Resume Game");
        Time.timeScale = 1f;
        gamePaused = false;
    }
    public void EndGame()
    {
        CancelInvoke("Stopper");
        if (win)
        {
            PlayClip(winClip);
            Debug.Log("You Win!!! Reoad?");
        }
        else
        {
            PlayClip(loseClip);
            Debug.Log("You Lose!!! Reload?");
        }
    }

    public void SetUseInfo(string info)
    {
        useInfo.text = info;
    }

    void PauseCheck()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void AddPoints(int point)
    {
        points += point;
        crystalText.text = points.ToString();
    }

    public void addTime(int addTime)
    {
        timeToEnd += addTime;
        timeText.text = timeToEnd.ToString();
    }

    public void FreezeTime(int freeze)
    {
        CancelInvoke("Stopper");
        snowFlake.enabled = true;
        InvokeRepeating("Stopper", freeze, 1);
    }

    public void AddKey(KeyColor color)
    {
        if (color == KeyColor.Gold)
        {
            goldKey++;
            goldKeyText.text = goldKey.ToString();
        }
        else if (color == KeyColor.Green)
        {
            greenKey++;
            greenKeyText.text = greenKey.ToString();
        }
        else if (color == KeyColor.Red)
        {
            redKey++;
            redKeyText.text = redKey.ToString();
        }
    }


}


