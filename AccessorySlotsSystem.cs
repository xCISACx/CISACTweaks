using CISACTweaks.Common.Configs;
using Terraria.ModLoader;

namespace CISACTweaks;

using Player = Terraria.Player;

public sealed class AccessorySlotSystem : ModSystem
{
    /*// To see how this config option was added, see ExampleModConfig.cs
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModContent.GetInstance<MyModConfig>().ExtraSlotsToggle;
    }*/

    public override void Load()
    {
        On.Terraria.Player.IsAValidEquipmentSlotForIteration += On_IsAValidEquipmentSlotForIteration;
        On.Terraria.Player.CanMasterModeAccessoryBeShown += On_CanMasterModeAccessoryBeShown;
        On.Terraria.Player.CanDemonHeartAccessoryBeShown += On_CanDemonHeartAccessoryBeShown;
    }


    public override void Unload()
    {
        On.Terraria.Player.IsAValidEquipmentSlotForIteration -= On_IsAValidEquipmentSlotForIteration;
        On.Terraria.Player.CanMasterModeAccessoryBeShown -= On_CanMasterModeAccessoryBeShown;
        On.Terraria.Player.CanDemonHeartAccessoryBeShown -= On_CanDemonHeartAccessoryBeShown;
    }

    private bool On_CanMasterModeAccessoryBeShown(On.Terraria.Player.orig_CanMasterModeAccessoryBeShown orig, Player self)
    {
        return true || orig.Invoke(self);
    }

    private bool On_CanDemonHeartAccessoryBeShown(On.Terraria.Player.orig_CanDemonHeartAccessoryBeShown orig, Player self)
    {
        return true || orig.Invoke(self);
    }

    private bool On_IsAValidEquipmentSlotForIteration(On.Terraria.Player.orig_IsAValidEquipmentSlotForIteration orig, Player self, int slot)
    {
        bool shouldBeValid = false;
        
        if (ModContent.GetInstance<MainConfig>().MasterSlotToggle) {
            shouldBeValid |= slot is 8;
        }
        if (ModContent.GetInstance<MainConfig>().DemonSlotToggle) {
            shouldBeValid |= slot is 9;
        }

        if (slot is 10) return true;
        
        return shouldBeValid | orig.Invoke(self, slot);
    }
}