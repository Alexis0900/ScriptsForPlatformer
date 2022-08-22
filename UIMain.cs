using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMain : MonoBehaviour
{
    public PlayerHealthBar healthBar;
    public HealthBar manaBar;

    public UIText goldText;
    public UIText critText;
    public UIText bonusCritText;
    public UIText attackSpeedText;
    public UIText bonusAttackSpeedText;


    public UIAbility ability1;
    public UIAbility ability2;
    public UIAbility ability3;
    

    public UIIcon passive1Icon;
    public UIIcon passive2Icon;


    public UIIcon playerPortrait;

    public UIText levelText;


    void Start()
    {
        levelText.SetText(SceneManager.GetActiveScene().name);
    }


    public PlayerHealthBar GetPlayerHealthBar()
    {
        return healthBar;
    }

    public HealthBar GetManaBar()
    {
        return manaBar;
    }
    


    public UIText GetGoldText()
    {
        return goldText;
    }

    public UIText GetCritText()
    {
        return critText;
    }

    public UIText GetBonusCritText()
    {
        return bonusCritText;
    }

    public UIText GetAttackSpeedText()
    {
        return attackSpeedText;
    }

    public UIText GetBonusAttackSpeedText()
    {
        return bonusAttackSpeedText;
    }



    public UIAbility GetAbility1()
    {
        return ability1;
    }

    
    public UIAbility GetAbility2()
    {
        return ability2;
    }
    
    public UIAbility GetAbility3()
    {
        return ability3;
    }


    public UIIcon GetPassive1()
    {
        return passive1Icon;
    }

    public UIIcon GetPassive2()
    {
        return passive2Icon;
    }

    public UIIcon GetPlayerPortrait()
    {
        return playerPortrait;
    }


}
