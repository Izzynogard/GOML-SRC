using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject Player;

    private float xin;           // x input holder
    private float yin;           // y input holder
    public float speed;             // speed multiplier
    private bool canDash;      // boolian for tracking if the player is moving
                                // is private because we only want player input to effect this variable (see line #51)
    private float dashforce = 0.8f;         // the force multiplier we will apply on the player (see line #67)

    public Camera cam_main;             // our camera (graphics renderer) object holder
    public Vector2 mpos;            // our mouse's position holder stored by a Vector (2D)
    private void Start()
    {
        // Initializing Vars //

        rb = Player.GetComponent<Rigidbody2D>();
    }

    private void Update()           // executes at the start of the frame
    {
        // Movement //

        xin = Input.GetAxisRaw("Horizontal");           // A + D keys
        yin = Input.GetAxisRaw("Vertical");             // W + S keys

        rb.MovePosition(rb.position + new Vector2(xin, yin) * speed);           // moving our character
    }

    private void FixedUpdate()          // executes at the end of the frame
    {
        // player orientation //

        mpos = cam_main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookdir = mpos - rb.position;           // finding the direction to look via. subtracting two 2D Vectors
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;            // updates orientation using the predefined (angle)

        // Dashing //

        if (xin != 0 || yin != 0)
        {
            canDash = true;
            if (canDash == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    dash();
                }
            }
            else
            {
                
            }
        }
    }

    void dash()
    {
        rb.AddForce(new Vector2(xin, yin) * dashforce);
        speed = 0.05f;
        canDash = false;
        wait(2.5f);
        speed = 0.2f;
    }       // script will now wait 2.5 seconds after dashing before the function can be called again (also slows player)


    // wait function //
    public IEnumerable wait(float seconds) // creates a IEnumerable (Base interface for "Generic collections")
    {
        yield return new WaitForSeconds(seconds); // waits for the given time (seconds)
        StopCoroutine("wait()");
    }
}
