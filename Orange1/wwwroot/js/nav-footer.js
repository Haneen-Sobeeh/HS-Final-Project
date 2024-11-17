// /js/script.js

// Function to include HTML components
function includeHTML() {
    let elements = document.querySelectorAll('[data-include]');
    
    elements.forEach(el => {
      let file = el.getAttribute('data-include');
      fetch(file)
        .then(response => response.text())
        .then(data => {
          el.innerHTML = data;
        })
        .catch(error => console.error('Error loading file:', error));
    });
  }
  
  // Call the function when the page loads
  document.addEventListener('DOMContentLoaded', includeHTML);

  function toggleMenu() {
    const navLinks = document.getElementById('navLinks');
    navLinks.classList.toggle('show'); // Toggle active class for mobile view
}

function toggleDropdown() {
  const dropdownMenu = document.getElementById('dropdownMenu');
  dropdownMenu.style.display = dropdownMenu.style.display === 'block' ? 'none' : 'block';
}

// Close the dropdown if the user clicks outside of it
window.onclick = function(event) {
  if (!event.target.matches('.fa-user')) {
    const dropdowns = document.getElementsByClassName('dropdown-menu');
    for (let i = 0; i < dropdowns.length; i++) {
      const openDropdown = dropdowns[i];
      if (openDropdown.style.display === 'block') {
        openDropdown.style.display = 'none';
      }
    }
  }
};
