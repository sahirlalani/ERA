using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float horizontalMoveRange;

    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - player.transform.position.x > horizontalMoveRange && transform.position.x > -3)
        {
            transform.position = new Vector3(player.transform.position.x + horizontalMoveRange, transform.position.y, transform.position.z);
        }
        if (player.transform.position.x - transform.position.x > horizontalMoveRange)
        {
            transform.position = new Vector3(player.transform.position.x - horizontalMoveRange, transform.position.y, transform.position.z);
        }
    }
}
