using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float x, y;
    public float speed = 1f;
    public Quaternion direction;

    private GameObject playerObject;
    private PlayerController playerScript;
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();

        transform.rotation = direction;
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
        
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObject.GetComponent<PlayerController>();
        x = transform.position.x;
        y = transform.position.y;
    }
    void Update() {
        checkBorder();
    }
    public void checkBorder() {
        if (transform.position.y > 5.4f || transform.position.y < -5.4f 
            || transform.position.x < -9.1f || transform.position.x > 9.1f) Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "comet") {
            playerScript.score++;
            Destroy(this.gameObject);
        }
    }
}
