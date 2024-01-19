using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float destoryTime;

    private void Start()
    {
        Destroy(gameObject, destoryTime);
    }
}
