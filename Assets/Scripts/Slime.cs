using System.Collections;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float MoveSpeed = 3;
    public ContactFilter2D RightContactFilter;
    public ContactFilter2D LeftContactFilter;
    private Rigidbody2D rb;
    private Collider2D c;
    private int direction = 1;
    private GameObject player;
    private Collider2D sc;
    private GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        rb = GetComponent<Rigidbody2D>();
        c = GetComponent<Collider2D>();
        player = GameObject.Find("Knight");
        sc = transform.GetChild(0).gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocityX = MoveSpeed * direction;
        if (IsOnRightWall())
        {
            direction = -1;
        }
        else if (IsOnLeftWall())
        {
            direction = 1;
        }
    }

    private bool IsOnRightWall()
    {
        return rb.IsTouching(RightContactFilter);
    }

    private bool IsOnLeftWall()
    {
        return rb.IsTouching(LeftContactFilter);
    }

    public IEnumerator Die()
    {
        gameManager.GetComponent<GameManager>().AddScore(2);
        Destroy(c);
        Destroy(sc);
        rb.linearVelocityY = 6;
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            StartCoroutine(player.GetComponent<Player>().Die());
        }
    }
}
