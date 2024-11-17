// JavaScript for FAQ accordion functionality
document.addEventListener("DOMContentLoaded", function() {
    const questions = document.querySelectorAll('.faq .question');

    questions.forEach(question => {
        question.addEventListener('click', function() {
            // Toggle active class
            this.classList.toggle('active');

            // Select the next sibling element (the answer div)
            const answer = this.nextElementSibling;

            if (this.classList.contains('active')) {
                // If the question is active, show the answer
                answer.style.display = 'block';
            } else {
                // If the question is not active, hide the answer
                answer.style.display = 'none';
            }
        });
    });
});
