namespace pixalquarks.bgj2022_2
{
    public interface IDamageable
    {
        bool IsAlive { get; set; }
        void Damage();
    }
}