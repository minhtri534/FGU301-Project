using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private float speed = 5;
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Knight");
        
    }

    void FixedUpdate()
    {
        if (player != null && player.GetComponent<Player>().IsDead == false)
        {
            transform.position = Vector2.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}
