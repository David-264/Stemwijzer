<?php
require 'db.php';
$vraagQuery = $pdo->query("SELECT * FROM vragen ORDER BY vraag_id ASC");
$vragen = $vraagQuery->fetchAll();
?>

<!DOCTYPE html>
<html lang="nl">
<head>
    <meta charset="UTF-8">
    <title>StemWijzer | Vragen</title>
    <link rel="stylesheet" href="style.css">
    <script src="Stemwijzer.js"></script>
</head>
<body>
    <header>
    <nav class="navbar">
      <a href="index.php" class="nav-icon">
        <img src="Logos\Logos/logo-neutraal-kieslab-lichtblauw.svg" alt="Home">
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
            <h1>Beantwoord de vragen</h1>
            <p class="intro">Ontdek welke partij het beste bij jouw visie past.</p>

            <form method="POST" action="stemwijzerresult.php">
                <?php foreach ($vragen as $index => $vraag): ?>
                    <div class="vraag-container fade-in">
                        <h3>Vraag <?= $index + 1 ?> van <?= count($vragen) ?></h3>
                        <p class="vraag-tekst"><?= htmlspecialchars($vraag['vraag_tekst']) ?></p>
                        <div class="opties">
                            <?php
                                $opties = [0 => 'Oneens', 1 => 'Neutraal', 2 => 'Eens'];
                                foreach ($opties as $waarde => $label): ?>
                                    <label class="radio-label">
                                        <input type="radio" name="antwoord[<?= $vraag['vraag_id'] ?>]" value="<?= $waarde ?>" required>
                                        <span><?= $label ?></span>
                                    </label>
                            <?php endforeach; ?>
                        </div>
                    </div>
                <?php endforeach; ?>

                <div class="submit-container">
                    <button type="submit" class="vergelijk-knop">Bekijk resultaten</button>
                </div>
            </form>
        </div>
    </div>
    <footer>
    <div class="footer-content">
      <p>&copy; <?php echo date("Y"); ?> StemWijzer. Alle rechten voorbehouden.</p>
    </div>
  </footer>
</body>
</html>