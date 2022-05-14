using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


    public class Player : MonoBehaviour
{

    //Move Speed Normal
    public float moveSpeed;


    //Door & Key objects
    public GameObject theDoor;
    public GameObject theKey;
    public GameObject theKey2;
    public GameObject theKey3;

    public GameObject victoryDoor;

        //Key counter
        int KeyCounter = 0;


    //Health Bar
    public healthBarScript healthBar;

    //Collission With Enemy
    void OnTriggerEnter2D(Collider2D collision)
    {
       //Collision With Enemy
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
            
            //Knockback
            Vector2 difference = transform.position - collision.transform.position;
            transform.position = new Vector2(transform.position.x + difference. x, transform.position.y + difference.y);
        }

        //Collision With Key
        if(collision.gameObject.name == "Key1")
        {
            KeyCounter += 1;
            Debug.Log(KeyCounter);
           
            //Removing the key
            theKey.gameObject.SetActive(false);
        }
        if(collision.gameObject.name == "Key2")
        {
            KeyCounter += 1;
            Debug.Log(KeyCounter);
           
            //Removing the key
            theKey2.gameObject.SetActive(false);
        }
        if(collision.gameObject.name == "Key3")
        {
            KeyCounter += 1;
            Debug.Log(KeyCounter);

           Debug.Log("Test works");
            //Removing the key
            theKey3.gameObject.SetActive(false);
        }

        //Collision if you win!
        if(collision.gameObject.name == "Victory")
        {
            Debug.Log("Victory!");
            SceneManager.LoadScene("WinScreen");
        }
    }

    //Opening the door
    void doorOpen()
    {     
        theDoor.gameObject.SetActive(false);
        
    }

    //Change movement direction
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;

    //Health Bar Stuff
    public int maxHealth = 3;
    public int currentHealth;

    //Stamina Bar Stuff
    private int maxStamina = 60;
    private int currentStamina;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);

    //Sets up StaminaBar at start of game
    void Start()
    {
        //Stamina Bar
        currentStamina = maxStamina;
        moveSpeed = 2f;

        //Health Bar
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    //Updates every seconds
    void Update()
    {
        //Checks movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        //Check for Key's
        if (KeyCounter == 3)
        {
            doorOpen();
        }

        /* Debug code to take damage with space bar top test gameover screen
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
        */


       
        //Adding in Sprint button

       if (currentStamina > 0)
        {
            
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
               
                moveSpeed += 2f;
                currentStamina -= 10;
                staminaBarScript.instance.UseStamina(currentStamina);

                Debug.Log("Move Speed" + moveSpeed);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                moveSpeed -= 2f;
                Debug.Log("Move Speed" + moveSpeed);

            }
         
        }
        else
        {
            Debug.Log(currentStamina);
            Debug.Log(moveSpeed);
            if (currentStamina < 0)
            {
                currentStamina = 0;
            }
            moveSpeed = 2f;
        }
      


        //Updates staminaBar
        staminaBarScript.instance.UseStamina(currentStamina);
        StartCoroutine(RegenStamina());
    }

    //Damage
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth < 1)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    

    //Regens stamina
    private IEnumerator RegenStamina()
    {
        if (currentStamina > maxStamina)
        {
            yield return new WaitForSeconds(2);

            while (currentStamina < maxStamina)
            {
                currentStamina += maxStamina / 100;
                staminaBarScript.instance.UseStamina(currentStamina);
                yield return regenTick;
            }

        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}




