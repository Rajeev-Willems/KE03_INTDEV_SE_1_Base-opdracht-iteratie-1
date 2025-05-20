// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
if(document.querySelectorAll('.winkel').length > 0) {
    const winkelknoppen = document.querySelectorAll('.winkel');

    winkelknoppen.forEach((knop) => {
        knop.addEventListener('click', () => {
            const productKaart = knop.closest('.product-card');
            const naam = productKaart.querySelector('h1').textContent;
            const prijsText = productKaart.querySelector('prijs').textContent;
            const prijs = prijsText.replace(/[^\d,]/g, '').replace(',', '.');

            const nieuwProduct = {
                naam: naam,
                prijs: parseFloat(prijs).toFixed(2)
            };

            let winkelmandje = JSON.parse(localStorage.getItem('winkelmandje')) || [];
            winkelmandje.push(nieuwProduct);
            localStorage.setItem('winkelmandje', JSON.stringify(winkelmandje));

            alert(`${naam} is toegevoegd aan je winkelmandje.`);
        });
    });
    <script>
    document.addEventListener('DOMContentLoaded', () => {
        const buttons = document.querySelectorAll('.add-to-cart');

        buttons.forEach(button => {
            button.addEventListener('click', () => {
                const id = button.getAttribute('data-id');

                fetch('/Index?handler=AddToCart', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(parseInt(id))
                })
                    .then(response => response.json())
                    .then(result => {
                        if (result.success) {
                            alert("Product toegevoegd aan winkelmandje!");
                        }
                    });
            });
        });
    });
    </script>




    document.addEventListener("DOMContentLoaded", function () {
        const winkelmandContainer = document.getElementById('winkelmandje');
        if (!winkelmandContainer) return;

        const winkelmandje = JSON.parse(localStorage.getItem('winkelmandje')) || [];

        if (winkelmandje.length === 0) {
            winkelmandContainer.innerHTML = "<p>Je winkelmandje is leeg.</p>";
        } else {
            let totaal = 0;
            winkelmandje.forEach((product, index) => {
                totaal += parseFloat(product.prijs);
                const div = document.createElement('div');
                div.classList.add('product-card');
                div.innerHTML = `
                <h2>${product.naam}</h2>
                <p>Prijs: €${product.prijs}</p>
                <button class="verwijder" data-index="${index}">Verwijder</button>
            `;
                winkelmandContainer.appendChild(div);
            });

            const totaalDiv = document.createElement('div');
            totaalDiv.innerHTML = `<h3>Totaal: €${totaal.toFixed(2)}</h3>`;
            winkelmandContainer.appendChild(totaalDiv);
        }


        winkelmandContainer.addEventListener('click', function (e) {
            if (e.target.classList.contains('verwijder')) {
                const index = parseInt(e.target.dataset.index);
                winkelmandje.splice(index, 1);
                localStorage.setItem('winkelmandje', JSON.stringify(winkelmandje));
                location.reload();
            }
        });


        <script>

            document.addEventListener('DOMContentLoaded', function () {
        const buttons = document.querySelectorAll('.add-to-cart');

        buttons.forEach(btn => {
                btn.addEventListener('click', function () {
                    const productId = parseInt(this.getAttribute('data-id'));
                    addToCart(productId);
                });
        });
    });


            function addToCart(productId) {

                let cart = JSON.parse(sessionStorage.getItem('cart')) || [];


            cart.push(productId);


            sessionStorage.setItem('cart', JSON.stringify(cart));

            alert("Product toegevoegd aan winkelmandje!");
    }
        </script>
    }
});

