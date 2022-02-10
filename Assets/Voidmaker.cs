using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voidmaker : MonoBehaviour
{
    [SerializeField] Material skyboxMaterial;
    [SerializeField] Light sun;
    // Start is called before the first frame update
    void Start()
    {
        skyboxMaterial.SetColor("_SunColor", Color.white);
        sun.intensity = 0.3f;
        skyboxMaterial.SetColor("_Horizon", Color.black);
        skyboxMaterial.SetColor("_Zenith", Color.black);
        skyboxMaterial.SetColor("_Nadir", Color.black);
        Shader.SetGlobalVector("_SunDirection", transform.forward);


        skyboxMaterial.SetFloat("_CloudOpacity", 0f);
        skyboxMaterial.SetFloat("_StarOpacity", 1f);
        skyboxMaterial.SetFloat("_CloudColorIntensity", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
