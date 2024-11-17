// Sample data for cart items
const cartData = [
    {
        id: 1,
        name: "Product 1",
        price: 20.0,
        quantity: 1,
        image: "https://via.placeholder.com/80"
    },
    {
        id: 2,
        name: "Product 2",
        price: 15.0,
        quantity: 2,
        image: "https://via.placeholder.com/80"
    }
];

// Function to render cart items
function renderCartItems() {
    const cartItemsContainer = document.getElementById("cart-items");
    cartItemsContainer.innerHTML = ""; // Clear existing items

    let total = 0;

    cartData.forEach(item => {
        const itemTotal = item.price * item.quantity;
        total += itemTotal;

        const cartItem = document.createElement("div");
        cartItem.className = "cart-item";
        cartItem.innerHTML = `
            <div><img src="${item.image}" alt="${item.name}"></div>
            <div>${item.name}</div>
            <div>$${item.price.toFixed(2)}</div>
            <div class="quantity-control">
                <button onclick="changeQuantity(${item.id}, -1)">-</button>
                <span>${item.quantity}</span>
                <button onclick="changeQuantity(${item.id}, 1)">+</button>
            </div>
            <div>$${itemTotal.toFixed(2)}</div>
            <div><button class="imageButton" onclick="removeItem(${item.id})">Remove</button></div>
        `;
        cartItemsContainer.appendChild(cartItem);
        
        
    });

    document.getElementById("cart-total").innerText = total.toFixed(2);
}

// Function to change item quantity
function changeQuantity(id, amount) {
    const item = cartData.find(product => product.id === id);
    if (item) {
        item.quantity += amount;
        if (item.quantity < 1) item.quantity = 1;
        renderCartItems();
    }
}

// Function to remove item from cart
function removeItem(id) {
    const index = cartData.findIndex(product => product.id === id);
    if (index !== -1) {
        cartData.splice(index, 1);
        renderCartItems();
    }
}

// Initial render
renderCartItems();
