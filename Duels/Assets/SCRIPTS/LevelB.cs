using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelB : MonoBehaviour
{
    public string Level; 

public void OpenLevel()
    {
        SceneManager.LoadScene(Level);
    }
}
