// mainMenuController.js

import { loadScene } from './sceneLoader.js';
import { playAudioFeedback } from './audioUtils.js';

class MainMenuController {
  constructor(menuElements) {
    this.buttons = menuElements.buttons;
    this.sceneTitle = menuElements.title;
    this.initListeners();
  }

  initListeners() {
    this.buttons.start.addEventListener('click', () => {
      playAudioFeedback('click');
      this.fadeOutMenu(() => loadScene('LevelSelection'));
    });

    this.buttons.settings.addEventListener('click', () => {
      playAudioFeedback('click');
      this.toggleSettingsMenu();
    });

    this.buttons.exit.addEventListener('click', () => {
      playAudioFeedback('click');
      this.confirmExit();
    });
  }

  fadeOutMenu(callback) {
    this.sceneTitle.classList.add('fade-out');
    Object.values(this.buttons).forEach(button => button.classList.add('fade-out'));

    setTimeout(() => {
      callback();
    }, 1500);
  }

  toggleSettingsMenu() {
    const settingsPanel = document.getElementById('settingsPanel');
    settingsPanel.classList.toggle('visible');
  }

  confirmExit() {
    if (confirm("Are you sure you want to exit the game?")) {
      window.close(); // Will only work in packaged environments
    }
  }
}

export default MainMenuController;
