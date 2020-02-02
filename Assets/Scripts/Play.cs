using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Lose()
    {
        SceneManager.LoadScene(2);
    }
}
