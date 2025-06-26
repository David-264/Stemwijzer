<?php
require 'db.php';

$antwoorden = $_POST['antwoord'] ?? [];

// Partijen ophalen
$partijenQuery = $pdo->query("SELECT * FROM partijen");
$partijen = $partijenQuery->fetchAll(PDO::FETCH_ASSOC);

// Standpunten ophalen
$standpuntenQuery = $pdo->query("SELECT * FROM partij_standpunt");
$partij_standpunten = $standpuntenQuery->fetchAll(PDO::FETCH_ASSOC);

// Mapping van enum naar getal
$enumNaarInt = [
    'oneens' => 0,
    'neutraal' => 1,
    'eens' => 2,
];

// Partij-antwoorden in cijfers
$partijAntwoorden = [];
foreach ($partij_standpunten as $ps) {
    $partijID = $ps['partijID'];
    $stellingID = $ps['stellingID'];
    $antwoordTekst = strtolower($ps['antwoord']);

    if (isset($enumNaarInt[$antwoordTekst])) {
        $partijAntwoorden[$partijID][$stellingID] = $enumNaarInt[$antwoordTekst];
    }
}

// Scores berekenen
$scores = [];
$aantalVragen = count($antwoorden);

foreach ($partijen as $partij) {
    $partijID = $partij['partijID'];
    $score = 0;

    foreach ($antwoorden as $stellingID => $antwoord) {
        if (isset($partijAntwoorden[$partijID][$stellingID])) {
            $partijAntwoord = $partijAntwoorden[$partijID][$stellingID];

            if ($partijAntwoord === (int)$antwoord) {
                $score++;
            }
        }
    }

    $percentage = ($score / $aantalVragen) * 100;
    $scores[$partijID] = round($percentage, 2);
}

arsort($scores);
?>

<!DOCTYPE html>
<html lang="nl">
<head>
    <meta charset="UTF-8">
    <title>StemWijzer | Resultaten</title>
    <link rel="stylesheet" href="style.css">
</head>
<body>
    <header>
        <nav class="navbar">
            <a href="index.php" class="nav-icon">
                <img src="Logos/Logos/logo-neutraal-kieslab-lichtblauw.svg" alt="Home">
            </a>
            <a href="partijenpagina.php">Partijen</a>
            <a href="verkiezingen.php">Verkiezingen</a>
            <a href="inloggen.php" class="nav-icon">
                <img src="images/user-white.png" alt="Inloggen">
            </a>
        </nav>
    </header>

    <div class="stemwijzer-wrapper">
        <div class="stemwijzer-card">
            <h1>Jouw stemwijzer resultaat</h1>
            <p class="intro">Hieronder zie je hoe goed jouw antwoorden overeenkomen met de standpunten van de partijen.</p>

            <div class="resultaten-lijst">
                <?php foreach ($scores as $partijID => $percentage): ?>
                    <?php
                        foreach ($partijen as $partij) {
                            if ($partij['partijID'] == $partijID): ?>
                                <div class="resultaat-balk">
                                    <span class="partij-naam"><?= htmlspecialchars($partij['partijnaam']) ?></span>
                                    <div class="balk">
                                        <div class="voortgang" style="width: <?= $percentage ?>%">
                                            <?= $percentage ?>%
                                        </div>
                                    </div>
                                </div>
                    <?php endif; } ?>
                <?php endforeach; ?>
            </div>
        </div>
    </div>

    <footer>
        <div class="footer-content">
            <p>&copy; <?= date("Y") ?> StemWijzer. Alle rechten voorbehouden.</p>
        </div>
    </footer>
</body>
</html>
