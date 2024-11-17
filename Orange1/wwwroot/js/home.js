// Get elements
const sliderContainer = document.querySelector('.slider-container');
const slideRightBtn = document.getElementById('slideRight');
const slideLeftBtn = document.getElementById('slideLeft');

// Set the number of pixels to move per click
const scrollAmount = 300;

// Move the slider right
slideRightBtn.addEventListener('click', () => {
    sliderContainer.scrollBy({ left: scrollAmount, behavior: 'smooth' });
});

// Move the slider left
slideLeftBtn.addEventListener('click', () => {
    sliderContainer.scrollBy({ left: -scrollAmount, behavior: 'smooth' });
});




const feedbackButton = document.getElementById('feedbackButton');
const feedbackModal = document.getElementById('feedbackModal');
const closeModal = feedbackModal.querySelector('.close');

// Show modal when "Feedback" button is clicked
feedbackButton.addEventListener('click', () => {
    feedbackModal.style.display = 'block'; // Display the modal
});

// Hide modal when "Close" (x) button is clicked
closeModal.addEventListener('click', () => {
    feedbackModal.style.display = 'none'; // Hide the modal
});

// Hide modal when clicking outside the modal content
window.addEventListener('click', (event) => {
    if (event.target === feedbackModal) {
        feedbackModal.style.display = 'none';
    }
});

document.getElementById("submitFeedback").addEventListener("click", async function () {
    const feedbackText = document.getElementById("feedbackText").value;

    if (!feedbackText.trim()) {
        alert("Please write some feedback before submitting.");
        return;
    }

    try {
        const response = await fetch('/Testimonial/Testimonial', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ message: feedbackText }),
        });

        if (response.ok) {
            const newTestimonial = await response.json();
            addTestimonialToPage(newTestimonial);
            document.getElementById("feedbackText").value = "";
            alert("Thank you for your feedback!");
        } else {
            alert("An error occurred while submitting your feedback.");
        }
    } catch (error) {
        console.error("Error submitting feedback:", error);
        alert("An error occurred. Please try again later.");
    }
});

// Dynamically add the new testimonial to the page
function addTestimonialToPage(testimonial) {
    const testimonialsContainer = document.getElementById("testimonialsContainer");

    const testimonialDiv = document.createElement("div");
    testimonialDiv.classList.add("testimonial");
    testimonialDiv.innerHTML = `<p><strong>${testimonial.commenUser}:</strong> ${testimonial.commentText}</p>`;

    testimonialsContainer.appendChild(testimonialDiv);
}
