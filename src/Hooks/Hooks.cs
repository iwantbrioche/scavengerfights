namespace ScavFights.Hooks
{
    public static class Hooks
    {
        public static void PatchAll()
        {
            On.ScavengerAI.IUseARelationshipTracker_UpdateDynamicRelationship += ScavengerAI_IUseARelationshipTracker_UpdateDynamicRelationship;
            On.Scavenger.ctor += Scavenger_ctor;
            On.ScavengerAbstractAI.InitGearUp += ScavengerAbstractAI_InitGearUp;
        }

        private static void ScavengerAbstractAI_InitGearUp(On.ScavengerAbstractAI.orig_InitGearUp orig, ScavengerAbstractAI self)
        {
            if (!ScavFights.remix.weaponSpawn.Value)
            {
                return;
            }
            orig(self);
        }

        private static void Scavenger_ctor(On.Scavenger.orig_ctor orig, Scavenger self, AbstractCreature abstractCreature, World world)
        {
            orig(self, abstractCreature, world);
            (self.State as HealthState).health *= ScavFights.remix.healthMult.Value;
        }

        private static CreatureTemplate.Relationship ScavengerAI_IUseARelationshipTracker_UpdateDynamicRelationship(On.ScavengerAI.orig_IUseARelationshipTracker_UpdateDynamicRelationship orig, ScavengerAI self, RelationshipTracker.DynamicRelationship dRelation)
        {
            if (dRelation.trackerRep.representedCreature.creatureTemplate.type == CreatureTemplate.Type.Scavenger || 
                dRelation.trackerRep.representedCreature.creatureTemplate.type == MoreSlugcatsEnums.CreatureTemplateType.ScavengerElite || 
                dRelation.trackerRep.representedCreature.creatureTemplate.type == MoreSlugcatsEnums.CreatureTemplateType.ScavengerKing ||
                dRelation.trackerRep.representedCreature.creatureTemplate.type.value == "Scrounger")
            {
                if (dRelation.trackerRep.representedCreature.state.dead) return new CreatureTemplate.Relationship(CreatureTemplate.Relationship.Type.Ignores, 1f);

                (dRelation.state as ScavengerAI.ScavengerTrackState).taggedViolenceType = ScavengerAI.ViolenceType.Lethal;
                return new CreatureTemplate.Relationship(CreatureTemplate.Relationship.Type.Attacks, 1f);
            }
            return orig(self, dRelation);
        }
    }
}
