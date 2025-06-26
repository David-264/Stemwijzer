function inlogpagina(){
    window.location.href = 'inloggen.php';
}
document.addEventListener('DOMContentLoaded', function () {
    const toggleButton = document.getElementById('darkmode-btn');
    toggleButton.addEventListener('click', function () {
        document.body.classList.toggle('dark-mode');
    });
});
function toggleDetails(cardElement) {
  cardElement.classList.toggle('active');
}
function stemwijzer(){
    window.location.href = 'stemwijzer.php';
}
document.addEventListener("DOMContentLoaded", () => {
            document.querySelectorAll(".partij-container").forEach(container => {
                container.addEventListener("click", () => {
                    const body = container.querySelector(".partij-body");
                    body.style.display = body.style.display === "block" ? "none" : "block";
                    });
        });
});
function selectVerkiezing(type) {
    // Eventueel een redirect of iets met een parameter meegeven
    // Bijvoorbeeld doorgaan naar een pagina met stemwijzer per type
    window.location.href = `stemwijzer.php?verkiezing=${type}`;
}
let huidigeIndex = 0;
let antwoorden = {};
const container = document.getElementById('vraag-container');
const volgendeBtn = document.getElementById('volgendeBtn');

function laadVraag(index) {
    const vraag = vragenData[index];
    container.innerHTML = `
        <div class="vraag-blok">
            <p>${vraag.vraag_tekst}</p>
            <label><input type="radio" name="keuze" value="0"> Oneens</label><br>
            <label><input type="radio" name="keuze" value="1"> Neutraal</label><br>
            <label><input type="radio" name="keuze" value="2"> Eens</label>
        </div>
    `;
    volgendeBtn.disabled = true;

    document.querySelectorAll('input[name="keuze"]').forEach(input => {
        input.addEventListener('change', () => {
            volgendeBtn.disabled = false;
        });
    });
}

volgendeBtn.addEventListener('click', () => {
    const keuze = document.querySelector('input[name="keuze"]:checked').value;
    antwoorden[vragenData[huidigeIndex].vraag_id] = parseInt(keuze);
    huidigeIndex++;

    if (huidigeIndex < vragenData.length) {
        laadVraag(huidigeIndex);
    } else {
        document.getElementById('antwoordData').value = JSON.stringify(antwoorden);
        document.getElementById('resultForm').submit();
    }
});

laadVraag(huidigeIndex);
document.addEventListener("DOMContentLoaded", function () {
    const vragenContainers = document.querySelectorAll('.vraag-container');

    vragenContainers.forEach(container => {
        const labels = container.querySelectorAll('.radio-label');

        labels.forEach(label => {
            const input = label.querySelector('input[type="radio"]');

            input.addEventListener('change', () => {
                // Verwijder 'selected' van alle labels binnen deze container
                labels.forEach(l => l.classList.remove('selected'));
                // Voeg 'selected' toe aan het aangeklikte label
                label.classList.add('selected');
            });
        });
    });
});
