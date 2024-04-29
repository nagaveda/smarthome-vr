using UnityEngine;
using System.Collections;

public class AvatarMove : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public Camera CameraObj;
    private Animator animator;
    private CharacterController char_cont;
    private float ySpeed;
    private float OSO;

    void Start()
    {
        animator = GetComponent<Animator>();
        char_cont = GetComponent<CharacterController>();
        OSO = char_cont.stepOffset;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = CameraObj.transform.forward; 

        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 movementDirection = (cameraForward * verticalInput + CameraObj.transform.right * horizontalInput).normalized;
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;

        ySpeed += Physics.gravity.y * Time.deltaTime;

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        char_cont.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("Ismoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {

            animator.SetBool("Ismoving", false);
        }


        if (Input.GetButton("js10") && (DoorsHandler.isDoorOpen))
        {
            animator.SetBool("Isopening", true);
            StartCoroutine(ResetIsOpening());
        }
        

    }
    IEnumerator ResetIsOpening()
    {
        // Wait for 1.5 seconds
        yield return new WaitForSeconds(1.0f);
        // After 1.5 seconds, set Isopening parameter back to false
        animator.SetBool("Isopening", false);
    }
}
