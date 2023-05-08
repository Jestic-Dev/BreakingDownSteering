using UnityEngine;

namespace Scenes.Christian.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed;
        public float rotataion;
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


            transform.Translate(transform.right * moveSpeed * Time.deltaTime * rotataion);

            if(transform.position.x > rightWall.position.x)
            {
                transform.position = new Vector3(rightWall.position.x, transform.position.y, transform.position.z);
            }

            if (transform.position.x < leftWall.position.x)
            {
                transform.position = new Vector3(leftWall.position.x, transform.position.y, transform.position.z);
            }
        }
    }
}
