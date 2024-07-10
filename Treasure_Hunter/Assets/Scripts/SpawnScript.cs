using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SpawnScript : MonoBehaviour
{
    public GameObject Prefab;
    public int maxClones = 1;
    private int currentClones = 0;
    private bool SetPosition()
    {
        Transform cam = Camera.main.transform;
        GameObject obj = Instantiate(Prefab, cam.forward * 10, Quaternion.identity);
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
    void Update()
    {
        if (currentClones < maxClones)
        {
            // Generar un nuevo clone del objeto prefab
            GameObject clone = Instantiate(Prefab, transform.position, transform.rotation);
            currentClones++;
        }
    }
}


