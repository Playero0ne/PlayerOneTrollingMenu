using System;
using System.ComponentModel;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using ModMenuPatch.HarmonyPatches;
using UnityEngine;

namespace ModMenuPatch
{
	// Token: 0x02000003 RID: 3
	[Description("HauntedModMenu")]
	[BepInPlugin("org.legoandmars.gorillatag.modmenupatch", "EyeKlown menu", "1.0.0")]
	public class ModMenuPatch : BaseUnityPlugin
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		private void OnEnable()
		{
			ModMenuPatches.ApplyHarmonyPatches();
			ConfigFile configFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "ModMonkeyPatch.cfg"), true);
			ModMenuPatch.speedMultiplier = configFile.Bind<float>("Configuration", "SpeedMultiplier", 100f, "How much to multiply the speed. 10 = 10x higher jumps");
			ModMenuPatch.jumpMultiplier = configFile.Bind<float>("Configuration", "JumpMultiplier", 1.5f, "How much to multiply the jump height/distance by. 10 = 10x higher jumps");
			ModMenuPatch.RandomColor = configFile.Bind<bool>("rgb_monke", "RandomColor", false, "Whether to cycle through colours of rainbow or choose random colors");
			ModMenuPatch.CycleSpeed = configFile.Bind<float>("rgb_monke", "CycleSpeed", 1f, "The speed the color cycles at each frame (1=Full colour cycle). If random colour is enabled, this is the time in seconds before switching color");
			ModMenuPatch.GlowAmount = configFile.Bind<float>("rgb_monke", "GlowAmount", 1f, "The brightness of your monkey. The higher the value, the more emissive your monkey is");
			ModMenuPatch.sp = configFile.Bind<float>("Configuration", "Spring", 10f, "spring");
			ModMenuPatch.dp = configFile.Bind<float>("Configuration", "Damper", 30f, "damper");
			ModMenuPatch.ms = configFile.Bind<float>("Configuration", "MassScale", 12f, "massscale");
			ModMenuPatch.rc = configFile.Bind<Color>("Configuration", "webColor", Color.white, "webcolor hex code");
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000223D File Offset: 0x0000043D
		private void OnDisable()
		{
			ModMenuPatches.RemoveHarmonyPatches();
		}

		// Token: 0x04000004 RID: 4
		public static bool allowSpaceMonke = true;

		// Token: 0x04000005 RID: 5
		public static ConfigEntry<float> multiplier;

		// Token: 0x04000006 RID: 6
		public static ConfigEntry<float> speedMultiplier;

		// Token: 0x04000007 RID: 7
		public static ConfigEntry<float> jumpMultiplier;

		// Token: 0x04000008 RID: 8
		public static ConfigEntry<bool> RandomColor;

		// Token: 0x04000009 RID: 9
		public static ConfigEntry<float> CycleSpeed;

		// Token: 0x0400000A RID: 10
		public static ConfigEntry<float> GlowAmount;

		// Token: 0x0400000B RID: 11
		public static ConfigEntry<float> sp;

		// Token: 0x0400000C RID: 12
		public static ConfigEntry<float> dp;

		// Token: 0x0400000D RID: 13
		public static ConfigEntry<float> ms;

		// Token: 0x0400000E RID: 14
		public static ConfigEntry<Color> rc;

		// Token: 0x0400000F RID: 15
		internal static object randomColor;

		// Token: 0x04000010 RID: 16
		internal static object glowAmount;
	}
}
