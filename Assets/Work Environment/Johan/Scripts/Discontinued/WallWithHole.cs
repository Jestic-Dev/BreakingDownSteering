using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWithHole : MonoBehaviour
{
    public Vector3 myMoveDirection;
    public float myMoveSpeed;
    [SerializeField]
    private float myHolePosition;

    public Transform leftPart;
    public Transform rightPart;

    // Start is called before the first frame update
    void Start()
    {
        myHolePosition = Random.Range(-.8f, .8f);

        leftPart.localScale = new Vector3(4f + myHolePosition * 4, leftPart.localScale.y, leftPart.localScale.z);
        rightPart.localScale = new Vector3(4f - myHolePosition * 4, leftPart.localScale.y, leftPart.localScale.z);
    }

    private void Update()
    {
        transform.Translate(myMoveDirection * myMoveSpeed * Time.deltaTime);
    }
}
