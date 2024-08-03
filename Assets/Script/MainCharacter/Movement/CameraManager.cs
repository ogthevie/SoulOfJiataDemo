using System.Collections.Generic;
using UnityEngine;

namespace SJ
{
    public class CameraManager : MonoBehaviour
    {
        #region variables
        public Transform targetTransform; //la position de l'objet à suivre
        [SerializeField] Transform targetTransformSecond;
        public Transform cameraTransform; //la position actuel du parent de la camera
        public Transform cameraPivotTransform; //la position du pivot de la caméra
        public Transform myTransform;
        private Vector3 cameraTransformPosition;
        public LayerMask ignoreLayers;
        public LayerMask environmentLayer;
        private Vector3 cameraFollowVelocity = Vector3.zero;

        EnemyManager enemyManager;
        InputManager inputManager;
        PlayerManager playerManager;
        GameManager gameManager;

        public static CameraManager singleton;

        public float lookSpeed = 0.03f;
        public float followSpeed = 0.07f;
        public float pivotSpeed = 0.02f;

        private float targetPosition; 
        private float defautlPosition; //la position par défaut de la camera en Y
        private float lookAngle;
        private float pivotAngle;
        readonly float minimumPivot = -60f;
        readonly float maximumPivot = 60;

        public readonly float cameraSphereRadius = 0.5f;
        public readonly float cameraCollisionOffset = 1.5f; //De combien la camera sera décalé en cas de collision
        public readonly float minimumCollisionOffset = 1.1f;
        public float lockedPivotPosition = 2.25f;
        public float unlockedPivotPosition = 1.65f;
        public EnemyManager currentLockOnTarget;
        public List<EnemyManager>availableTargets = new();
        public float maximumLockOnDistance = 30;
        public EnemyManager nearestLockOnTarget;
        public EnemyManager leftLockTarget;
        public EnemyManager rightLockTarget;
        [SerializeField] LayerMask ignoreLayersMask;

        #endregion


        private void Awake()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("MainCamera"); // Recherche d'objets avec le tag spécifique

            if (objs.Length > 1) // Vérifier s'il y a déjà des objets persistants présents
            {
                // Si oui, détruire les doublons
                for (int i = 1; i < objs.Length; i++)
                {
                    Destroy(objs[i]);
                }
            }
            DontDestroyOnLoad(this.gameObject);

            singleton = this;
            myTransform = transform;
            defautlPosition = cameraTransform.localPosition.z;
            /*targetTransform = FindObjectOfType<PlayerManager>().transform;
            inputManager = FindObjectOfType<InputManager>();
            playerManager = FindObjectOfType<PlayerManager>();*/
            enemyManager = FindObjectOfType<EnemyManager>();
        }
        private void Start() 
        {          
            targetTransform = FindObjectOfType<PlayerManager>().transform;
            inputManager = FindObjectOfType<InputManager>();
            playerManager = FindObjectOfType<PlayerManager>();
            environmentLayer = LayerMask.NameToLayer("Environment");
            gameManager = FindObjectOfType<GameManager>();
        }
        public void FollowTarget()
        {
            Vector3 targetPosition = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref cameraFollowVelocity , followSpeed);
            myTransform.position = targetPosition;

