using UnityEngine;
using UnityEngine.SceneManagement;

public class BackUp : MonoBehaviour
{
    public string GetBackToWork;

    private bool timeRunning = false;
    private float timePassed = 0.0f;
    public float TargetTime = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeRunning = true;
    }
    private void FixedUpdate()
    {
        if (timeRunning == true)
        {
            if (timePassed < TargetTime)
            {
                timePassed += Time.deltaTime;
            }
            if (timePassed >= TargetTime)
            {
                SceneManager.LoadScene(GetBackToWork);
                timeRunning = false;

            }
        }
    }


}
