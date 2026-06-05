using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 12f; //szybkość naszej postaci
    public float gravity = -10; //przyspieszenie ziemskie 
    Vector3 velocity; //wyliczona prędkość w każdym kierunku
    CharacterController characterController; //komponent Character Controller

    public Transform groundCheck; // miejsce na nasz obiekt
    public LayerMask groundMask; // grupa obiektow, ktore beda warstwa uznawana za teren

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        //velocity.y += gravity * Time.deltaTime;

        //characterController.Move(velocity * Time.deltaTime);

        RaycastHit hit; // zmienna, w której zapisywana jest referencja do uderzonego obiektu

        if (Physics.Raycast(groundCheck.position, transform.TransformDirection(Vector3.down),
            out hit, 0.4f, groundMask))
        {
            string terrainType;
            terrainType = hit.collider.gameObject.tag;

            switch (terrainType)
            {
                default: // standardowa prędkość gdy chodzimy po dowolnym terenie
                    speed = 12;
                    break;

                case "Low":
                    speed = 3;
                    break;

                case "High":
                    speed = 20;
                    break;
            }

        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Somethin Collided!!!");
        Debug.Log(hit.gameObject.name);

        if(hit.gameObject.tag == "PickUp")
        {
            hit.gameObject.GetComponent<PickUp>().Picked();
        }

    }

    
}
