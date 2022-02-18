using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  

public class PlayerForce : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude;
    private bool isCollided = false;
    float points = 0f;
     CharacterController controller;
    public float timeRemaining = 10f;
    public Text TimerText;
    public Text PointsText;
    public Text EndText;


    void Start() {
        controller = GetComponent<CharacterController>();
        controller.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Display and update timer
        TimerText.text = "TIME UNTIL HUMAN IS BACK: "+(timeRemaining).ToString("0")+" SEC";
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else {
            Invoke("End",1f);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        // Add forces to object in contact
        if(rigidbody != null && !isCollided) {
            Vector3 forceDirection = hit.gameObject.transform.position = transform.position;
            forceDirection.y = 0f;
            forceDirection.Normalize();
            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
            Invoke("checkCollision",2f);

            // Counts the number of object moved
            if (hit.gameObject.tag != "Object") {
                points++;
            }

            // Detects only once
            hit.gameObject.tag = ("Object");
        }
    }

    void checkCollision() {
        isCollided = false;
    }

    // Will display score and return to menu
    void End() {
        controller.enabled = false;
        EndText.text = "Reload to play again";
        PointsText.text = "You displaced "+points+" objects";
        // Invoke("Scene01",4f);
    }

    // Doesnt work for now :<
    // void Scene01 () {
    //     controller.enabled = true;
    //     SceneManager.LoadScene(0); 
    // }
}
