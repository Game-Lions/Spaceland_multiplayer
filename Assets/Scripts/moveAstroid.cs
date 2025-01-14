using UnityEngine;

public class move : MonoBehaviour
{
    private Rigidbody2D rb;
    public float MinValueX;
    public float MaxValueX;
    public float MinValueY;
    public float MaxValueY;
    void Start()
    {
        float randomValueX = Random.Range(MinValueX, MaxValueX);
        float randomValueY = Random.Range(MinValueY, MaxValueY);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-randomValueX, randomValueY), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
