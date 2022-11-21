using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private AudioSource playerAudio;
    private float gravityModifier = 1.5f;
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;
    public float floatForce;
    public bool gameOver;
    public bool isLowEnough;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

    // Apply a small upward force at the start of the game
        playerRb = GetComponent<Rigidbody>();
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
    // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && isLowEnough && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

        if (transform.position.y > 13)
        { 
            isLowEnough = false; 
        }
        else 
        { 
            isLowEnough = true; 
        }
    }

    private void OnCollisionEnter(Collision other)
    {
     // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

     // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }
     // The balloon can drop below the ground   
        else if (other.gameObject.CompareTag("Ground") && !gameOver)
        {
            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            playerAudio.PlayOneShot(bounceSound, 1.5f);
        }
    }
}
