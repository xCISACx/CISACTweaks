using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace CISACTweaks.Common.Configs
{
	[Label("Settings")]
	public class MainConfig : ModConfig
	{
		// ConfigScope.ClientSide should be used for client side, usually visual or audio tweaks.
		// ConfigScope.ServerSide should be used for basically everything else, including disabling items or changing NPC behaviours
		public override ConfigScope Mode => ConfigScope.ServerSide;
		
		// The "$" character before a name means it should interpret the name as a translation key and use the loaded translation with the same key.
		// The things in brackets are known as "Attributes".
		[Header("$Mods.CISACTweaks.Config.DropsHeader")] // Headers are like titles in a config. You only need to declare a header on the item it should appear over, not every item in the category.
		[Label("$Mods.CISACTweaks.Config.NoDropRestrictionsToggle.Label")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
		[Tooltip("$Mods.CISACTweaks.Config.NoDropRestrictionsToggle.Tooltip")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
		[DefaultValue(true)] // This sets the configs default value.
		[ReloadRequired]
		public bool NoDropRestrictionsToggle;
		
		[Label("$Mods.CISACTweaks.Config.NormalModeBossBagsToggle.Label")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
		[Tooltip("$Mods.CISACTweaks.Config.NormalModeBossBagsToggle.Tooltip")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
		[DefaultValue(true)] // This sets the configs default value.
		[ReloadRequired]
		public bool NormalModeBossBagsToggle;

		// The "$" character before a name means it should interpret the name as a translation key and use the loaded translation with the same key.
		// The things in brackets are known as "Attributes".
		[Header("$Mods.CISACTweaks.Config.SlotsHeader")] // Headers are like titles in a config. You only need to declare a header on the item it should appear over, not every item in the category.
		[Label("$Mods.CISACTweaks.Config.MasterSlotToggle.Label")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
		[Tooltip("$Mods.CISACTweaks.Config.MasterSlotToggle.Tooltip")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
		[DefaultValue(true)] // This sets the configs default value.
		[ReloadRequired]
		public bool MasterSlotToggle; // To see the implementation of this option, see ExampleWings.cs

		[Label("$Mods.CISACTweaks.Config.DemonSlotToggle.Label")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
		[Tooltip("$Mods.CISACTweaks.Config.DemonSlotToggle.Tooltip")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
		[DefaultValue(true)] // This sets the configs default value.
		[ReloadRequired]
		public bool DemonSlotToggle; // To see the implementation of this option, see ExampleWings.cs
		
		[Header("$Mods.CISACTweaks.Config.ProjectilesHeader")] // Headers are like titles in a config. You only need to declare a header on the item it should appear over, not every item in the category.
		[Label("$Mods.CISACTweaks.Config.DisableProjectilesThroughWallsToggle.Label")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
		[Tooltip("$Mods.CISACTweaks.Config.DisableProjectilesThroughWallsToggle.Tooltip")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
		[DefaultValue(true)] // This sets the configs default value.
		public bool DisableProjectilesThroughWallsToggle; // To see the implementation of this option, see ExampleWings.cs
	}
}