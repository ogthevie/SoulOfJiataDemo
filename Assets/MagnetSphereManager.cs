using SJ;
using UnityEngine;

public class MagnetSphereManager : MonoBehaviour
{
    Rigidbody msRigibody;
    VaseContainerManager vaseContainerManager;

    void Awake()
    {
        msRigibody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        msRigibody.isKinematic = false;
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 3)
        {
            msRigibody.velocity = Vector3.zero;
        }
        else if(other.gameObject.layer == 10)
        {
            if(other.collider.TryGetComponent<VaseContainerManager>(out VaseContainerManager component)) component.HandleVaseConatinerProcess();
        }
    }
}
