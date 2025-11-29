using UnityEngine;

namespace ArcaneForgeNS
{
    internal class OreExcavator : CardData
    {
        private string parentActionId = "";

        public override void UpdateCard()
        {
            if(this.MyGameCard.HasParent)
            {
                string parentId = this.MyGameCard.Parent.CardData.Id;

                bool isCombatableMine = parentId == "quarry" || parentId == "mine" || parentId == "gold_mine" || parentId == "sand_quarry" || parentId == Consts.Card(Consts.MAGIC_DUST_MINE);
                bool isEnergyMine = parentId == "cities_iron_mine" || parentId == "gravel_pit" || parentId == "copper_mine" || parentId == "uranium_mine";

                if (isCombatableMine)
                {
                    CombatableHarvestable parent = (CombatableHarvestable)this.MyGameCard.Parent.CardData;
                    parentActionId = parent.GetActionId("CompleteHarvest");
                    this.MyGameCard.StartTimer(0.5f * parent.HarvestTime, new TimerAction(parent.CompleteHarvest), parent.StatusText, parentActionId, true, false, false);
                }
                else if(isEnergyMine)
                {
                    EnergyHarvestable parent = (EnergyHarvestable)this.MyGameCard.Parent.CardData;
                    parentActionId = parent.GetActionId("CompleteHarvest");
                    this.MyGameCard.StartTimer(0.5f * parent.HarvestTime, new TimerAction(parent.CompleteHarvest), parent.StatusText, parentActionId, true, false, false);
                }
                else
                {
                    this.MyGameCard.CancelTimer(parentActionId);
                }
            }
            else
            {
                this.MyGameCard.CancelTimer(parentActionId);
            }
            base.UpdateCard();
        }
    }
}
