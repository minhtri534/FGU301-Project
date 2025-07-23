using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class Cage : MonoBehaviour
{
    public bool isUnlocked = false;
    public Vector3 velocity;
    public Vector3 acceleration = new Vector3(0,20,0);
    private GameObject player;
    private GameObject gameManager;
    void Start()
    {
        player = GameObject.Find("Knight");
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (isUnlocked)
        {
            velocity += acceleration * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }
    }

    public IEnumerator Win()
    {
        isUnlocked = true;
        yield return new WaitForSeconds(3);
        gameManager.GetComponent<GameManager>().GameWin();
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            StartCoroutine(Win());
        }
    }
}
