using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_Skill : BaseSkill
{
    protected override KeyCode SkillKey => KeyCode.U;

    protected override string GroundAnimationTrigger => "USkill";

    protected override string AirAnimationName => "K+U";
}
