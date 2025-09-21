namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал - "Замедли передвижение игрона на SlowMultiplier с длительностью эффекта SlowEffectTime"
    /// </summary>
    public class PlayerSlowSignal 
    {
        private readonly float _slowMultiplier;

        private readonly int _slowEffectTime;
        public float SlowMultiplier => _slowMultiplier;

        public int SlowEffectTime => _slowEffectTime;

        public PlayerSlowSignal(float slowMultiplier, int slowEffectTime)
        {
            _slowMultiplier = slowMultiplier;
            _slowEffectTime = slowEffectTime;
        }
    }
}