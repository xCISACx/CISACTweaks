using System;
using System.Reflection;
using CISACTweaks.Common.Configs;
using IL.Terraria.ID;
using On.Terraria;
using On.Terraria.DataStructures;
using Terraria.ModLoader;
using Main = Terraria.Main;
using NPCID = On.Terraria.ID.NPCID;

public sealed class BetterLootSystem : ModSystem
{
    /*// To see how this config option was added, see ExampleModConfig.cs
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModContent.GetInstance<MyModConfig>().ExtraSlotsToggle;
    }*/
    
    private static readonly FieldInfo ExpertModeField;
    private static readonly FieldInfo MasterModeField;

    private static bool? OverrideExpertMode
    {
        get => (bool?) ExpertModeField.GetValue(null);
        set => ExpertModeField.SetValue(null, value);
    }

    private static bool? OverrideMasterMode
    {
        get => (bool?) MasterModeField.GetValue(null);
        set => MasterModeField.SetValue(null, value);
    }

    static BetterLootSystem()
    {
        ExpertModeField = typeof(Main).GetField("_overrideForExpertMode", BindingFlags.Static | BindingFlags.NonPublic) ?? throw new NullReferenceException();
        MasterModeField = typeof(Main).GetField("_overrideForMasterMode", BindingFlags.Static | BindingFlags.NonPublic) ?? throw new NullReferenceException();
    }

    private static void OnNPCLoot(NPC.orig_NPCLoot orig, Terraria.NPC self)
    {
        var wasSkeletronDowned = Terraria.NPC.downedBoss3;
        var wasPlanteraDowned = Terraria.NPC.downedPlantBoss;
        var wasAnyMechBossDowned = Terraria.NPC.downedMechBossAny;
        var wereAllMechBossesDowned = Terraria.NPC.downedMechBoss1 && Terraria.NPC.downedBoss2 && Terraria.NPC.downedMechBoss3;
        var wasHardMode = Main.hardMode;
        var wasExpertMode = OverrideExpertMode;
        var wasMasterMode = OverrideMasterMode;
        
        var config = ModContent.GetInstance<MainConfig>();
        
        var dropNormalBossBags = config.NormalModeBossBagsToggle;
        var disableDropRestrictions = config.NoDropRestrictionsToggle;
        
        bool overrideExpertAndMaster = dropNormalBossBags || disableDropRestrictions;
        bool overrideHardMode = disableDropRestrictions;

        try
        {
            if (!dropNormalBossBags && !disableDropRestrictions)
            {
                return;
            }

            if (dropNormalBossBags || disableDropRestrictions)
            {
                OverrideExpertMode = overrideExpertAndMaster;
                OverrideMasterMode = overrideExpertAndMaster;
            }

            if (disableDropRestrictions)
            {
                Main.hardMode = overrideHardMode;
                wasSkeletronDowned = overrideHardMode;
                wasPlanteraDowned = overrideHardMode;
                wasAnyMechBossDowned = overrideHardMode;
                wereAllMechBossesDowned = overrideHardMode;
                OverrideExpertMode = overrideExpertAndMaster;
                OverrideMasterMode = overrideExpertAndMaster;
            }
            
            if (self.type == Terraria.ID.NPCID.WallofFlesh)
            {
                Main.hardMode = true;
                wasHardMode = Main.hardMode;
            }

            if (self.type == Terraria.ID.NPCID.SkeletronHead)
            {
                Terraria.NPC.downedBoss3 = true;
                wasSkeletronDowned = Terraria.NPC.downedBoss3;
            }
            
            if (self.type == Terraria.ID.NPCID.Plantera)
            {
                Terraria.NPC.downedPlantBoss = true;
                wasPlanteraDowned = Terraria.NPC.downedPlantBoss;
            }
            
            if (self.type == Terraria.ID.NPCID.TheDestroyer)
            {
                Terraria.NPC.downedMechBoss1 = true;
                wasAnyMechBossDowned = Terraria.NPC.downedMechBoss1;
            }
            
            if (self.type == Terraria.ID.NPCID.Retinazer || self.type == Terraria.ID.NPCID.Spazmatism)
            {
                Terraria.NPC.downedMechBoss2 = true;
                wasAnyMechBossDowned = Terraria.NPC.downedMechBoss2;
            }

            if (self.type == Terraria.ID.NPCID.SkeletronPrime)
            {
                Terraria.NPC.downedMechBoss3 = true;
                wasAnyMechBossDowned = Terraria.NPC.downedMechBoss3;
            }

            orig.Invoke(self);

            /*if (!ModContent.GetInstance<MainConfig>().NormalModeBossBagsToggle && !ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle)
                return;

            if (ModContent.GetInstance<MainConfig>().NormalModeBossBagsToggle &&
                !ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle)
            {
                if (self.boss || self.type == Terraria.ID.NPCID.EaterofWorldsTail || self.type == Terraria.ID.NPCID.Retinazer)
                {
                    //Main.hardMode = ModContent.GetInstance<MainConfig>().NormalModeBossBagsToggle;
                    OverrideExpertMode = ModContent.GetInstance<MainConfig>().NormalModeBossBagsToggle;
                    OverrideMasterMode = ModContent.GetInstance<MainConfig>().NormalModeBossBagsToggle;
                    orig.Invoke(self);
                }
                else
                {
                    orig.Invoke(self);
                }
            }
            
            else if (!ModContent.GetInstance<MainConfig>().NormalModeBossBagsToggle &&
                ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle)
            {
                Main.hardMode = ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle;
                wasSkeletronDowned = ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle;
                wasPlanteraDowned = ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle;
                wasAnyMechBossDowned = ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle;
                wereAllMechBossesDowned = ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle;
                OverrideExpertMode = ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle;
                OverrideMasterMode = ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle;
                orig.Invoke(self);   
            }
            
            else if (ModContent.GetInstance<MainConfig>().NormalModeBossBagsToggle &&
                     ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle)
            {
                Main.hardMode = ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle;
                wasSkeletronDowned = ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle;
                wasPlanteraDowned = ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle;
                wasAnyMechBossDowned = ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle;
                wereAllMechBossesDowned = ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle;
                OverrideExpertMode = ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle;
                OverrideMasterMode = ModContent.GetInstance<MainConfig>().NoDropRestrictionsToggle;
                orig.Invoke(self);
            }*/

        }
        finally
        {
            Main.hardMode = wasHardMode;
            Terraria.NPC.downedBoss3 = wasSkeletronDowned;
            Terraria.NPC.downedPlantBoss = wasPlanteraDowned;
            Terraria.NPC.downedMechBossAny = wasAnyMechBossDowned;
            Terraria.NPC.downedMechBoss1 = wereAllMechBossesDowned;
            Terraria.NPC.downedMechBoss2 = wereAllMechBossesDowned;
            Terraria.NPC.downedMechBoss3 = wereAllMechBossesDowned;
            OverrideExpertMode = wasExpertMode;
            OverrideMasterMode = wasMasterMode;
        }
    }

    public override void Load()
    {
        NPC.NPCLoot += OnNPCLoot;
    }

    public override void Unload()
    {
        NPC.NPCLoot -= OnNPCLoot;
    }
}