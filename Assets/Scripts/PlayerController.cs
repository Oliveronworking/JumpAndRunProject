using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public Animator playerAnim;
    public AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce;
    public float slideSpeed = 40f;  // Geschwindigkeit des Rutschens
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;
    public GameObject GameOverCanvas;
    private bool isSliding = false;  // Überprüfen, ob der Charakter rutscht

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;

        if (playerRb == null) Debug.LogError("Rigidbody is not assigned.");
        if (playerAnim == null) Debug.LogError("Animator is not assigned.");
        if (playerAudio == null) Debug.LogError("AudioSource is not assigned.");
        if (explosionParticle == null) Debug.LogError("Explosion Particle is not assigned.");
        if (dirtParticle == null) Debug.LogError("Dirt Particle is not assigned.");
        if (jumpSound == null) Debug.LogError("Jump Sound is not assigned.");
        if (crashSound == null) Debug.LogError("Crash Sound is not assigned.");
        if (GameOverCanvas == null) Debug.LogError("GameOver Canvas is not assigned.");
    }

    // Update is called once per frame
    void Update()
    {
        // Springen
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        // Rutschen
        if (Input.GetKeyDown(KeyCode.LeftShift) && isOnGround && !isSliding && !gameOver)
        {
            StartCoroutine(SlideDown());
        }
    }

    private IEnumerator SlideDown()
    {
        isSliding = true;
        playerAnim.SetBool("Slide_b", true);  // Assuming you have a slide animation
        float slideTime = 1f;  // Dauer des Rutschens
        float startTime = Time.time;

        while (Time.time < startTime + slideTime)
        {
            playerRb.AddForce(Vector3.forward * slideSpeed, ForceMode.Impulse);
            yield return null;
        }

        playerAnim.SetBool("Slide_b", false);
        isSliding = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            if (GameOverCanvas != null)
            {
                GameOverCanvas.SetActive(true);
            }
            else
            {
                Debug.LogError("GameOver Canvas is not assigned.");
            }
        }
    }
}
