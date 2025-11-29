using SandBox.GameComponents;
using StoryMode;
using StoryMode.StoryModeObjects;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace InstantDeath
{
    internal class InstantDeath : SandboxAgentDecideKilledOrUnconsciousModel
    {
        public override float GetAgentStateProbability(Agent affectorAgent, Agent effectedAgent, DamageTypes damageType, WeaponFlags weaponFlags, out float useSurgeryProbability)
        {
            useSurgeryProbability = 1f;
            if (Agent.Main != null && effectedAgent == Agent.Main)
            {
                return 1F;
            }
            else if (StoryModeManager.Current != null)
            {
                useSurgeryProbability = 1f;
                if (effectedAgent.Character.IsHero && (effectedAgent.Character == StoryModeHeroes.ElderBrother.CharacterObject || effectedAgent.Character == StoryModeHeroes.Radagos.CharacterObject || effectedAgent.Character == StoryModeHeroes.RadagosHenchman.CharacterObject) && !StoryModeManager.Current.MainStoryLine.IsCompleted)
                {
                    return 0f;
                }

                if (!StoryModeManager.Current.MainStoryLine.TutorialPhase.IsCompleted && Mission.Current.GetMemberCountOfSide(effectedAgent.Team.Side) > 4)
                {
                    return 0f;
                }

                return base.GetAgentStateProbability(affectorAgent, effectedAgent, damageType, weaponFlags, out useSurgeryProbability);
            }
            else
            {
                return base.GetAgentStateProbability(affectorAgent, effectedAgent, damageType, weaponFlags, out useSurgeryProbability);
            }
        }
    }
}