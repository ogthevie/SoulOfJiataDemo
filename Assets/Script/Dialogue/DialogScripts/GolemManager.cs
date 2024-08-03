using UnityEngine;

public class GolemManager : CharacterManager
{
    [SerializeField] Material originalMaterial, activatematerial, speakMaterial;
    protected override void OnEnable()
    {
        if(storyManager.storyStep > 5)
        {
            var materials = GetComponent<Renderer>().materials;
            materials[1] = activatematerial;
            GetComponent<Renderer>().materials = materials;
            transform.GetChild(0).gameObject.GetComponent<Renderer>().material = activatematerial;            
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
            transform.GetChild(0).GetComponent<Renderer>().material = speakMaterial;
        }    
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 3 && storyManager.storyStep >= 6)
        {
            var materials = GetComponent<Renderer>().materials;
            materials[1] = activatematerial;
            GetComponent<Renderer>().materials = materials;
            transform.GetChild(0).GetComponent<Renderer>().material = activatematerial;         
        }
    }
}
