using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    public static Stopwatch Instance;
    public float TimeElapsed { get; private set; }
    private bool isRunning;
    public TextMeshProUGUI timerText;

    void Start()
    {
        StartStopwatch();
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "YouWin")
        {
            StopStopwatch();
        }
        else if (isRunning)
            TimeElapsed += Time.deltaTime;
        Debug.Log(TimeElapsed);
        timerText.text = $"Time: {TimeElapsed:F2}";
    }

    public void StartStopwatch() => isRunning = true;

    public void StopStopwatch() => isRunning = false;
}
