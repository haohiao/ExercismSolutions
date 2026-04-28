static class QuestLogic
{
    public static bool CanFastAttack(bool knight_is_awake)
    {
        bool can_fast_attack = !knight_is_awake;
        return can_fast_attack;
    }

    public static bool CanSpy(bool knight_is_awake, bool archer_is_awake , bool prisoner_is_awake)
    {
        bool can_spy = knight_is_awake || archer_is_awake || prisoner_is_awake;
        return can_spy;
    }

    public static bool CanSignalPrisoner(bool archer_is_awake, bool prisoner_is_awake)
    {
        bool can_signal_prisoner = !archer_is_awake && prisoner_is_awake;
        return can_signal_prisoner;
    }

    public static bool CanFreePrisoner(bool knight_is_awake, bool archer_is_awake, bool prisoner_is_awake, bool pet_dog_is_present)
    {
        bool can_free_prisoner = 
            (!archer_is_awake && pet_dog_is_present) ||
            (!knight_is_awake && !archer_is_awake && prisoner_is_awake && !pet_dog_is_present);
        return can_free_prisoner;
    }
}
