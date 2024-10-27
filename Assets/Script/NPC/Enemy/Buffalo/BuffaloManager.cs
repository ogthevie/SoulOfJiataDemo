using System.Collections;
using UnityEngine;
using SJ;

public class BuffaloManager : EnemyManager
{
    //buffalo a l'armure si et seulement si le feu est autour de lui
    public BuffaloPattern buffaloPattern;
    [SerializeField] StoryManager storyManager;
    BossHealthBar bossHealthBar;
    [SerializeField] GameObject chargingPop, fakeCrane, trueCrane, kaoPortalFx, limitBoss, portalActivate;
    [SerializeField]protected CameraShake cameraShake;
    //public GameObject [] Torche = new GameObject [8];
    public int buffaloHealth;
    public bool iStun, isTiming, isArmor, isHellbow, isReady;
    public ParticleSystem plotArmorFx, summonKossiFx, spawnKaoFx;
    public Collider plotCollider, kaoPortalCollider;
    //public ParticleSystem ragePS, breastPS, chargePS;
    [SerializeField] Material kaoPortalMaterial;
    
    private void Awake() 
    {
        cameraManager = FindFirstObjectByType<CameraManager>();
        buffaloPattern = GetComponent<BuffaloPattern>();
        playerAttacker = FindFirstObjectByType<PlayerAttacker>();
        bossHealthBar = GetComponent<BossHealthBar>();
        detectionRadius = 60f;
        buffaloHealth = 700;
        storyManager = FindFirstObjectByType<StoryManager>();

        currentHealth = buffaloHealth;
        if(storyManager.storyStep > 5) 
        {
            plotCollider.enabled = false;
            HandlePortal();
            Destroy(this.gameObject);
        }
        //ragePS = rageFx.GetComponent<ParticleSystem>();
        //breastPS = breastFx.GetComponent<ParticleSystem>();
        //chargePS = chargeFx.GetComponent<ParticleSystem>();     
    }

    private void Start()
    {
        cameraShake = FindFirstObjectByType<CameraShake>();
        if(storyManager.storyStep >= 6) HandlePortal();
    }

    IEnumerator StartLifeKao()
    {
        PlayerManager playerManager = playerAttacker.GetComponent<PlayerManager>();
        chargingPop.SetActive(true);
        limitBoss.SetActive(true);
        fakeCrane.SetActive(true);
        StartCoroutine(playerManager.tutoManager.HandleToggleTipsUI("... "));
        cameraShake.Shake(4f, 0.25f);
        yield return new WaitForSeconds(4f);
        isReady = true;
        yield return new WaitForSeconds(3f);
        bossHealthBar.enabled = true;
        bossHealthBar.bossHUD.SetActive(true);
        storyManager.storyStep = 5;
        StartCoroutine(storyManager.gameManager.StartHandleToDo(5));
    }

    private void Update() 
    {
        if(!isReady && !chargingPop.activeSelf)
        {
            float distance = Vector3.Distance(transform.position, playerAttacker.transform.position);
            if(distance < 30) StartCoroutine(StartLifeKao());
        }

        if(isDead || !isReady) return;

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
            kaoPortalCollider.enabled = false;
            Destroy(buffaloPattern);
            yield return new WaitForSeconds(1.5f);
            fakeCrane.SetActive(false);
            bossHealthBar.bossHUD.SetActive(false);
            isHellbow = isArmor = false;
            FindFirstObjectByType<GrotteKossiManager>().GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(5f);
            trueCrane.SetActive(true);
            HandlePortal();
            var bomboktanManager = FindFirstObjectByType<BomboktanManager>();
            StartCoroutine(bomboktanManager.SpawnBomboktan(3));
        }
    }

    private void HandlePortal()
    {

        //for(int k = 0; k < 8; k++) Torche[k].transform.GetChild(0).gameObject.SetActive(true);
        limitBoss.SetActive(false);
        kaoPortalCollider.enabled = false;
        portalActivate.SetActive(true);
        kaoPortalCollider.transform.GetChild(0).GetComponent<Renderer>().material = kaoPortalMaterial;
        var materials = kaoPortalCollider.transform.GetChild(1).GetComponent<Renderer>().materials;
        materials[1] = kaoPortalMaterial;
        kaoPortalCollider.transform.GetChild(1).GetComponent<Renderer>().materials = materials;
        if(this != null) Destroy(this);
    }

}
