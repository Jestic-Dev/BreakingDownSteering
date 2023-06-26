using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerType2 : MonoBehaviour
{
    // Start is called before the first frame update

        public float moveSpeed;
        public float maxSpeed;
        public float currentSpeed;

    public Transform rightWall;
    public Transform leftWall;

    // Update is called once per frame
    void Update()
        {
            this.maxSpeed = 0;


             if(Input.GetKey(KeyCode.Q))
            {
                this.maxSpeed = -60;
            }

                         if(Input.GetKey(KeyCode.W))
            {
                this.maxSpeed = -30;
            }

                         if(Input.GetKey(KeyCode.E))
            {
                this.maxSpeed = -10;
            }

            
                         if(Input.GetKey(KeyCode.R))
            {
                this.maxSpeed = 10;
            }

            
                         if(Input.GetKey(KeyCode.T))
            {
                this.maxSpeed = 30;
            }

                                    if(Input.GetKey(KeyCode.Y))
            {
                this.maxSpeed = 60;
            }

            if(currentSpeed< maxSpeed){
                currentSpeed+= 0.01f;
            }

            if(currentSpeed> maxSpeed){
                currentSpeed-= 0.01f;
            }


            transform.Translate(transform.right * moveSpeed * Time.deltaTime * currentSpeed);

        if (transform.position.x > rightWall.position.x)
        {
            this.currentSpeed = 0;
            transform.position = new Vector3(rightWall.position.x, transform.position.y, transform.position.z);
        }

        if (transform.position.x < leftWall.position.x)
        {
            this.currentSpeed = 0;
            transform.position = new Vector3(leftWall.position.x, transform.position.y, transform.position.z);
        }
    }
}
