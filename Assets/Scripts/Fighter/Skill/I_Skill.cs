using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_Skill : BaseSkill
{
    protected override KeyCode SkillKey => KeyCode.I;

    protected override string GroundAnimationTrigger => "Iskill";

    protected override string AirAnimationName => "K+I";

    protected override void ActiveSkill()
    {
        if (Input.GetKeyDown(SkillKey))
        {
            if(playerRage.currentRage >= 30f)
            {
                playerRage.CostRage(30f);
                PerformSkill();
            }
            else
            {
                return;
            }
            
        }
    }
}
