using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundVertical : MonoBehaviour
{

    [SerializeField] private float speed = 1f;
    [SerializeField] private float clamppos;
    [SerializeField] private GameObject player;
    [SerializeField] private bool BackgroundUp;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    void FixedUpdate()
    {
        float newPosition = Mathf.Repeat(Time.time * speed, clamppos);
        transform.position = startPosition - Vector3.up * newPosition;

        if (player.transform.position.x >= transform.transform.position.x + GetComponent<Renderer>().bounds.size.x + (GetComponent<Renderer>().bounds.size.x / 2)) {
            startPosition.x += (GetComponent<Renderer>().bounds.size.x * 3);
        }

        else if (player.transform.position.x <= transform.transform.position.x - GetComponent<Renderer>().bounds.size.x - (GetComponent<Renderer>().bounds.size.x / 2))
        {
            startPosition.x -= (GetComponent<Renderer>().bounds.size.x * 3);
        }

        if (player.transform.position.y <= transform.transform.position.y - ((GetComponent<Renderer>().bounds.size.y * 3) + (GetComponent<Renderer>().bounds.size.y / 2)))
        {
            startPosition.y -= (GetComponent<Renderer>().bounds.size.y * 6);
        }

        else if (player.transform.position.y <= transform.transform.position.y - ((GetComponent<Renderer>().bounds.size.y * 4) + (GetComponent<Renderer>().bounds.size.y / 2)))
        {
            startPosition.y -= (GetComponent<Renderer>().bounds.size.y * 7);
        }


        if (player.transform.position.y >= transform.transform.position.y + ((GetComponent<Renderer>().bounds.size.y * 3) + (GetComponent<Renderer>().bounds.size.y / 2)))
        {
            startPosition.y += (GetComponent<Renderer>().bounds.size.y * 6);
        }
    }
}
