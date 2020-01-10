using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;

    // referentie naar de jump en crash sound
    public AudioClip jumpSound;
    public AudioClip crashSound;

    private AudioSource playerAudio;

    // Referenties naar de particle effect op de player
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    // referentie naar de animator 
    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        // hier voeg je de components toe aan de player
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    
    

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space)&& isOnGround && !gameOver)
        {
            // als je op de grond staat en de spatie balk indrukt spring je omhoog
            // als je niet op de grond staat kun je dus niet springen 
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

            // De PLayer anim jump trig zorgt ervoor dat de jump animatie wordt gebruikt als je op spatie drukt
            playerAnim.SetTrigger("Jump_trig");

            // Als je springt stopt de particle animatie
            dirtParticle.Stop();

            //Als je op spatie drukt speel je de jump sound 1 keer af
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        //hier wordt gedetecteerd of je op de grond staat
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;

            // Als je op de grond komt speelt de dirt animatie weer
            dirtParticle.Play();
        }
        // hier wordt gedetecteerd of je tegen een obstacle aanloopt
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            // dit laad de death animation
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            // Dit laat de explosie zien las je tegen een hekje aanlopt
            explosionParticle.Play();

            // Als je dood gaat stop de dirtParticle
            dirtParticle.Stop();

            // Als je dood gaat speelt de crash audio 1x af
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
