using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30;

    // Referentie naar de PlayerController script
    private PlayerController playerControllerScript;

    private float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        // connect de referentie met het playercontroller script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    { // alles stopt met bewegen als je de player aanraakt
        if (playerControllerScript.gameOver == false)
        {
            //Dit laat dingen naar links bewegen
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        //Dit laat de obstacles wegg gaan als ze onder X-10 komen
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        
        
        
    }
}
