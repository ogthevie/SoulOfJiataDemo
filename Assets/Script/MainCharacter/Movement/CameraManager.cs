using System.Collections.Generic;
using UnityEngine;

namespace SJ
{
    public class CameraManager : MonoBehaviour
    {
        #region variables
        public Transform targetTransform; //la position de l'objet à suivre
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

        public static CameraManager singleton;

        public float lookSpeed = 0.03f;
        public float followSpeed = 0.07f;
        public float pivotSpeed = 0.006f;

        private float targetPosition; 
        private float defautlPosition; //la position par défaut de la camera en Y
        private float lookAngle;
        private float pivotAngle;
        public readonly float minimumPivot = -10;
        public float maximumPivot = 90;

        public float cameraSphereRadius = 0.2f;
        public readonly float cameraCollisionOffset = 0.2f; //De combien la camera sera décalé en cas de collision
        public float minimumCollisionOffset = 0.1f;
        public float lockedPivotPosition = 2.25f;
        public float unlockedPivotPosition = 1.65f;


        public EnemyManager currentLockOnTarget;

        public List<EnemyManager>availableTargets = new();
        public float maximumLockOnDistance = 30;
        public EnemyManager nearestLockOnTarget;
        public EnemyManager leftLockTarget;
        public EnemyManager rightLockTarget;

        #endregion


        private void Awake()
        {

            singleton = this;
            myTransform = transform;
            defautlPosition = cameraTransform.localPosition.z;
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
            targetTransform = FindObjectOfType<PlayerManager>().transform;
            inputManager = FindObjectOfType<InputManager>();
            playerManager = FindObjectOfType<PlayerManager>();
            enemyManager = FindObjectOfType<EnemyManager>();
        }
        private void Start() 
        {
            environmentLayer = LayerMask.NameToLayer("Environment");
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

            if(Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetPosition)/*, ignoreLayers*/))
            {
                float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetPosition = -(dis - cameraCollisionOffset);
            }

            if(Mathf.Abs(targetPosition) < minimumCollisionOffset)
            {
                targetPosition = -minimumCollisionOffset;
            }

            cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
            cameraTransform.localPosition = cameraTransformPosition;
        }

        public void HandleLockOn()
        {
            float shortesDistance = Mathf.Infinity;
            float shortesDistanceOfLeftTarget = -Mathf.Infinity;
            float shortesDistanceOfRightTarget = Mathf.Infinity;

            Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26);

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
                            Debug.DrawLine(playerManager.lockOnTransform.position, enemy.lockOnTransform.position);

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
        }

        public void DestroyLockOntargets()
        {
            if(currentLockOnTarget == null)
            {
                inputManager.lockOnFlag = false;
                availableTargets.Clear();
                nearestLockOnTarget = null;
                currentLockOnTarget = null;         
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
    }
}

