using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public void RestartLevel()
    {
        SceneManager.LoadScene("Test Level");
    }
}
