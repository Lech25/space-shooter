using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class comet : MonoBehaviour
{
    public float minspeed, maxspeed;

    public GameObject explosionParticle;

    public PlayerController player;

    private float speed;
    private Rigidbody2D rb;
    
    int pos;
    float rotation, scalex, scaley;
    Vector2 position, range;

    void Start() {
        speed = Random.Range(minspeed, maxspeed);
        scalex = Random.Range(6f, 10f);
        scaley = scalex;
        transform.localScale = new Vector3(scalex, scaley, 1);

        rb = GetComponent<Rigidbody2D>();
        pos = Random.Range(1, 5);
        if (pos == 1) {
            position = new Vector2(0, 6.65f);
            range = new Vector2(-225, -135);
        }
        else if (pos == 2) {
            position = new Vector2(0, -6.65f);
            range = new Vector2(-45, 45);
        }
        else if (pos == 3) {
            position = new Vector2(-10.4f, 0);
            range = new Vector2(-120, -60);
        }
        else {
            position = new Vector2(10.4f, 0);   
            range = new Vector2(60, 120);
        }
        float rangex = range.x;
        float rangey = range.y;
        transform.position = position;
        rotation = Random.Range(rangex, rangey);
        transform.Rotate(new Vector3(0, 0, rotation));
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    void Update() {
        checkBorder();
    }

    public void checkBorder() {
        if (transform.position.y > 15f || transform.position.y < -15f
            || transform.position.x < -20f || transform.position.x > 20f) Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag != "comet") {
            var expl = Instantiate(explosionParticle, transform.position, transform.rotation);
            Destroy(this.gameObject);
            if(collision.gameObject.tag == "bullet") FindObjectOfType<Shake>().CamShake("shake");
            Destroy(expl, 2f);
        }
        
    }
}
