using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;

        public Rigidbody2D rb;
        public Camera cam;

        Vector2 movement;
        Vector2 mousePos;

    // Update is called once per frame
    void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            //ShootingAim
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

            Vector2 lookDir = mousePos - rb.position;
            //Atan2 = mathematical function that returns the angle between the x-axis and a 2D vector starting at 0 and teminating at X, Y
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;

        }
    }


