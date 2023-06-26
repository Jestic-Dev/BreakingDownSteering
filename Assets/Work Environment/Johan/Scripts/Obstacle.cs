using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveSpeed;
    protected float mySpeed = 0;
    public float speedModifier;

    private void Start()
    {
        mySpeed = moveSpeed + (speedModifier * (GameMaster.speedModifier - 1));
    }

    void Update()
    {
        Move();
    }

    protected void Move()
    {
        transform.position -= new Vector3(0, 0, mySpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        AvatarManager playerAvatar = other.GetComponent<AvatarManager>();

        if (playerAvatar != null && playerAvatar.isInvincible == false)
        {
            playerAvatar.GetHit();

            ObstaclePattern thisPattern = GetComponentInParent<ObstaclePattern>();

            if (thisPattern != null)
            {
                thisPattern.RegisterHit();
            }
        }
    }
}
