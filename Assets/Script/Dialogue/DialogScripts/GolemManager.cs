using UnityEngine;

public class GolemManager : CharacterManager
{
    public Material originalMaterial, activatematerial;
    protected override void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            var materials = GetComponent<Renderer>().materials;
            materials[1] = activatematerial;
            GetComponent<Renderer>().materials = materials;
        }    
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            var materials = GetComponent<Renderer>().materials;
            materials[1] = originalMaterial;
            GetComponent<Renderer>().materials = materials;
        }       
    }

}
