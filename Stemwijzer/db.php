<?php
    $host = 'localhost';
    $db = 'stemwijzer';
    $user = 'root';
    $password = '';

    try
    {
        $pdo = new PDO("mysql:host=$host;dbname=$db", $user, $password);
        $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    }
    catch (PDOException $e)
    {
        echo "Fout: " . $e->getMessage();
        die();
    }

    $sql = "SELECT partijen.partijnaam, partijen.partijinfo, partijen.aantalstemmen, partijen.partijvisie FROM partijen;";
    $stmt = $pdo->query($sql);
    $result_stemwijzer = $stmt->FetchAll();
?>