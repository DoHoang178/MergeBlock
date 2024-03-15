using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameManager : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
