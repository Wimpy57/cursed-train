namespace Core.Scripts.Achievements
{
    // it is not allowed to change Achievement's IDs
    // should always be as specified
    // if it is necessary to add a new one, you should give it a unique ID
    public enum Achievement
    {
        TrashSearcher = 113, // find the coin inside trash bin in toilet todo
        HoldYourHorses = 156, // try to open conductor door earlier than you should
        ElectroWizard = 263, // put fingers inside the coupe socket in the beginning todo 
        SunHater = 303, // close all the curtains in corridor before making the train dark todo
        Inadequate = 500, // try to kill the old man with extinguisher todo
        HideAndSeek = 562, // find the coin under conductor's seat todo
        AreYouScared = 666, // encounter screamer when there is no light and train horns loudly todo 
        SlowGuy = 707, // complete the game in more than 20 minutes todo
        TheFastest = 777, // complete the game in less than 5 minutes todo
        Hardcore = 999, // complete the game without any single death todo
    }
}
