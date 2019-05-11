using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour
{
    [SerializeField] GameObject collectParticles;
    [SerializeField] GameObject persistentParticles;
    [SerializeField] Transform mesh;

    public bool IsActive { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && IsActive)
        {
            Collect();
        }
    }

    public void Collect()
    {
        IsActive = false;
        collectParticles.SetActive(true);
        persistentParticles.SetActive(false);
        mesh.gameObject.SetActive(false);
    }

    public void Activate()
    {
        mesh.gameObject.SetActive(true);
        mesh.localScale = Vector3.zero;
        StartCoroutine(Bundle.LerpToLocalScale(mesh, Vector3.one, 0.5f));
        persistentParticles.SetActive(true);
        IsActive = true;
    }

    public void Reset()
    {
        if(IsActive)
        {
            IsActive = false;
            StopAllCoroutines();
            StartCoroutine(Bundle.LerpToLocalScale(mesh, Vector3.zero, 0.25f, null, () =>
            {
                persistentParticles.gameObject.SetActive(false);
            }));
        }
    }
}
