using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SpawnScript : MonoBehaviour
{
    public GameObject Bone;
    private bool SetPosition()
    {
        Transform cam = Camera.main.transform;
        GameObject obj = Instantiate(Bone, cam.forward * 10, Quaternion.identity);
        return true;
    }
    private bool mPositionSet;

    void Start()
    {
        StartCoroutine(ChangePosition());
    }

    private IEnumerator ChangePosition()
    {
        yield return new WaitForSeconds(0.2f);
        if (!mPositionSet)
        {
            if (VuforiaBehaviour.Instance.enabled)
            {
                SetPosition();
            }
        }
    }

}
