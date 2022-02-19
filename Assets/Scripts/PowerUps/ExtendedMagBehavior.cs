using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedMagBehavior : MonoBehaviour
{
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 1.0f * Time.deltaTime * rotationSpeed, 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<WeaponHolder>().ReturnEquippedWeapon().SetExtendedMagSize();
            Destroy(gameObject);
        }
    }
}
