using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float thrust = 1f;
    public float rotatePower = 1f;
    public comet Comet;

    public int hp, score;

    public Sprite hp4;
    public Sprite hp3;
    public Sprite hp2;
    public Sprite hp1;

    private float x, y;
    private SpriteRenderer renderer;

    public ParticleSystem thrustParticle;
    public ParticleSystem smokeParticle;
    public ParticleSystem explosionParticle;

    void Start() {
        smokeParticle.Stop();
        explosionParticle.Stop();
        renderer = GetComponent<SpriteRenderer>();
        score = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        float rotate = Input.GetAxis("Horizontal");

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && hp > 0) {
            rb.AddForce(transform.up * thrust * Time.deltaTime);
            thrustParticle.Play();
        } else thrustParticle.Stop();

        if (hp < 4) smokeParticle.Play();

        if (hp > 0) {
            checkBorder();
            transform.Rotate(new Vector3(0, 0, -rotate * rotatePower * Time.deltaTime));
        }

        var emission = smokeParticle.emission;
        if (hp == 3) {
            emission.rateOverTime = 50;
            renderer.sprite = hp3;
        }
        else if (hp == 2) {
            emission.rateOverTime = 100;
            renderer.sprite = hp2;
        }
        else if (hp == 1) {
            emission.rateOverTime = 200;
            smokeParticle.startSize = 3.7f;
            renderer.sprite = hp1;
        }
        else if (hp <= 0) smokeParticle.Stop();
    }

    private void checkBorder() {
        if (transform.position.y > 5.4f) y = -5.4f;
        else if (transform.position.y < -5.4f) y = 5.4f;

        else if (transform.position.x > 9.1f) x = -9.1f;
        else if (transform.position.x < -9.1f) x = 9.1f;

        else {
            x = transform.position.x;
            y = transform.position.y;
        }

        transform.position = new Vector2(x, y);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "comet") {
            hp--;
            if(hp > 0) FindObjectOfType<Shake>().CamShake("shake");
        }
    }

    public void explode() {
        explosionParticle.Play();
        renderer.sprite = null;
    }
}
