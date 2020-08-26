using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public GameObject Fluid;
    public AudioSource WaterSpill;
    public int DefaultGardenWateringCount = 2;
    public int GardenWateringCount;

    public void OnWaterGrab()
    {
        GardenWateringCount = DefaultGardenWateringCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnWaterGrab();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
            return;
        if (GardenWateringCount > 0 && (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270
                || transform.rotation.eulerAngles.x > 90 && transform.rotation.eulerAngles.x < 270))
        {
            GardenWateringCount = 0;
            Instantiate(Fluid, transform.position, Quaternion.identity);
            WaterSpill.Play();
        }
    }
}
