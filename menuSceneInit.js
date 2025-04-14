// menuSceneInit.js

import MainMenuController from './mainMenuController.js';
import { animateEntrance, animateAllButtons, animateButtonHover } from './menuAnimator.js';
import { startBackgroundMusic } from './audioUtils.js';

document.addEventListener('DOMContentLoaded', () => {
  const menuElements = {
    title: document.getElementById('gameTitle'),
    buttons: {
      start: document.getElementById('startBtn'),
      settings: document.getElementById('settingsBtn'),
      exit: document.getElementById('exitBtn')
    }
  };

  const controller = new MainMenuController(menuElements);
  const menuContainer = document.getElementById('menuContainer');

  animateEntrance(menuContainer);
  animateAllButtons(Object.values(menuElements.buttons));

  Object.values(menuElements.buttons).forEach(button => animateButtonHover(button));
  startBackgroundMusic();
});
