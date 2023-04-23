using UnityEngine;

namespace Scenes.Christian.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed;

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKey(KeyCode.A))
            {
                transform.Translate(-transform.right * moveSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(transform.right * moveSpeed * Time.deltaTime);
            }
        }
    }
}