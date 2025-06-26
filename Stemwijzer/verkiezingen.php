<!DOCTYPE html>
<html lang="nl">
<head>
    <meta charset="UTF-8">
    <title>Verkiezingen KiesLab</title>
    <link rel="stylesheet" href="style.css">
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
    <div class="verkiezingen-container">
        <h1>Kies je verkiezing</h1>
        <div class="verkiezingen-grid">

            <a href="stemwijzer.php?verkiezing=gemeenteraad" class="verkiezing-card">
                <h2>Gemeenteraadsverkiezingen</h2>
                <p>Lokaal beleid, directe invloed in jouw gemeente.</p>
            </a>

            <a href="stemwijzer.php?verkiezing=tweedekamer" class="verkiezing-card">
                <h2>Tweede Kamerverkiezingen</h2>
                <p>Landelijk beleid, wetgeving en regeringsvorming.</p>
            </a>

            <a href="stemwijzer.php?verkiezing=eerstekamer" class="verkiezing-card">
                <h2>Eerste Kamerverkiezingen</h2>
                <p>Wettelijke controle, indirect gekozen via Provinciale Staten.</p>
            </a>

            <a href="stemwijzer.php?verkiezing=provincie" class="verkiezing-card">
                <h2>Provinciale Statenverkiezingen</h2>
                <p>Regionale thema’s zoals ruimtelijke ordening en infrastructuur.</p>
            </a>

            <a href="stemwijzer.php?verkiezing=europa" class="verkiezing-card">
                <h2>Europees Parlement</h2>
                <p>Europese wetgeving, grensoverschrijdende thema’s.</p>
            </a>

        </div>
    </div>
</main>
    <footer>
        <div class="footer-content">
            <p>&copy; <?php echo date("Y"); ?> StemWijzer. Alle rechten voorbehouden.</p>
        </div>

    <script src="Stemwijzer.js"></script>
</body>
</html>
