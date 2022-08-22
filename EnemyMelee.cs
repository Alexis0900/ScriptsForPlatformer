using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour, EnemyController
{
    
    // ----------------------------------------------
    // Variable Parameters
    

    public int maxHealth = 100;
    public int currentHealth;
    private float gravity = 25f;
    //private float jumpSpeed = 1.6f; // might make them jump later
    public float moveSpeed = 6f;
    


    // ----------------------------------------------
    // state booleans
    public bool isFlying = false;
    public bool isPatrolling = true;
    public bool looksAtPlayer = false;
    private bool isDead = false;
    

    // ----------------------------------------------
    // Objects we need to reference
    private GameObject player;
    private CharacterController enemyController;
    private Animator animator;
    public ReturnAnimator animationChild;

    public HealthBar healthBar;

    public MeleeCollider weaponCollider;



    // ----------------------------------------------
    // walkpoints for patrolling
    public GameObject leftWalkpoint;
    public GameObject rightWalkpoint;
    private GameObject patrollTarget;



    // ----------------------------------------------
    // Attacking variables
    public float timeBetweenAttacks;
    private bool alreadyAttacked;


    // ----------------------------------------------
    // Sight and Attack ranges
    public float sightRange;
    public float attackRange;
    private bool playerInSightRange;
    private bool playerInAttackRange;

    private bool isAttacking = false;


    // ----------------------------------------------
    // Animation States
    private string currentState;
    const string ENEMY_IDLE = "Idle";
    const string ENEMY_WALK = "Walking";
    const string ENEMY_ATTACK = "Attack";
    const string ENEMY_DIE = "Dying";



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemyController = GetComponent<CharacterController>();
        animator = animationChild.GetComponent<Animator>();


        if(isPatrolling == true){
            if(Random.Range(1,3) == 1){
                patrollTarget = leftWalkpoint;
            }
            else{
                patrollTarget = rightWalkpoint;
            }
        }

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {   
        if(currentHealth <= 0){
            Death();
        }

        if(isDead == false){
            if(alreadyAttacked == false){
                if(distanceFromPlayer() <= sightRange){
                    playerInSightRange = true;
                }
                else{
                    playerInSightRange = false;
                }

                if(distanceFromPlayer() <= attackRange){
                    playerInAttackRange = true;
                }
                else{
                    playerInAttackRange = false;
                }



                if(playerInSightRange == false && playerInAttackRange == false && isPatrolling == true){
                    Patrolling();
                }

                if((playerInSightRange == true && playerInAttackRange == false)){
                    FollowPlayer();
                }

                if(playerInSightRange == true && playerInAttackRange == true){
                    Attack();
                }
            
            }
        }

        // if flying = gravity doesnt matter
        if(isFlying==false){
            
            Vector3 direction = new Vector3(0,0,0);

            direction.y -= gravity * Time.deltaTime;

            enemyController.Move(direction);
        }

    }

    float distanceFromPlayer()
    {
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y));

        return distance;
    }

    public void Patrolling()
    {
        Vector3 direction = new Vector3(0,0,0);

        Vector3 patrollPoint = patrollTarget.transform.position;

        if(transform.position.x - patrollPoint.x > 0){
            direction.x = -1;
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else{
            direction.x = 1;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        // change target
        if(Mathf.Abs(transform.position.x - patrollPoint.x) <= 1){
           
           if(patrollTarget == leftWalkpoint){
               patrollTarget = rightWalkpoint;
           }
           else{
               patrollTarget = leftWalkpoint;
           }
        }

        enemyController.Move(direction * Time.deltaTime * moveSpeed);

        ChangeAnimationState(ENEMY_WALK);
    }

    public void FollowPlayer()
    {
        Vector3 direction = new Vector3(0,0,0);
        isPatrolling = false;
        sightRange = 30 ;
        
        // follow player 

        if(transform.position.x - player.transform.position.x > 0){
            direction.x = -1;
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else{
            direction.x = 1;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        // can't float after the player if it's flying
        if(isFlying == true){
            if(transform.position.y - player.transform.position.y > 0){
                direction.y = -1;
            }
            else{
                direction.y = 1;
            }
        }
        
        //look at player
        if(looksAtPlayer == true){
            transform.LookAt(player.transform);
        }

        enemyController.Move(direction * Time.deltaTime * moveSpeed);
        
        ChangeAnimationState(ENEMY_WALK);
    }

    public void Attack()
    {
        if(alreadyAttacked == false){
            weaponCollider.gameObject.SetActive(true);

            ChangeAnimationState(ENEMY_ATTACK);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        weaponCollider.gameObject.SetActive(false);
    }

    void ChangeAnimationState(string newState)
    {
        // dont self interrupt
        if(currentState == newState)
            return;

        //play the animation
        animator.Play(newState);

        //save new state
        currentState = newState;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void Death()
    {
        isDead = true;
        gameObject.layer = 2;
        //Debug.Log("enemy dead");
        //enemyController.height = 0;
        ChangeAnimationState(ENEMY_DIE);

        Invoke(nameof(Death_2), 3.5f);
        //play death animation
        //wait time
        //destroy object
        //summon death particles;
    }

    void Death_2()
    {
        //summonparticles
        Destroy(gameObject);
    }
}
