// menuAnimator.js

export function animateEntrance(menuContainer) {
  menuContainer.style.opacity = 0;
  menuContainer.style.transform = 'translateY(-100px)';
  
  requestAnimationFrame(() => {
    menuContainer.style.transition = 'all 0.6s ease-out';
    menuContainer.style.opacity = 1;
    menuContainer.style.transform = 'translateY(0px)';
  });
}

export function animateButtonHover(button) {
  button.addEventListener('mouseenter', () => {
    button.classList.add('hovered');
  });

  button.addEventListener('mouseleave', () => {
    button.classList.remove('hovered');
  });
}

export function animateAllButtons(buttons) {
  buttons.forEach((button, index) => {
    setTimeout(() => {
      button.style.transform = 'scale(1)';
      button.style.opacity = 1;
    }, index * 150);
  });
}
