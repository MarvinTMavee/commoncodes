using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    /*How to implement:
 * 1. Add a box colider to your ground, and tag the object with "Floor"
 * 2. Add a RigidBody2D, A Capsule & Box Collider 2D to the player
 * 3. Place the box collider slightly under the player for ground detection
 * 4. Check "IsTriggered" in the BoxCollider2D
 * 5. Set linear Drag, and GravityScale in the RigidBody2D, and also set the movespeed and jumpForce in this script.
 */

    private Rigidbody2D rb2D;


    //Define vars for future use
    public float moveSpeed;
    public float jumpForce;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        //NOTE: The input axis is set to the default in Unity, if changed correct it here:

        moveHorizontal = Input.GetAxisRaw("Horizontal"); //Set's the horizonal Input (Can be: -1 0 1)
        moveVertical = Input.GetAxisRaw("Vertical"); //Set's the vertical Input (Can be: -1 0 1)
    }
    void FixedUpdate()
    {
        if (moveHorizontal >  0.1f || moveHorizontal < - 0.1f) //Check if Axis values changed to move horinzontal
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0), ForceMode2D.Impulse); //Adds the force to move horizontal
        }

        if (!isJumping && moveVertical > 0.1f) //Check if jumping and if Axis values changed to move horinzontal
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse); //Adds the force to move vertical
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //Sets isjumping to false when ground (an object with the tag "Floor") is detected
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJumping = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //Unsets isJumping if no object is detected
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJumping = true;
        }
    }
}
