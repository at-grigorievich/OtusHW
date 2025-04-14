using UnityEngine;

public interface IHero
{
    float Speed { get; set; }
    float HitPoints { get; set; }
    float Damage { get; set; }
}

public sealed class HeroData: IHero
{
    public float Speed { get; set; }
    public float HitPoints { get; set; }
    public float Damage { get; set; }
}

public class Hero : MonoBehaviour, IHero
{
    public float Speed { get; set; }
    public float HitPoints { get; set; }
    public float Damage { get; set; }
}