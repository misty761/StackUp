using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject uiTitle;
    public GameObject uiPlaying;
    public GameObject uiGameOver;
    public Text textCurrentScore;
    public Text textTopScore;

    public float stackedHeight;

    int score;
    int scoreTop;
    readonly float thresholdCameraY = 5f;
    public float offsetCamera;

    public enum GameState
    {
        Title,
        Playing,
        GameOver
    }

    public GameState gameState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log(this + " : GameObject is already existed!");
            Destroy(gameObject);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Title;
        uiTitle.SetActive(true);
        uiPlaying.SetActive(false);
        uiGameOver.SetActive(false);
        scoreTop = PlayerPrefs.GetInt("ScoreTop", 0);
        textTopScore.text = "Top:" + scoreTop;
    }

    public void GameStart()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.audioClick, Vector3.zero, 1f);

        gameState = GameState.Playing;

        uiTitle.SetActive(false);
        uiPlaying.SetActive(true);
        uiGameOver.SetActive(false);

        ItemSpawner itemSpawner = FindObjectOfType<ItemSpawner>();
        itemSpawner.SpawnItem();

        score = 0;
        stackedHeight = 0f;
        offsetCamera = 0f;
    }

    public void GameOver()
    {
        if (gameState == GameState.GameOver) return;
        SoundManager.instance.PlaySound(SoundManager.instance.audioGameOver, Vector3.zero, 1f);
        gameState = GameState.GameOver;
        uiTitle.SetActive(false);
        uiPlaying.SetActive(false);
        uiGameOver.SetActive(true);

        // 최고기록 저장
        if (score > scoreTop)
        {
            SoundManager.instance.PlaySound(SoundManager.instance.audioFanfare, Vector3.zero, 1f);
            scoreTop = score;
            PlayerPrefs.SetInt("ScoreTop", scoreTop);
            textTopScore.text = "Top:" + scoreTop;
        }
    }

    public void GameRetry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void AddScore()
    {
        if (gameState != GameState.Playing) return;
        score = (int) (stackedHeight * 10);
        textCurrentScore.text = "" + score;
        
        // 물건이 쌓였을 때 카메라 올려야하는 값
        if (stackedHeight > thresholdCameraY)
        {
            offsetCamera = stackedHeight - thresholdCameraY;
        }
    }

}
