using HarmonyLib;
using System;
using System.Reflection;
using RimWorld;
using Verse;
namespace GermanFemaleJobs
{
  [StaticConstructorOnStartup]
  public static class GermanFemaleJobs
  {
    static GermanFemaleJobs()
    {
      var harmony = new Harmony("GermanFemaleJobs");
      harmony.PatchAll(Assembly.GetExecutingAssembly());
      Harmony.DEBUG = true;
    }

    // #################################### //
    // Enable female work type definitions. //
    // #################################### //
    [HarmonyPatch(typeof(RimWorld.CharacterCardUtility), "GetWorkTypesDisabledByWorkTag")]
    public class CharacterCardUtility_GetWorkTypesDisabledByWorkTag
    {
      [HarmonyPostfix]
      public static void Postfix(ref string __result, ref WorkTags workTag)
      {
        Pawn SelPawn = Find.Selector.SingleSelectedThing as Pawn;
        if (SelPawn.gender == Gender.Female)
        { 
          string[] newResult = __result.Split(new string[1] { "\n" }, StringSplitOptions.None);
          int i = 1;
          foreach (WorkTypeDef allDef in DefDatabase<WorkTypeDef>.AllDefs)
          {
            if ((allDef.workTags & workTag) > WorkTags.None)
            {
              newResult[i] = newResult[i].Replace(allDef.pawnLabel, (allDef.defName + ".pawnLabelFemale").Translate());
              i++;
            }
          }
          __result = String.Join("\n", newResult);
        }
      }
    }    
  }  
}
