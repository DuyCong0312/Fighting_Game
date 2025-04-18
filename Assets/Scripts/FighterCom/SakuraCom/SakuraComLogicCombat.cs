using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SakuraComLogicCombat : MonoBehaviour
{
    private enum ComAction { None, Combo, Movement, Combat, Skill}
    ComAction currentAction = ComAction.None;

    private ComMovement move;
    private ComAttack attack;
    private ComUseSkill skill;
    private PlayerState playerState;
    private PlayerHealth health;
    private PlayerRage rage;
    private CheckGround groundCheck;
    private Transform player;
    private int comboStep = 0;

    private void Start()
    {
        move = GetComponent<ComMovement>();
        attack = GetComponent<ComAttack>();
        skill = GetComponent<ComUseSkill>(); 
        playerState = GetComponent<PlayerState>();
        health = GetComponentInParent<PlayerHealth>();
        rage = GetComponentInParent<PlayerRage>(); 
        groundCheck = GetComponent<CheckGround>();
        StartCoroutine(WaitForPlayers());
    }

    private IEnumerator WaitForPlayers()
    {
        while (GameObject.FindGameObjectsWithTag("Player").Length < 1)
        {
            yield return null;
        }

        FindPlayers();
    }

    private void FindPlayers()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        UtilityCom();
        ExecuteCurrentAction();
    }

    private void UtilityCom()
    {
        if (currentAction != ComAction.None) return;

        float comboScore = 0f;
        float moveScore = 0f;
        float combatScore = 0f;
        float skillScore = 0f;

        comboScore = Random.Range(0f, 100f);
        moveScore = Random.Range(0f, 100f);
        combatScore = Random.Range(0f, 100f);
        skillScore = Random.Range(0f, 100f);

        if (comboScore >= moveScore && comboScore >= combatScore && comboScore >= skillScore)
        {
            currentAction = ComAction.Combo;
        }
        else if (moveScore >= combatScore && moveScore >= skillScore)
        {
            currentAction = ComAction.Movement;
        }
        else if (combatScore >= skillScore)
        {
            currentAction = ComAction.Combat;
        }
        else
        {
            currentAction = ComAction.Skill;
        }
    }

    void ExecuteCurrentAction()
    {
        switch (currentAction)
        {
            case ComAction.Combo:
                FirstCombo();
                break;

            case ComAction.Movement:
                HandleMovement();
                currentAction = ComAction.None;
                break;

            case ComAction.Combat:
                HandleCombat();
                currentAction = ComAction.None;
                break;

            case ComAction.Skill:
                HandleSkill();
                currentAction = ComAction.None;
                break;
        }
    }

    private void HandleMovement()
    {
        float DistanceToPlayerX = Mathf.Abs(player.transform.position.x - transform.position.x);
        bool IsPlayerAbove = player.transform.position.y > transform.position.y;

        if (IsPlayerAbove)
        {
            move.HandleJump();
            if (DistanceToPlayerX >= 7f)
            {
                move.HandleDash();
            }
            else if (DistanceToPlayerX >= 5f)
            {
                move.MoveToPlayer();
            }
        }
        else
        {
            if (DistanceToPlayerX >= 7f)
            {
                move.HandleDash();
            }
            else if (DistanceToPlayerX >= 5f)
            {
                move.MoveToPlayer();
            }
        }
    }

    private void HandleCombat()
    {
        float randomCombat = Random.Range(0f, 100f);
        float combatScore = (health.currentHealth > 25f) ? 60f : 20f;
        combatScore += randomCombat;

        if (combatScore >= 60f)
        {
            if (Vector2.Distance(player.position, transform.position) > 0.1f)
            {
                attack.Attack();
            }
            else
            {
                attack.StopAttack();
            }

            attack.StopDefend();
        }
        else if (combatScore >= 30f)
        {
            attack.Defend();
            attack.StopAttack();
        }
        else
        {
            attack.StopAttack();
            attack.StopDefend();
        }
    }

    private void HandleSkill()
    {
        float DistanceToPlayerX = Mathf.Abs(player.transform.position.x - transform.position.x);
        bool IsPlayerAbove = player.transform.position.y > transform.position.y;

        if (rage.currentRage > 35f)
        {
            if (DistanceToPlayerX < 2f && groundCheck.isGround)
            {
                skill.PlayISkill();
            }
            else if (DistanceToPlayerX < 1f && !groundCheck.isGround)
            {
                skill.PlayIKSkill();
            }
        }
        else
        {
            if (DistanceToPlayerX > 2f
                && groundCheck.isGround)
            {
                skill.PlayUskill();
            }
            else if (!groundCheck.isGround)
            {
                skill.PlayUKskill();
            }
        }
    }
    private void FirstCombo()
    {
        bool isSkillEnd = playerState.isUsingSkill ? false : true;

        switch (comboStep)
        {
            case 0: 
                if (comboStep == 0)
                {
                    skill.PlayUskill();
                    comboStep++;
                }
                break;
            case 1: 
                if (isSkillEnd)
                {
                    move.HandleDash();
                    comboStep++;
                }
                break;
            case 2: 
                if (!move.isDashing)
                {
                    move.HandleJump();
                    comboStep++;
                }
                break;
            case 3: 
                if (comboStep == 3 && !isSkillEnd)
                {
                    skill.PlayUKskill();
                    comboStep = 0;
                    currentAction = ComAction.None;
                }
                break;
        }
    }

}
