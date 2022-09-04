# NormalisedLadleRotation

Mini-plugin for PotionCraft 0.5.0 that fixes a bug encountered when the potion indicator is rotated by the ladle (pouring water or oil into the potion) or Philosopher's Salt.
The issue is that the rotation from these sources varies depending on the FPS of the game when the rotation happens. Higher FPS = less rotation; lower FPS = more rotation.
This plugin adjusts the rotation so that moves at the same rate regardless of FPS.