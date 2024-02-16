// Decompiled with JetBrains decompiler
// Type: NoBotFriendlyFire.Plugin
// Assembly: NoBotFriendlyFire, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6566B244-9EC9-4C3A-984F-95045670A884
// Assembly location: C:\Users\djrid\AppData\Roaming\r2modmanPlus-local\GTFO\profiles\Whatever\BepInEx\plugins\hirnukuono-NoBotFriendlyFire\NoBotFriendlyFire.dll

using BepInEx;
using BepInEx.Unity.IL2CPP;
using Gear;
using HarmonyLib;
using UnityEngine;

#nullable enable
namespace NoBotFriendlyFire
{
  [BepInPlugin("NoBotFriendlyFire", "NoBotFriendlyFire", "0.0.1")]
  public class Plugin : BasePlugin
  {
    public virtual void Load() => new Harmony("NoBotFriendlyFire").PatchAll();

    [HarmonyPatch]
    private class NoBotFriendlyFire_Patches
    {
      [HarmonyPrefix]
      [HarmonyPatch(typeof (BulletWeapon), "BulletHit")]
      public static bool BulletHit(
        BulletWeapon __instance,
        Weapon.WeaponHitData weaponRayData,
        bool doDamage,
        float additionalDis = 0.0f,
        uint damageSearchID = 0)
      {
        RaycastHit rayHit1 = weaponRayData.rayHit;
        GameObject gameObject = ((Component) ((RaycastHit) ref rayHit1).collider).gameObject;
        RaycastHit rayHit2 = weaponRayData.rayHit;
        if (Object.op_Inequality((Object) ((RaycastHit) ref rayHit2).collider, (Object) null))
        {
          RaycastHit rayHit3 = weaponRayData.rayHit;
          if (Object.op_Inequality((Object) ((Component) ((RaycastHit) ref rayHit3).collider).gameObject, (Object) null) && weaponRayData.owner.Owner.IsBot)
          {
            RaycastHit rayHit4 = weaponRayData.rayHit;
            if (CustomExtensions.IsInLayerMask(((Component) ((RaycastHit) ref rayHit4).collider).gameObject, LayerMask.op_Implicit(LayerMask.GetMask(new string[1]
            {
              "PlayerSynced"
            }))))
            {
              doDamage = false;
              return false;
            }
          }
        }
        return true;
      }
    }
  }
}
