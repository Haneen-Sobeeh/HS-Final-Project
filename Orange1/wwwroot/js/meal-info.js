document.addEventListener('DOMContentLoaded', function() {
    // Prevent selecting today's date or past dates
    const today = new Date().toISOString().split('T')[0];
    document.getElementById('order-date').setAttribute('min', today);

    // Get modal element
    const modal = document.getElementById('card-modal');
    const paymentMethodSelect = document.getElementById('payment-method');
    const closeBtn = document.getElementsByClassName('close')[0];

    // Submit button handler
    document.getElementById('submit-btn').addEventListener('click', function() {
        const quantity = document.getElementById('quantity').value;
        const orderDate = document.getElementById('order-date').value;
        const orderTime = document.getElementById('order-time').value;
        const comments = document.getElementById('comments').value;
        const location = document.getElementById('location').value;
        const paymentMethod = paymentMethodSelect.value;

        if (orderDate === "" || orderTime === "") {
            alert('Please select a valid date and time.');
            return;
        }

        if (paymentMethod === "card") {
            modal.style.display = "block"; // Show the modal
        } else {
            alert(`Order submitted:\nQuantity: ${quantity}\nDate: ${orderDate}\nTime: ${orderTime}\nComments: ${comments}\nLocation: ${location}\nPayment Method: ${paymentMethod}`);
        }
    });

    // Close the modal when the user clicks on <span> (x)
    closeBtn.onclick = function() {
        modal.style.display = "none";
    }

    // Submit card info button handler
    document.getElementById('submit-card-info').addEventListener('click', function() {
        const cardNumber = document.getElementById('card-number').value;
        const expiryDate = document.getElementById('expiry-date').value;
        const cvv = document.getElementById('cvv').value;

        // Here, you can add logic to handle card information submission

        alert(`Card Information Submitted:\nCard Number: ${cardNumber}\nExpiry Date: ${expiryDate}\nCVV: ${cvv}`);
        
        // Close modal after submission
        modal.style.display = "none";
    });

    // Close modal if the user clicks outside of it
    window.onclick = function(event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
});


function addReview() {
    const reviewText = document.getElementById('new-review').value;
    if (reviewText) {
        // Create a new review element
        const reviewItem = document.createElement('div');
        reviewItem.classList.add('review-item');

        // Add the text to the review item
        const reviewContent = document.createElement('p');
        reviewContent.textContent = reviewText;
        reviewItem.appendChild(reviewContent);

        // Append the review item to the reviews container
       
        document.getElementById('reviews-container').appendChild(reviewItem);

        // Clear the input field
        document.getElementById('new-review').value = '';
    }
}
