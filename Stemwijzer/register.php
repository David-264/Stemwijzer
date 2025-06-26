<?php
// Databaseverbinding (komt in plaats van een aparte db.php)
$host = 'localhost';
$db   = 'stemwijzer'; // pas aan als jouw database anders heet
$user = 'root';
$pass = ''; // eventueel je wachtwoord invullen
$charset = 'utf8mb4';

$dsn = "mysql:host=$host;dbname=$db;charset=$charset";
$options = [
    PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION,
    PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
    PDO::ATTR_EMULATE_PREPARES   => false,
];

try {
    $conn = new PDO($dsn, $user, $pass, $options);
} catch (PDOException $e) {
    die("Databaseverbinding mislukt: " . $e->getMessage());
}

$error = '';
$success = '';

// Verwerk het formulier alleen als er gepost is
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $username = $_POST['username'] ?? '';
    $email    = $_POST['email'] ?? '';
    $password = $_POST['password'] ?? '';

    // Eenvoudige validatie
    if (empty($username) || empty($email) || empty($password)) {
        $error = "Vul alle velden in.";
    } else {
        // Check of gebruiker al bestaat
        $stmt = $conn->prepare("SELECT * FROM gebruikers WHERE gebruikersnaam = :username OR email = :email");
        $stmt->execute(['username' => $username, 'email' => $email]);
        if ($stmt->fetch()) {
            $error = "Gebruikersnaam of e-mailadres is al in gebruik.";
        } else {
            // Wachtwoord hashen
            $passwordHash = password_hash($password, PASSWORD_DEFAULT);

            // Invoegen
            $insert = $conn->prepare("INSERT INTO gebruikers (gebruikersnaam, email, wachtwoord_hash) VALUES (:username, :email, :wachtwoord)");
            $insert->execute([
                'username' => $username,
                'email' => $email,
                'wachtwoord' => $passwordHash
            ]);
            $success = "Registratie geslaagd! Je kunt nu <a href='inloggen.php'>inloggen</a>.";
        }
    }
}
?>

<!DOCTYPE html>
<html lang="nl">
<head>
  <meta charset="UTF-8">
  <title>Registreren</title>
  <link rel="stylesheet" href="style.css">
</head>
<body id="body">
<div id="login-container">
    <img id="login-logo" src="Logos/Logos/logo-neutraal-kieslab-donkerblauw.svg" alt="KiesLab Logo">
    <h2 id="login-naam">Neutraal KiesLab</h2>
    <h2 id="login-title">Registreren</h2>

    <?php if ($error): ?>
        <div id="login-error"><?= htmlspecialchars($error) ?></div>
    <?php endif; ?>
    <?php if ($success): ?>
        <div id="login-success"><?= $success ?></div>
    <?php endif; ?>

    <form id="login-form" method="post">
        <input type="text" id="username-input" name="username" placeholder="Gebruikersnaam" required>
        <input type="email" id="email-input" name="email" placeholder="E-mailadres" required>
        <input type="password" id="password-input" name="password" placeholder="Wachtwoord" required>
        <input type="submit" id="login-button" value="Registreren">
    </form>
    <p id="register-link">Heb je al een account? <a href="inloggen.php">Log in</a></p>
</div>
</body>
</html>
