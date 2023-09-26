using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float threshold;   // Start is called before the first frame update
    public Camera mainCam;
    private Vector3 OldPlrVelocity = new Vector3(0,0,0);

    

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        mainCam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {


        bool grounded = Physics.Raycast(rb.position, Vector3.down, 1);
        Vector3 playerInput = new Vector3();
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.z = Input.GetAxis("Vertical");

        Vector3 CurrentRot = new Vector3();
        CurrentRot.x = this.transform.rotation.x;
        CurrentRot.y = mainCam.transform.rotation.y;
        CurrentRot.z = this.transform.rotation.z;

        //this.transform.rotation = Vector3.RotateTowards(Quaternion.ToEulerAngles(this.transform.rotation), new Vector3(this.transform.rotation.x, mainCam.transform.rotation.y, this.transform.rotation.z), 10f, 10f);
        this.transform.rotation = Quaternion.AngleAxis(mainCam.transform.eulerAngles.y, Vector3.up); //Quaternion.Euler(0, mainCam.transform.rotation.y, 0);
        //this.transform.rotation = Quaternion.Euler(mainCam.transform.eulerAngles.x, 30, transform.eulerAngles.z);


        float velocity = rb.velocity.magnitude;
        //Debug.Log(velocity);

        //Debug.Log(playerInput);
        if (velocity < threshold && grounded == true)
        {
            rb.AddRelativeForce(playerInput * 100, ForceMode.Acceleration);
        }
        
       

        Debug.Log("Magnitude");
        Debug.Log(playerInput.magnitude);


        if (playerInput.magnitude == 0 && grounded == true)
        {
            Vector3 plrVelocity = rb.velocity;
            Vector3 NewMovementVector;

            
            if (plrVelocity.magnitude < 1) {
                


                NewMovementVector = new Vector3(plrVelocity.x,0,plrVelocity.z);
                rb.AddRelativeForce(-NewMovementVector, ForceMode.VelocityChange);
            
            }
            else
            {
               if (plrVelocity.x == 0) {

                    NewMovementVector = new Vector3(plrVelocity.x, 0, plrVelocity.z);

                }

                rb.AddRelativeForce(-(plrVelocity / 3), ForceMode.VelocityChange);
            }
            


        }
        if (CheckJump() && grounded == true )
        {
            rb.AddForce(Vector3.up * 7, ForceMode.VelocityChange);

            
        }

    }

    private bool CheckJump()
    {
        return Input.GetKey(KeyCode.Space);
    }
    }
