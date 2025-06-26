<?php
require 'db.php';

$error = '';
session_start();
?>

<!DOCTYPE html>
<html lang="nl">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Partijen</title>
  <link href="style.css" rel="stylesheet">
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

<main>

  <?php require 'db.php'; ?>

 <h1 class="pagina-titel">Partijen Overzicht</h1>

    <div class="partijen-wrapper">
        <?php foreach ($result_stemwijzer as $partij): ?>
            <div class="partij-container">
              <?php if (!empty($partij['afbeelding'])): ?>
                <img class="partij-afbeelding" src="<?= htmlspecialchars($partij['afbeelding']) ?>" alt="Logo <?= htmlspecialchars($partij['naam']) ?>">
              <?php endif; ?>
                <div class="partij-header">
                    <span><?= htmlspecialchars($partij['partijnaam']) ?></span>
                    <span><?= (int)$partij['aantalstemmen'] ?> stemmen</span>
                </div>
                <div class="partij-body">
                    <p><strong>Partijinfo:</strong> <?= nl2br(htmlspecialchars($partij['partijinfo'])) ?></p>
                    <p><strong>Visie:</strong> <?= nl2br(htmlspecialchars($partij['partijvisie'])) ?></p>
                </div>
            </div>
        <?php endforeach; ?>
    </div>
</main>

<footer>
    <div class="footer-content">
      <p>&copy; <?php echo date("Y"); ?> StemWijzer. Alle rechten voorbehouden.</p>
    </div>
  </footer>

</body>
</html>
