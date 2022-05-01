using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField]
    private Animator bookAnimator;
    [SerializeField]
    private BoxCollider boxCollider;

    public void OpenBook()
    {
        bookAnimator.SetBool("Open", true);
        boxCollider.enabled = false;
    }

    public void CloseBook()
    {
        bookAnimator.SetBool("Open", false);
        boxCollider.enabled = true;
    }
}
