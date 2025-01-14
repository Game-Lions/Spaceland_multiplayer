using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crash : NetworkBehaviour
{
    private Rigidbody2D rb;
    public float ImpactCapacity;
    public GameObject explosion;
    private float initialSpeed;
    public string SceneToReloadOnCrash;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        explosion.SetActive(false);
    }

    private void Update()
    {
        initialSpeed = rb.linearVelocity.magnitude;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Spaceship"))
        {
            Debug.Log("Velocity: " + initialSpeed);
            if (initialSpeed > ImpactCapacity)
            {
                Debug.Log("Crash!");
                explosion.SetActive(true);
                StartCoroutine(GrowExplosion());
            }
        }
        else
        {
            Debug.Log("Crash!");
            explosion.SetActive(true);
            StartCoroutine(GrowExplosion());
        }

    }

    private System.Collections.IEnumerator GrowExplosion()
    {
        Vector3 initialScale = explosion.transform.localScale;
        Vector3 targetScale = new Vector3(10f, 10f, 1f); ;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            explosion.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        explosion.transform.localScale = targetScale; // Ensure it ends exactly at maxScale
        explosion.SetActive(false);
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneToReloadOnCrash);
    }
}
