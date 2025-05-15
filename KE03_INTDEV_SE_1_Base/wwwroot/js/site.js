// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
if(document.querySelectorAll('.winkel').length > 0) {
    const winkelknoppen = document.querySelectorAll('.winkel');

    winkelknoppen.forEach((knop) => {
        knop.addEventListener('click', () => {
            const productKaart = knop.closest('.product-card');
            const naam = productKaart.querySelector('h1').textContent;
            const prijsText = productKaart.querySelector('h2').textContent;
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
}


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

});
