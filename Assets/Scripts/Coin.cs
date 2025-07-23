using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D c;
    private GameObject player;
    private GameObject gameManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        c = GetComponent<Collider2D>();
        gameManager = GameObject.Find("GameManager");
        player = GameObject.Find("Knight");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator Collect()
    {
        gameManager.GetComponent<GameManager>().AddScore(1);
        Destroy(c);
        rb.gravityScale = 2;
        rb.linearVelocityY = 8;
        rb.angularVelocity = 10;
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            StartCoroutine(Collect());
        }
    }

}
