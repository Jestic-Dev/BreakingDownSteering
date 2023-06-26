using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerType3 : MonoBehaviour
{
        //public float moveSpeed;
        public float rotataion;
    public float transformedX;
    public Transform rightWall;
    public Transform leftWall;

    // Update is called once per frame
    void Update()
        {
            this.rotataion = 0;


             if(Input.GetKey(KeyCode.Q))
            {
                this.rotataion = -60;
            }

            if(Input.GetKey(KeyCode.W))
            {
                this.rotataion = -30;
            }

           if(Input.GetKey(KeyCode.E))
            {
                this.rotataion = -10;
            }

            
            if(Input.GetKey(KeyCode.R))
            {
                this.rotataion = 10;
            }

            
            if(Input.GetKey(KeyCode.T))
            {
                this.rotataion = 30;
            }

            if(Input.GetKey(KeyCode.Y))
            {
                this.rotataion = 60;
            }


            this.transformedX = this.rotataion/60 * rightWall.position.x;


            transform.position = new Vector3(this.transformedX, transform.position.y, transform.position.z);
        }
}
