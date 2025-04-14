// audioUtils.js

const audioClips = {
  click: new Audio('assets/audio/click.wav'),
  hover: new Audio('assets/audio/hover.wav'),
  bgMusic: new Audio('assets/audio/menu_bg.mp3')
};

audioClips.bgMusic.loop = true;

export function playAudioFeedback(type) {
  if (audioClips[type]) {
    audioClips[type].currentTime = 0;
    audioClips[type].play();
  }
}

export function startBackgroundMusic() {
  audioClips.bgMusic.volume = 0.4;
  audioClips.bgMusic.play().catch(e => {
    console.warn("User interaction required to play audio:", e);
  });
}

export function stopBackgroundMusic() {
  audioClips.bgMusic.pause();
  audioClips.bgMusic.currentTime = 0;
}
