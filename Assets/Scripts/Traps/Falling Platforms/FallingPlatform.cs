using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallingTime;
    private TargetJoint2D target;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Invoke("Falling", fallingTime);
        }
    }

    private void Falling(){
        target.enabled = false;
        boxCollider.isTrigger = true;
    }
}
