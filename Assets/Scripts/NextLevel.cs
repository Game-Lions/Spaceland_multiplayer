using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private float timeOnPlatform = 0;
    private bool spaceshipOnPlatform = false;
    public string nextScene;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spaceship"))
        {
            spaceshipOnPlatform = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Spaceship"))
        {
            spaceshipOnPlatform = false;
            timeOnPlatform = 0;
        }
    }

    void Update()
    {
        if (spaceshipOnPlatform)
        {
            timeOnPlatform += Time.deltaTime;
            if (timeOnPlatform >= 3)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }
}
