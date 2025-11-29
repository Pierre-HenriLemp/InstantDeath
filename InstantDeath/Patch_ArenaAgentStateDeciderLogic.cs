using HarmonyLib;
using SandBox.Missions.MissionLogics.Arena;
using System;
using System.Reflection;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace InstantDeath
{
    internal class Patch_ArenaAgentStateDeciderLogic
    {
        private static readonly Harmony Harmony = new Harmony(Guid.NewGuid().ToString());
        private static bool _patched = false;
        public static bool Patch()
        {
            if (_patched)
            {
                return _patched;
            }
            try
            {
                Harmony.Patch(
                    typeof(ArenaAgentStateDeciderLogic).GetMethod(nameof(ArenaAgentStateDeciderLogic.GetAgentState), BindingFlags.Instance | BindingFlags.Public),
                    prefix: new HarmonyMethod(typeof(Patch_ArenaAgentStateDeciderLogic).GetMethod(
                            nameof(__GetAgentState), BindingFlags.Static | BindingFlags.Public)));
                InformationManager.DisplayMessage(new InformationMessage("Patched!"));
                _patched = true;
            } catch(Exception ex)
            {
                _patched = false;
                InformationManager.DisplayMessage(new InformationMessage("Fail to patch: " + ex.ToString()));
                return _patched;
            }
            return _patched;
        }

        public static bool __GetAgentState(Agent effectedAgent, float deathProbability, out bool usedSurgery, ref AgentState __result)
        {
            InformationManager.DisplayMessage(new InformationMessage("New code dalled"));
            usedSurgery = false;
            __result = AgentState.Killed;
            return false;
        }
    }
}
