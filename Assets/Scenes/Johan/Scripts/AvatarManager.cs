using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager : MonoBehaviour
{
    public Animator myAnimator;

    private float previousX;

    private int animSpeedID;
    private int animMotionSpeedID;

    public bool isInvincible = false;
    public float invincibilityFlickerSpeed;
    public float invincibilityBaseTime;
    public float invincibilitySpeedScaler;

    public GameObject characterModel;
    public GameObject accesoryModel;

    public bool mapAnimStyle;
    public bool animateFromTrack;
    public Transform obstacleTrack;

    private void Start()
    {
        animSpeedID = Animator.StringToHash("Speed");
        animMotionSpeedID = Animator.StringToHash("MotionSpeed");
        myAnimator.SetFloat(animMotionSpeedID, 1);
        myAnimator.fireEvents = false;
    }

    private void Update()
    {
        if (!animateFromTrack)
        {
            float xDiff = previousX - transform.position.x;

            if (xDiff < 0)
            {
                transform.LookAt(transform.position + Vector3.right);

                myAnimator.SetFloat(animSpeedID, mapAnimStyle ? 6 : 2 + -xDiff * 10);
            }
            else if (xDiff > 0)
            {
                transform.LookAt(transform.position - Vector3.right);

                myAnimator.SetFloat(animSpeedID, mapAnimStyle ? 6 : 2 + xDiff * 10);
            }
            else
            {
                transform.LookAt(transform.position + Vector3.forward);

                myAnimator.SetFloat(animSpeedID, 0);
            }

            previousX = transform.position.x;
        }
        else
        {
            float xDiff = previousX - obstacleTrack.position.x;

            if (xDiff < 0)
            {
                transform.LookAt(transform.position - Vector3.right);

                myAnimator.SetFloat(animSpeedID, mapAnimStyle ? 6 : 2 + -xDiff * 10);
            }
            else if (xDiff > 0)
            {
                transform.LookAt(transform.position + Vector3.right);

                myAnimator.SetFloat(animSpeedID, mapAnimStyle ? 6 : 2 + xDiff * 10);
            }
            else
            {
                transform.LookAt(transform.position + Vector3.forward);

                myAnimator.SetFloat(animSpeedID, 0);
            }

            previousX = obstacleTrack.position.x;
        }
    }

    public void GetHit()
    {
        StartCoroutine(BeInvincible());
    }

    public IEnumerator BeInvincible()
    {
        isInvincible = true;

        float invincibilityTime = invincibilityBaseTime - invincibilitySpeedScaler * GameMaster.speedModifier;

        while (invincibilityTime > 0)
        {
            characterModel.SetActive(!characterModel.activeSelf);
            accesoryModel.SetActive(!accesoryModel.activeSelf);

            yield return new WaitForSeconds(invincibilityFlickerSpeed);
            invincibilityTime -= Time.deltaTime;
        }

        characterModel.SetActive(true);
        accesoryModel.SetActive(true);

        isInvincible = false;
    }
}
