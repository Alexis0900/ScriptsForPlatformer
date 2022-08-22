using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    
    public int playerType = 0;
    private int revives = 0;

    public GameObject UIObject;
    private UIMain mainUI;

    /// health
    public int maxHealth = 300;
    public int currentHealth;
    private PlayerHealthBar healthBar;

    /// mana
    public int maxMana = 200;
    public float currentMana;
    public int manaRegen = 20;
    private HealthBar manaBar;

    /// gold
    public int gold = 0;
    private UIText goldText;

    /// bonus stats
    public int bonusCrit = 0;
    public float bonusAttackSpeed = 0.1f;
    private UIText bonusCritText;
    private UIText bonusAttackSpeedText;


    /// weapon list and stats
    private int damage = 20;
    private int manaCost = 20;
    private int crit = 0;
    private float attackSpeed = 0.5f;
    private int projectileSpeed = 1000;
    private string weaponName;
    private UIText critText;
    private UIText attackSpeedText;


    // projectile si sprite
    private GameObject projectile;
    private Sprite weaponSprite;
    private UIAbility weaponUI;


    // weapon list    
    private int weaponIndex = 0;
    public List<Weapon> weaponList;
    public Weapon defaultWeapon;
    public Weapon[] weaponArray;

    // shooting the weapon
    public ShooterController shooter;
    private float nextAttack;


    // heal abilities
    public AbilityController healAbility;
    private int healCooldown;
    private float healWaitTime;
    private int healUses = 5;

    private UIAbility healUI;


    // bonus ability
    public AbilityController bonusAbility;
    private int bonusAbilityCooldown;
    private float bonusAbilityWaitTime;
    private int bonusAbilityAmmount;
    private UIAbility bonusAbilityUI;


    // passives
    public AbilityController passive1;
    public AbilityController passive2;

    private UIIcon passive1Icon;
    private UIIcon passive2Icon;

    // player icon

    public Sprite playerSprite;
    private UIIcon playerPortrait;

    // minimap godbless

    // you died menu

    private GameObject youDied;


    void Start()
    {   
        youDied = GameObject.FindWithTag("YOU DIED");


        UIObject = GameObject.FindWithTag("Player UI");
        mainUI = UIObject.GetComponent<UIMain>();


        healthBar = mainUI.GetPlayerHealthBar();
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth);


        manaBar = mainUI.GetManaBar();

        currentMana = maxMana;
        manaBar.SetMaxHealth(maxMana);
        manaBar.SetHealth(maxMana);


        goldText = mainUI.GetGoldText();
        GainGold(0);

        bonusCritText = mainUI.GetBonusCritText();
        GainBonusCrit(0);
        bonusAttackSpeedText = mainUI.GetBonusAttackSpeedText();
        GainBonusAttackSpeed(0);

        critText = mainUI.GetCritText();
        UpdateCritText();
        attackSpeedText = mainUI.GetAttackSpeedText();
        UpdateAttackSpeedText();


        

        weaponUI = mainUI.GetAbility2();

        weaponList = new List<Weapon>();
        
        AddWeapon(defaultWeapon);
        ChangeWeapon(0);
        nextAttack = Time.time;


        // abilities here
        // heal
        healUI = mainUI.GetAbility1();
        healUI.SetSprite(healAbility.abilitySprite);
        healUI.SetText(healAbility.abilityName);

        healCooldown = healAbility.abilityCooldown;
        healUI.ammount.gameObject.SetActive(true);
        healUI.ammount.SetText(healUses.ToString());

        healWaitTime = Time.time;
        

        // bonus ability
        bonusAbilityUI = mainUI.GetAbility3();
        bonusAbilityUI.SetSprite(bonusAbility.abilitySprite);
        bonusAbilityUI.SetText(bonusAbility.abilityName);

        bonusAbilityCooldown = bonusAbility.abilityCooldown;
        bonusAbilityWaitTime = Time.time;


        // passives
        passive1Icon = mainUI.GetPassive1();
        passive1Icon.SetSprite(passive1.abilitySprite);
        passive1Icon.SetText(passive1.abilityName);

        passive2Icon = mainUI.GetPassive2();
        passive2Icon.SetSprite(passive2.abilitySprite);
        passive2Icon.SetText(passive2.abilityName);


        // player icon

        playerPortrait = mainUI.GetPlayerPortrait();
        playerPortrait.SetSprite(playerSprite);



        if(playerType == 2){
            revives = 1;
        }
        //LoadPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
        // shooting the weapon
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {   
            if(nextAttack <= Time.time){
                if(manaCost <= currentMana){
                    nextAttack = Time.time + (float)(1/(attackSpeed+bonusAttackSpeed));
                    SpendMana(manaCost);
                    ShootWeapon();
                }
            }
            
        }
        
        // weapon change
        if(weaponList.Count > 1){
            if(Input.GetAxis("Mouse ScrollWheel") > 0){
                weaponIndex++;
                if(weaponIndex > weaponList.Count-1){
                    weaponIndex = 0;
                }

                ChangeWeapon(weaponIndex);

                Debug.Log("Weapon Index - " + weaponIndex);
            }
            else if(Input.GetAxis("Mouse ScrollWheel") < 0){
                weaponIndex--;
                if(weaponIndex < 0){
                    weaponIndex = weaponList.Count-1;
                }
                
                ChangeWeapon(weaponIndex);

                Debug.Log("Weapon Index - " + weaponIndex);
            }
        }

       
        // health potion ability
        if (Input.GetKeyDown("f"))
        {   
            
            if(healWaitTime <= 0){
                if(healUses > 0){

                    healUses--;
                    healUI.ammount.SetText(healUses.ToString());

                    healWaitTime = healCooldown;
                    healUI.cooldown.gameObject.SetActive(true);
                    healUI.cooldown.SetText(healWaitTime.ToString("F2"));

                    HealPlayer(300);
                }
            }
            //HealPlayer(300);
        }

        // heal cooldown
        if(healWaitTime > 0){
            
            healWaitTime -= Time.deltaTime;

            if(healWaitTime <= 0){
                healWaitTime = 0;
                healUI.cooldown.gameObject.SetActive(false);
            }

            healUI.cooldown.SetText(healWaitTime.ToString("F2"));
        }


        
        // bonus ability
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {   
            
            if(bonusAbilityWaitTime <= 0){

                bonusAbilityWaitTime = bonusAbilityCooldown;
                bonusAbilityUI.cooldown.gameObject.SetActive(true);
                bonusAbilityUI.cooldown.SetText(bonusAbilityWaitTime.ToString("F2"));

                switch(playerType)
                {
                    case 0:
                            GainMana(500);
                        break;
                    
                    case 1:
                            if(GetGold() >= 30 && currentMana >= 100){
            
                                SpendGold(30);
                                SpendMana(100);

                                GainBonusCrit(10);
                                GainBonusAttackSpeed(0.2f);
                            }
                        break;

                    case 2:
                            if(currentHealth > 60){
                                TakeDamage(60);
                                GainGold(50);
                            }
                        break;

                    default:

                        break;
                }
                
                //bonusAbility.Ability();

            }
        }

        // bonus ability cooldown
        if(bonusAbilityWaitTime > 0){
            
            bonusAbilityWaitTime -= Time.deltaTime;

            if(bonusAbilityWaitTime <= 0){
                bonusAbilityWaitTime = 0;
                bonusAbilityUI.cooldown.gameObject.SetActive(false);
            }

            bonusAbilityUI.cooldown.SetText(bonusAbilityWaitTime.ToString("F2"));
        }
        


        // Mana Regen
        if(currentMana < maxMana){
            GainMana(manaRegen * Time.deltaTime);
        }
        

    }



    // ----------------------------------------------
    // Shooting and damage methods

    void ShootWeapon()
    {   
        int finalDmg = damage * DamageMultiplier();

        shooter.ShootWeapon(finalDmg, projectileSpeed, projectile);
    }

    int DamageMultiplier()
    {   
        // for every 100% crit you gain 1 multiplier
        int multiplier = 1;
        int totalCrit = crit + bonusCrit;

        multiplier += (int) totalCrit/100;

        int chance = Random.Range(0,100);
        if(chance <= totalCrit%100){
            multiplier++;
        }

        return multiplier;
    }


    // ----------------------------------------------
    // Current Health
    public void TakeDamage(int damage)
    {
        Debug.Log("Player took damage = " + damage);

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        // call death function later
        if(currentHealth <= 0){

            if(revives > 0){
                revives--;
                HealPlayer(maxHealth);
            }
            else{
                youDied.transform.GetChild(0).gameObject.SetActive(true);
                
                GameObject levelManager = GameObject.FindWithTag("Level Manager");
                levelManager.GetComponent<LevelManager>().currentScene = " ";
                levelManager.GetComponent<LevelManager>().SaveLevel();

                Debug.Log("player is now dead ;-;");
            }
            
        }
    }

    public void HealPlayer(int hp)
    {
        Debug.Log("Player haled for - " + hp);

        currentHealth += hp;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }

        healthBar.SetHealth(currentHealth);
    }


    // ----------------------------------------------
    // Max Health

    public void IncreaseMaxHealth(int hp)
    {
        Debug.Log("Max health increased by - " + hp);

        maxHealth += hp;
        currentHealth += hp;

        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth);
    }

    // dont think I have a reason to decrease max HP yet


    // ----------------------------------------------
    // Mana

    public void SpendMana(int cost)
    {
        Debug.Log("Mana spent - " + cost);

        currentMana -= cost;
        manaBar.SetHealth((int) currentMana);
    }

    public void GainMana(float mana)
    {
        //Debug.Log("Mana gained - " + mana);

        currentMana += mana;

        if(currentMana > maxMana){
            currentMana = maxMana;
        }

        manaBar.SetHealth((int) currentMana);
    }

    public void IncreaseManaRegen(int extraRegen)
    {
        Debug.Log("Increased mana regen - " + extraRegen);

        manaRegen += extraRegen;
    }



    // ----------------------------------------------
    // Gold

    public int GetGold()
    {
        return gold;
    }

    public void SpendGold(int ammount)
    {
        Debug.Log("Gold spent - " + ammount);

        gold -= ammount;
        goldText.SetText("Gold: " + gold);
    }

    public void GainGold(int ammount)
    {
        Debug.Log("Gold gained - " + ammount);

        gold += ammount;
        goldText.SetText("Gold: " + gold);
    
        if(playerType == 2){
            HealPlayer(10);
        }
    }

    // ----------------------------------------------
    // Bonus crit and attack speed

    public void GainBonusCrit(int ammount)
    {
        Debug.Log("Bonus Crit gained - " + ammount);

        bonusCrit += ammount;
        bonusCritText.SetText("Bonus: " + bonusCrit + "%");
    }

    public void GainBonusAttackSpeed(float ammount)
    {
        Debug.Log("Bonus Attack Speed gained - " + ammount);

        bonusAttackSpeed += ammount;
        bonusAttackSpeedText.SetText("Bonus: " + bonusAttackSpeed);
    }

    // ----------------------------------------------
    // Update sprite, crit and attack speed values

    public void UpdateWeaponSprite()
    {
        weaponUI.SetSprite(weaponSprite);
        weaponUI.SetText(weaponName);
    }    

    public void UpdateCritText()
    {
        critText.SetText("Crit Chance: " + crit + "%");
    }

    public void UpdateAttackSpeedText()
    {
        attackSpeedText.SetText("Attack Speed: " + attackSpeed);
    }

    // ----------------------------------------------
    // Weapon sprite, damage, cost, crit and attack speed

    public void ChangeWeapon(int index)
    {
        Weapon newWeapon = weaponList[index];

        damage = newWeapon.damage;
        manaCost = newWeapon.manaCost;
        crit = newWeapon.crit;
        attackSpeed = newWeapon.attackSpeed;

        weaponSprite = newWeapon.weaponSprite;
        projectile = newWeapon.projectile;

        projectileSpeed = newWeapon.projectileSpeed;

        weaponName = newWeapon.weaponName;

        //updates
        UpdateCritText();
        UpdateAttackSpeedText();
        UpdateWeaponSprite();
    }

    public void AddWeapon(Weapon newWeapon)
    {
        IncreaseManaRegen(5);

        bool alreadyExists = false;

        foreach(Weapon i in weaponList){
            if(i.weaponName.Equals(newWeapon.weaponName)){
                alreadyExists = true;
                break;
            }
        }

        if(alreadyExists == false){
            weaponList.Add(newWeapon);
        }
    }

    // ----------------------------------------------
    // Update ability name, sprite and cooldown





    // ----------------------------------------------
    //  Save and Load

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }


    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        playerType = data.playerType;

        maxHealth = data.maxHealth;
        currentHealth = data.currentHealth;

        IncreaseMaxHealth(0);
        HealPlayer(0);


        maxMana = data.maxMana;
        manaRegen = data.manaRegen;

        
        gold = data.gold;
        bonusCrit = data.bonusCrit;
        bonusAttackSpeed = data.bonusAttackSpeed;

        GainGold(0);
        GainBonusCrit(0);
        GainBonusAttackSpeed(0);

        foreach(Weapon weaponPrefab in weaponArray){

           foreach(string name in data.weaponArray){

               if(weaponPrefab.weaponName.Equals(name)){
                   AddWeapon(weaponPrefab);
               }
           }
        }
    }



}
