using UnityEngine;

public class GolemManager : CharacterManager
{
    public Material originalMaterial, activatematerial, speakMaterial;
    protected override void Start()
    {
        if(storyManager.storyStep > 5)
        {
            var materials = GetComponent<Renderer>().materials;
            materials[1] = activatematerial;
            GetComponent<Renderer>().materials = materials;              
        }
        else
        {
            var materials = GetComponent<Renderer>().materials;
            materials[1] = originalMaterial;
            GetComponent<Renderer>().materials = materials;           
        }
 
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3 && storyManager.storyStep >= 6)
        {
            //jouer un son
            var materials = GetComponent<Renderer>().materials;
            materials[1] = speakMaterial;
            GetComponent<Renderer>().materials = materials;
        }    
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 3 && storyManager.storyStep >= 6)
        {
            var materials = GetComponent<Renderer>().materials;
            materials[1] = originalMaterial;
            GetComponent<Renderer>().materials = materials;            
        }
    }
}
