using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using SJ;

public class CameraShake : MonoBehaviour
{
    Transform point;
    AudioManager audioManager;
    AnimatorManager animatorManager;

    public AnimationCurve smooth;

    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        animatorManager = FindFirstObjectByType<AnimatorManager>();
        point = new GameObject("Camera - PointInterested").transform;
    }

    void OnEnable()
    {
        // S'abonner à l'événement activeSceneChanged
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    void OnDisable()
    {
        // Se désabonner de l'événement lorsque le script est désactivé ou détruit
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }

    void OnActiveSceneChanged(Scene previousScene, Scene newScene)
    {
        point = new GameObject("Camera - PointInterested").transform;
        /*Debug.Log("Scène précédente : " + previousScene.name);
        Debug.Log("Nouvelle scène : " + newScene.name);*/

    }

    public void Shake(float duartion, float magnitude)
    {   
        StartCoroutine(Shaker(duartion, magnitude));
    }
    IEnumerator Shaker(float duartion, float magnitude) 
    {
        audioManager.EarthQuakeFx();
        animatorManager.PlayTargetAnimation("LookAround", true);
        point.position = transform.position + transform.forward;

        Vector3 oldPos = point.position;

        float elapsed = 0.0f;

        while (elapsed < duartion) {
            float x = Random.Range(-1f, 1) * (magnitude/40);
            float y = Random.Range(-1f, 1) * (magnitude/40);
            float z = Random.Range(-1f, 1) * (magnitude/40);

            Vector3 shaked = new Vector3(x, y, z);
            float sm = smooth.Evaluate(elapsed / duartion);
            point.position = oldPos + (shaked * sm);

            transform.LookAt(point);

            elapsed += Time.deltaTime;

            yield return null;
        }

        point.position = oldPos;
    }
}
