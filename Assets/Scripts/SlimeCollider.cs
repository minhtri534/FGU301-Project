using UnityEngine;

public class SlimeCollider : MonoBehaviour
{
    private Collider2D c;
    private GameObject player;
    private GameObject slime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        c = GetComponent<Collider2D>();
        player = GameObject.Find("Knight");
        slime = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            StartCoroutine(slime.GetComponent<Slime>().Die());
            player.GetComponent<Player>().Bounce();
        }
    }
}
