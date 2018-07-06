using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraDown : MonoBehaviour
{
    [SerializeField] Player player;
    Collider2D playerCollider;

    // Use this for initialization
    void Start()
    {
        playerCollider = player.GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow) && playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            gameObject.SetActive(true);
        }
    }
}
