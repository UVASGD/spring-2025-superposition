using System.Collections.Generic;
using UnityEngine;

public class DuplicateExactPosition : MonoBehaviour
{
    public Transform objectsToDuplicateParent;
    public Transform newLocationTransform;
    public Transform newParent;

    void Start()
    {
        foreach (Transform child in objectsToDuplicateParent)
        {
            GameObject newObj = Instantiate(child.gameObject, child.position - transform.position + newLocationTransform.position - new Vector3(0f,0.02f,0f), child.rotation * Quaternion.Inverse(transform.rotation) * newLocationTransform.rotation, newParent);
            newObj.transform.localScale = child.localScale;
        }
    }
}
