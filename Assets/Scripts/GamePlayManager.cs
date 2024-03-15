using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] Image loseBanner;
    [SerializeField] Image winBanner;
    private float remainingTime = 45f;
    private bool isRunningTime = true;
    private Board board;

    private void Start()
    {
        board = GameObject.FindObjectOfType<Board>();
        if (board == null)
        {
            Debug.LogError("null.");
            return;
        }
        board.OnConditionTrue += Win;
    }
    private void Update()
    {
        if (isRunningTime)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime < 0)
            {
                remainingTime = 0;
                Debug.Log("lose");
                loseBanner.gameObject.SetActive(true);
            
            }
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        
        
    }
    private void Win()
    {
        winBanner.gameObject.SetActive(true);
        isRunningTime = false;
    }
    public void LoadSceneGameMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }
    public void LoadSceneGamePlay()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void NextLevel()
    {
        board.level++;
        winBanner.gameObject.SetActive(false);
        board.ClearBoard();
        board.SetupLevel(board.level);
    }
    public void ResetLevel()
    {
        loseBanner.gameObject.SetActive(false);
        winBanner.gameObject.SetActive(false);

        board.ClearBoard();
        board.SetupLevel(board.level);
        remainingTime = 45f;
        isRunningTime = true;
    }
}
