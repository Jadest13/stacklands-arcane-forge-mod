using HarmonyLib;
using System;
using UnityEngine;

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

        //[HarmonyPatch(typeof(SokLoc), "LoadTermsFromFile")]
        //[HarmonyPostfix]
        //public static void Tmp(SokLoc __instance, string path, bool disableWarning = false)
        //{
        //    if (!path.Contains("ArcaneForge")) return;
        //    string[][] array = SokLoc.ParseTableFromTsv(File.ReadAllText(path));
        //    int languageColumnIndex = SokLoc.GetLanguageColumnIndex(array, __instance.CurrentLanguage);
        //    Debug.Log(path + ", " + languageColumnIndex);
        //    if (languageColumnIndex == -1)
        //    {
        //        return;
        //    }
        //    for (int i = 1; i < array.Length; i++)
        //    {
        //        string term = array[i][0];
        //        Debug.Log(i + ", " + term);
        //        string fullText = array[i][languageColumnIndex];
        //        term = term.Trim().ToLower();
        //        if (!string.IsNullOrEmpty(term))
        //        {
        //            SokTerm sokTerm = new SokTerm(__instance.CurrentLocSet, term, fullText);
        //            Debug.Log(sokTerm.GetText());
        //            if (__instance.CurrentLocSet.TermLookup.ContainsKey(term))
        //            {
        //                if (!disableWarning)
        //                {
        //                    Debug.LogError("Term " + term + " has been found more than once in the localisation sheet. Using last item in sheet.");
        //                }
        //                __instance.CurrentLocSet.TermLookup[term] = sokTerm;
        //                __instance.CurrentLocSet.AllTerms.RemoveAll((SokTerm x) => x.Id == term);
        //                __instance.CurrentLocSet.AllTerms.Add(sokTerm);
        //            }
        //            else
        //            {
        //                __instance.CurrentLocSet.AllTerms.Add(sokTerm);
        //                __instance.CurrentLocSet.TermLookup.Add(term, sokTerm);
        //            }
        //            Debug.Log(__instance.CurrentLocSet.TermLookup[term] + ", " + __instance.CurrentLocSet.TermLookup.TryGetValue(term, out var value));
        //        }
        //    }
        //}
    }
}
