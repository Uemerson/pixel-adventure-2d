using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformVertical : MonoBehaviour
{
    private PlatformEffector2D platformEffector2D;
    private float waitTime;
    private bool disablePlatform = false;

    [SerializeField] private Player player;

    // Start is called before the first frame update
    void Start()
    {
        platformEffector2D = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && !player.onWall)
        {
            platformEffector2D.rotationalOffset = 180f;
            waitTime = 0.15f;
            disablePlatform = true;
        }

        if (waitTime > 0)
            waitTime -= Time.deltaTime;

        if (Input.GetKey(KeyCode.UpArrow) && disablePlatform) {
            platformEffector2D.rotationalOffset = 0;
            disablePlatform = false;
        }
    }

    private void FixedUpdate()
    {
        if (waitTime <= 0f && disablePlatform)
        {
            platformEffector2D.rotationalOffset = 0;
            disablePlatform = false;
        }
    }
}
