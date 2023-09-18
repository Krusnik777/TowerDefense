namespace TowerDefense
{
    public enum Sound
    {
        Shop = 0,
        Arrow = 1,
        ArrowHit = 2,
        EnemyDie = 3,
        EnemyWin = 4,
        PlayerWin = 5,
        PlayerLose = 6,
        MainMenuBGM = 7,
        LevelMapBGM = 8,
        BriefingBGM = 9,
        LevelBGM = 10,
        EndGameBGM = 11,
        FireAbility = 12,
        SlowAbility = 13,
        Level2BGM = 14,
        Level3BGM = 15
    }

    public static class SoundExtensions
    {
        public static void Play(this Sound sound)
        {
            if (SoundPlayer.Instance) SoundPlayer.Instance.Play(sound);
        }

        public static void PlayBGM(this Sound sound, bool loop)
        {
            if (SoundPlayer.Instance) SoundPlayer.Instance.PlayBGM(sound, loop);
        }
    }
}
