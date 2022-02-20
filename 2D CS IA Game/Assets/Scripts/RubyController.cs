using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{

    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    public int health { get { return currentHealth; }}
    public int currentHealth; 

    Rigidbody2D rigidbody2D;
    float Horizontal;
    float Vertical;

    public float movementSpeed = 5.0f;

    bool isInvincible;
    float invincibleTimer;

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    public GameObject projectilePrefab;

    public ParticleSystem hitEffect;
    AudioSource audioSource;

    public AudioClip throwSound;
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
         Horizontal = Input.GetAxis("Horizontal");
         Vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(Horizontal, Vertical);

        if (!Mathf.Approximately(move.x, 0.5f) || !Mathf.Approximately(move.y, 0.5f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);


        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
            PlaySound(throwSound);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D rayhit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (rayhit.collider != null)
            {
                NonPlayerCharacter character = rayhit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                    character.DisplayDialog();
            }
        }

        

    }

     void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        position.x = position.x + movementSpeed * Horizontal * Time.deltaTime;
        position.y = position.y + movementSpeed * Vertical * Time.deltaTime;

        rigidbody2D.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

            animator.SetTrigger("Hit");
            PlaySound(hitSound);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        HealthBarUI.instance.SetValue(currentHealth / (float)maxHealth);

      

    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

    }

    public void PlaySound(AudioClip clip) 
    {
        audioSource.PlayOneShot(clip);
    }

   



}
