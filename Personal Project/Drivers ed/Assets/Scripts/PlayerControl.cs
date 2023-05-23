using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody playerRb; //Players rigidbody
    public float speed = 5.0f; //Players speed
    private GameObject focalPoint;
    public bool hasPowerup = false;
    public GameObject powerupIndicator; //where the camera is looking
    private float powerupStrength = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); //set player rigidbody
        focalPoint = GameObject.Find("Focal");
    }

    // Update is called once per frame
    void Update()
    {
        // read up/down input from player as vertical input.
        float forwardInput = Input.GetAxis("Vertical");
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        //move player forward/backward when we press up/down in the direction we are looking

        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

    }
    private void OnTriggerEnter(Collider other) // OnTriggerEnter is called on the first frame that we collide with a trigger.
    {
        if (other.CompareTag("Power up"))
        hasPowerup = true;
        Destroy(other.gameObject);
        StartCoroutine(PowerupCountdownRoutine()); // Coroutine allows the player to delay stuff for a couple seconds
        
        powerupIndicator.gameObject.SetActive(true);
    }
    IEnumerator PowerupCountdownRoutine()
        {
            yield return new WaitForSeconds(7);
            hasPowerup = false;
            powerupIndicator.gameObject.SetActive(false);

        }
    
//OncollisionEnter us called on the first frame that we collide with a physics collider
    private void OnCollision(Collision collision)
    {
        Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position); //calculate the directim away from player
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        Debug.Log("Collided with" + collision.gameObject.name + " with powerup set to " + hasPowerup);
        enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
    }
}