            HandleCameraCollisions();
        }

        public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
        {
            if(inputManager.lockOnFlag == false && currentLockOnTarget == null)
            {
                if(gameManager.isControllerConnected)
                {
                    Vector3 rotation;
                    Quaternion targetRotation ;
                    lookAngle += (mouseXInput * lookSpeed) / delta;
                    pivotAngle -= (mouseYInput * pivotSpeed) / delta;
                    pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

                    rotation = Vector3.zero;
                    rotation.y = lookAngle;
                    targetRotation = Quaternion.Euler(rotation);
                    myTransform.rotation =  targetRotation;

                    rotation = Vector3.zero;
                    rotation.x = pivotAngle;

                    targetRotation =  Quaternion.Euler(rotation);
                    cameraPivotTransform.localRotation = targetRotation;
                }
                else
                {
                    if(inputManager.moveCameraFlag)
                    {
                        Vector3 rotation;
                        Quaternion targetRotation ;
                        lookAngle += (mouseXInput * lookSpeed) / delta;
                        pivotAngle -= (mouseYInput * pivotSpeed) / delta;
                        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

                        rotation = Vector3.zero;
                        rotation.y = lookAngle;
                        targetRotation = Quaternion.Euler(rotation);
                        myTransform.rotation =  targetRotation;

                        rotation = Vector3.zero;
                        rotation.x = pivotAngle;

                        targetRotation =  Quaternion.Euler(rotation);
                        cameraPivotTransform.localRotation = targetRotation;                        
                    }
                    else
                    {
                        if(inputManager.resetCameraFlag) ResetCameraPosition(delta);
                    }                  
                }

            }
            else
            {
                if(currentLockOnTarget == null)
                    return;
                //float velocity = 0;

                Vector3 dir = currentLockOnTarget.transform.position - transform.position;
                dir.Normalize();
                dir.y = 0;

                Quaternion targetRotation = Quaternion.LookRotation(dir);
                transform.rotation = targetRotation;

                dir = currentLockOnTarget.transform.position - cameraPivotTransform.position;
                dir.Normalize();

                targetRotation = Quaternion.LookRotation(dir);
                Vector3 eulerAngle = targetRotation.eulerAngles;
                eulerAngle.y = 0;
                cameraPivotTransform.localEulerAngles = eulerAngle;
            }
        }

        private void HandleCameraCollisions()
        {
            targetPosition =  defautlPosition;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
            direction.Normalize();

            if(Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetPosition), ignoreLayersMask))
            {
                float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetPosition = -(dis - cameraCollisionOffset);
            }

            if(Mathf.Abs(targetPosition) < minimumCollisionOffset)
            {
                targetPosition = -minimumCollisionOffset;
            }

            cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.6f);
            cameraTransform.localPosition = cameraTransformPosition;
        }

        public void HandleLockOn()
        {
            float shortesDistance = Mathf.Infinity;
            float shortesDistanceOfLeftTarget = -Mathf.Infinity;
            float shortesDistanceOfRightTarget = Mathf.Infinity;

            Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 40);

            for (int i = 0; i < colliders.Length; i++)
            {
                EnemyManager enemy = colliders[i].GetComponent<EnemyManager>();

                if (enemy != null)
                {
                    Vector3 lockTargetDirection = enemy.transform.position - targetTransform.position;
                    float distanceFromtarget = Vector3.Distance(targetTransform.position, enemy.transform.position);
                    float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward);
                    RaycastHit hit;

                    if(enemy.transform.root != targetTransform.transform.root && viewableAngle > -50 && viewableAngle < 50 && distanceFromtarget <= maximumLockOnDistance)
                    {
                        if(Physics.Linecast(playerManager.lockOnTransform.position, enemy.lockOnTransform.position, out hit))
                        {
                            //Debug.DrawLine(playerManager.lockOnTransform.position, enemy.lockOnTransform.position);

                            if(hit.transform.gameObject.layer == environmentLayer)
                            {
                                //cannot lock
                            }
                            else
                            {
                                availableTargets.Add(enemy);
                            }
                        }
                    }
                }
            }

            for (int k = 0; k < availableTargets.Count; k++)
            {
                float distanceFromTarget = Vector3.Distance(targetTransform.position, availableTargets[k].transform.position);

                if (distanceFromTarget < shortesDistance)
                {
                    shortesDistance = distanceFromTarget;
                    nearestLockOnTarget = availableTargets[k];
                }

                if(inputManager.lockOnFlag)
                {
                    Vector3 relativeEnemyPosition = inputManager.transform.InverseTransformPoint(availableTargets[k].transform.position);
                    var distanceFromLeftTarget = relativeEnemyPosition.x;
                    var distanceFromRightTarget = relativeEnemyPosition.x;

                    if(relativeEnemyPosition.x <= 0.00 && distanceFromLeftTarget > shortesDistanceOfLeftTarget && availableTargets[k] != currentLockOnTarget)
                    {
                        shortesDistanceOfLeftTarget = distanceFromLeftTarget;
                        leftLockTarget = availableTargets[k];
                    }

                    else if(relativeEnemyPosition.x >= 0.00 && distanceFromRightTarget < shortesDistanceOfRightTarget && availableTargets[k] != currentLockOnTarget)
                    {
                        shortesDistanceOfRightTarget = distanceFromRightTarget;
                        rightLockTarget = availableTargets[k];
                    }
                }
            }
        }
        public void ClearLockOnTargets()
        {
            availableTargets.Clear();
            nearestLockOnTarget = null;
            currentLockOnTarget = null;
            myTransform.rotation = Quaternion.Euler(Vector3.zero);
        }

        public void DestroyLockOntargets()
        {
            if(currentLockOnTarget == null)
            {
                inputManager.lockOnFlag = false;
                availableTargets.Clear();
                nearestLockOnTarget = null;
                currentLockOnTarget = null;
                if(!inputManager.playerStats.stateJiataData.isIndomitable) inputManager.playerAttacker.StopEffectLitubaxFx();         
            }
        }
        public void SetCameraHeight()
        {
            Vector3 velocity = Vector3.zero;
            Vector3 newLockedPosition = new Vector3 (0, lockedPivotPosition);
            Vector3 newUnlockedPosition = new Vector3(0, unlockedPivotPosition);

            if(currentLockOnTarget != null)
            {
                cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newLockedPosition, ref velocity, Time.deltaTime);
            }
            else
            {
                cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newUnlockedPosition, ref velocity, Time.deltaTime);
            }
        }

        public void ResetCameraPosition(float delta)
        {
            Vector3 targetPosition = targetTransformSecond.position;
            Vector3 targetDir = targetPosition - myTransform.position;
            targetDir.Normalize();
            targetDir.z = targetDir.y = 0;

            float rotationSpeed = 20f;
            float damping = 4.0f; // Ajuster l'amortissement pour la fluidité souhaitée

            Vector3 smoothedDir = Vector3.Lerp(myTransform.forward, targetDir, damping * delta);

            Quaternion targetRotation = Quaternion.LookRotation(smoothedDir);
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, rotationSpeed * delta);

            cameraPivotTransform.localRotation = Quaternion.Slerp(cameraPivotTransform.localRotation, targetRotation, rotationSpeed * delta);
        }
    }
}

