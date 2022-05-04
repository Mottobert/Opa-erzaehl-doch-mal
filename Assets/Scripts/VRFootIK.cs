using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFootIK : MonoBehaviour
{
    private Animator animator;
    [Range(0, 1)]
    public float rightFootPosWeight = 1;
    [Range(0, 1)]
    public float leftFootPosWeight = 1;

    [SerializeField]
    private Transform rightFootTransform;
    [SerializeField]
    private Transform leftFootTransform;

    [SerializeField]
    private Transform rightFootConstraintTransform;
    [SerializeField]
    private Transform leftFootConstraintTransform;

    private Transform rightResetTransform;
    private Transform leftResetTransform;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        rightResetTransform = Instantiate(rightFootConstraintTransform, rightFootConstraintTransform.position, rightFootConstraintTransform.rotation);
        leftResetTransform = Instantiate(leftFootConstraintTransform, leftFootConstraintTransform.position, leftFootConstraintTransform.rotation);
    }

    private void FixedUpdate()
    {
        Vector3 rightFootPos = rightFootTransform.position;
        RaycastHit hit;

        bool hasHit = Physics.Raycast(rightFootPos + Vector3.up, Vector3.down, out hit);

        if (hasHit)
        {
            //animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPosWeight);
            //animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point);

            rightFootConstraintTransform.position = hit.point;
        }
        else
        {
            //animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);

            rightFootConstraintTransform.position = rightResetTransform.position;
        }


        Vector3 leftFootPos = leftFootTransform.position;

        hasHit = Physics.Raycast(leftFootPos + Vector3.up, Vector3.down, out hit);
        if (hasHit)
        {
            //animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPosWeight);
            //animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point);

            leftFootConstraintTransform.position = hit.point;
        }
        else
        {
            //animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);

            leftFootConstraintTransform.position = leftResetTransform.position;
        }
    }
}
