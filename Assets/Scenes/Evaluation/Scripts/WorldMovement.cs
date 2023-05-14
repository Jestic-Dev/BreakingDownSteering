using UnityEngine;

namespace Scenes.Evaluation.Scripts
{
    public class WorldMovement : MonoBehaviour
    {
        public float speed = 5.0f;

        // Update is called once per frame
        void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

            float deltaTimeSpeed = Time.deltaTime * speed;
            // Move the game object in the opposite direction of the player input
            transform.Translate(-movement * deltaTimeSpeed);
        }
    }
}
