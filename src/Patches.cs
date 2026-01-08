using HarmonyLib;

namespace ArcaneForgeNS
{
	internal class Patches
    {

        [HarmonyPatch(typeof(WorldManager), "InitActionTimeBases")]
        [HarmonyPostfix]
        public static void AddActionTimeBaseForMiner(WorldManager __instance)
        {
            __instance.actionTimeBases.Add(new ActionTimeBase((ActionTimeParams p) => p.villager.Id == "miner" && p.actionId == "complete_harvest" && (p.baseCard.Id == Consts.Card(Consts.GEODE) || p.baseCard.Id == Consts.Card(Consts.MAGIC_DUST_MINE)), 0.5f));
        }

        [HarmonyPatch(typeof(CombatableHarvestable), "CanHaveCard")]
        [HarmonyPostfix]
        public static void CombatableHarvestableCanHaveCard(CombatableHarvestable __instance, CardData otherCard, ref bool __result)
        {
            string baseId = __instance.Id;
            bool isMine = baseId == "quarry" || baseId == "mine" || baseId == "gold_mine" || baseId == "sand_quarry" || baseId == Consts.Card(Consts.MAGIC_DUST_MINE);
            __result = __result || (isMine && otherCard.Id == Consts.Card(Consts.ORE_EXCAVATOR));
        }

        [HarmonyPatch(typeof(EnergyHarvestable), "CanHaveCard")]
        [HarmonyPostfix]
        public static void EnergyHarvestableCanHaveCard(EnergyHarvestable __instance, CardData otherCard, ref bool __result)
        {
            string baseId = __instance.Id;
            bool isMine = baseId == "gravel_pit" || baseId == "copper_mine" || baseId == "uranium_mine";
            __result = __result || (isMine && otherCard.Id == Consts.Card(Consts.ORE_EXCAVATOR));
        }
    }
}
