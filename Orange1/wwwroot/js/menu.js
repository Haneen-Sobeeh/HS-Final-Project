// Get the search input field and the food cards
const searchInput = document.getElementById('searchByName');
const cards = document.querySelectorAll('.card');

// Add an event listener to the search input field
searchInput.addEventListener('input', function () {
    const searchTerm = searchInput.value.toLowerCase(); // Convert input to lowercase for case-insensitive search
    
    // Loop through each card and hide or show based on the search term
    cards.forEach(card => {
        const cardTitle = card.querySelector('h3').textContent.toLowerCase(); // Get the card's title in lowercase

        // Check if the search term matches the card title
        if (cardTitle.includes(searchTerm)) {
            card.style.display = 'block'; // Show the card if it matches
        } else {
            card.style.display = 'none'; // Hide the card if it doesn't match
        }
    });
});






document.addEventListener("DOMContentLoaded", function () {
    const cards = document.querySelectorAll(".card");
    const itemsPerPage = 6; // Number of items per page
    let currentPage = 1;

    function displayCards(page) {
        const start = (page - 1) * itemsPerPage;
        const end = start + itemsPerPage;

        // Hide all cards first
        cards.forEach((card, index) => {
            if (index >= start && index < end) {
                card.style.display = "block"; // Show card
            } else {
                card.style.display = "none"; // Hide card
            }
        });

        // Update pagination buttons
        updatePagination(page);
    }

    function updatePagination(currentPage) {
        const totalPages = Math.ceil(cards.length / itemsPerPage);
        const pagination = document.getElementById("pagination");
        pagination.innerHTML = ""; // Clear existing buttons

        // Create buttons for pagination
        for (let i = 1; i <= totalPages; i++) {
            const button = document.createElement("button");
            button.textContent = i;
            button.className = i === currentPage ? "active" : "";
            button.addEventListener("click", function () {
                displayCards(i); // Show cards for the clicked page
            });
            pagination.appendChild(button);
        }
    }

    // Initially display the first page
    displayCards(currentPage);
});
