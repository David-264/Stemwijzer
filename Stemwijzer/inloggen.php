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
session_start();

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $username = $_POST['username'] ?? '';
    $password = $_POST['password'] ?? '';

    $stmt = $conn->prepare("SELECT * FROM gebruikers WHERE gebruikersnaam = :username");
    $stmt->execute(['username' => $username]);
    $user = $stmt->fetch();

    if ($user && password_verify($password, $user['wachtwoord_hash'])) {
        $_SESSION['gebruikerID'] = $user['gebruikerID'];
        $_SESSION['gebruikersnaam'] = $user['gebruikersnaam'];

        header("Location: partijenpagina.php");
        exit;
    } else {
        $error = "Ongeldige gebruikersnaam of wachtwoord.";
    }
}
?>

<!DOCTYPE html>
<html lang="nl">
<head>
    <meta charset="UTF-8">
    <title>Inloggen</title>
    <link rel="stylesheet" href="style.css">
</head>
<body id="body">

<div id="login-container">
    <img id="login-logo" src="Logos/Logos/logo-neutraal-kieslab-donkerblauw.svg" alt="KiesLab Logo">
    <h2 id="login-naam">Neutraal KiesLab</h2>
    <h2 id="login-title">Inloggen</h2>
    <?php if ($error): ?>
        <div id="login-error"><?= htmlspecialchars($error) ?></div>
    <?php endif; ?>
    <form id="login-form" method="post">
        <input type="text" id="username-input" name="username" placeholder="Gebruikersnaam" required>
        <input type="password" id="password-input" name="password" placeholder="Wachtwoord" required>
        <input type="submit" id="login-button" value="Login">
    </form>
    <p id="register-link">Nog geen account? <a href="register.php">Registreer hier</a></p>
</div>

</body>
</html>