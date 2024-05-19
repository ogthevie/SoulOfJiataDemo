using System.Collections;
using UnityEngine;
using SJ;

public class BuffaloManager : EnemyManager
{
    //buffalo a l'armure si et seulement si le feu est autour de lui
    public BuffaloPattern buffaloPattern;
    [SerializeField] ThunderEventManager thunderEventManager;
    BossHealthBar bossHealthBar;

    public int buffaloHealth;
    public bool iStun, isTiming, isArmor, isHellbow;
    public ParticleSystem plotArmorFx, summonKossiFx;
    public Collider plotCollider;
    //public ParticleSystem ragePS, breastPS, chargePS;
    
    private void Awake() 
    {
        cameraManager = FindObjectOfType<CameraManager>();
        buffaloPattern = GetComponent<BuffaloPattern>();
        playerAttacker = FindObjectOfType<PlayerAttacker>();
        bossHealthBar = GetComponent<BossHealthBar>();
        thunderEventManager = FindObjectOfType<ThunderEventManager>();
        detectionRadius = 60f;
        buffaloHealth = 700;

        currentHealth = buffaloHealth;
        //ragePS = rageFx.GetComponent<ParticleSystem>();
        //breastPS = breastFx.GetComponent<ParticleSystem>();
        //chargePS = chargeFx.GetComponent<ParticleSystem>();     
    }

    private void Update() 
    {
        if(isDead) return;

        float delta = Time.deltaTime;
        buffaloPattern.HandleTimerAttack(delta);
        buffaloPattern.DisableStun(delta);

        if(isHellbow) buffaloPattern.HandleHellBow(delta);

        if(buffaloPattern.currentTarget == null) buffaloPattern.HandleDetection();
        else buffaloPattern.HandleMoveToTarget();        
    }

    public override void TakeDamage(int damage)
    {
        isArmor = false;

        currentHealth -= damage;
        //Debug.Log(currentHealth);

        bossHealthBar.SetCurrentHealth(currentHealth);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            thunderEventManager.HandleActivationCraneEvent();
            thunderEventManager.bossThemeAudioSource.Stop();
            buffaloPattern.agentBuffalo.enabled = false;
            isDead = true;
        }
    }

    protected override void HandleDeath()
    {
        if(isDead)
        {
            StartCoroutine(OnDeath());
            Transform transformParent = buffaloPattern.stele.transform;
            //parcourir toute l'aborescence de stele
            //pour chaque enfant ayant le kossikaze manager, detruire l'enfant.
            foreach (Transform child in transformParent.GetComponentsInChildren<Transform>())
            {
                if(child.TryGetComponent<kossiKazeManager>(out kossiKazeManager component))
                    component.kossiKazePattern.HandleExplosion();
            }
        }

        IEnumerator OnDeath()
        {
            buffaloPattern.DisablePlotArmor();
            Destroy(buffaloPattern);
            yield return new WaitForSeconds(1.5f);
            bossHealthBar.bossHUD.SetActive(false);
            isHellbow = isArmor = false;
            Destroy(this, 3f);
        }
    }

}
