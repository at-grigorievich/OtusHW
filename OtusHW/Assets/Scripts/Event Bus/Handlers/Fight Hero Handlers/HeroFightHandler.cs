using DefaultNamespace.Event_Bus.Events;
using UnityEngine;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class HeroFightHandler: EventLogicReceiver<FightEvent>
    {
        public HeroFightHandler(EventBus eventBus): base(eventBus)
        {
        }
        
        public override void OnEvent(FightEvent evt)
        {
            var defenderHealth = evt.Defender.GetComponent<Health>();
            var attackerDamage = evt.Attacker.GetComponent<Damage>();

            defenderHealth.Value = Mathf.Clamp(defenderHealth.Value - attackerDamage.Value, 0, int.MaxValue);
            
            evt.Attacker.AddComponent(new AttackerMarker());
            evt.Defender.AddComponent(new DamagedMarker());
        }
    }
}