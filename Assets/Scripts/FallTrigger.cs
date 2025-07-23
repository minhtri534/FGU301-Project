using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Knight");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            StartCoroutine(player.GetComponent<Player>().Die());
        }
    }
}
