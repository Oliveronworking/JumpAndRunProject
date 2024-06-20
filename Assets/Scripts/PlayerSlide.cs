using System.Collections;
using UnityEngine;

public class CharacterSlide : MonoBehaviour
{
    public CharacterController characterController;
    public float slideSpeed = 10f;
    public float slideDuration = 1f;
    public KeyCode slideKey = KeyCode.LeftShift;

    private bool isSliding = false;
    private Vector3 slideDirection;

    void Update()
    {
        if (Input.GetKeyDown(slideKey) && !isSliding)
        {
            StartCoroutine(Slide());
        }
    }

    IEnumerator Slide()
    {
        isSliding = true;
        float startTime = Time.time;

        // Set the slide direction based on the current forward direction of the character
        slideDirection = transform.forward;

        while (Time.time < startTime + slideDuration)
        {
            // Move the character in the slide direction
            characterController.Move(slideDirection * slideSpeed * Time.deltaTime);
            yield return null;
        }

        isSliding = false;
    }
}