using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

    [SerializeField] GameObject GunEndBarrel = default;
    PlayerHealth playerHealth;


    void Awake()
    {
        //GetMask
        shootableMask = LayerMask.GetMask("Shootable");

        //Mendapatkan Reference component
        gunParticles = GunEndBarrel.GetComponent<ParticleSystem>();
        gunLine = GunEndBarrel.GetComponent<LineRenderer>();
        gunAudio = GunEndBarrel.GetComponent<AudioSource>();
        gunLight = GunEndBarrel.GetComponent<Light>();
        playerHealth = GetComponent<PlayerHealth>();
    }


    void Update()
    {

        timer += Time.deltaTime;

        if(playerHealth.currentHealth <= 0)
        {
            return;
        }

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }


    public void DisableEffects()
    {
        //disable line renderer
        gunLine.enabled = false;

        //disable light
        gunLight.enabled = false;
    }


    public void Shoot()
    {
        timer = 0f;

        //Play audio
        gunAudio.Play();

        //enable Light
        gunLight.enabled = true;

        //Play gun particle
        gunParticles.Stop();
        gunParticles.Play();

        //Set posisi ray shoot dan direction
        shootRay.origin = GunEndBarrel.transform.position;
        shootRay.direction = transform.forward;

        //enable Line renderer dan set first position
        gunLine.enabled = true;
        gunLine.SetPosition(0, shootRay.origin);

        //Lakukan raycast jika mendeteksi id enemy hit apapun
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            //Lakukan raycast hit hace component Enemyhealth
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                //Lakukan Take Damage
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }

            //Set line end position ke hit position
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            //set line end position ke range freom barrel
            gunLine.SetPosition(1, shootRay.origin + (shootRay.direction * range));
        }
    }
}