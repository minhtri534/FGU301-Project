using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D c;
    private GameObject player;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        c = GetComponent<Collider2D>();
        player = GameObject.Find("Knight");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Win()
    {
        Destroy(c);
        rb.gravityScale = 2;
        rb.linearVelocityY = 8;
        rb.angularVelocity = 10;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        SceneManager.LoadScene("Level 2");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            StartCoroutine(Win());
        }
    }
}
