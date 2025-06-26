-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Gegenereerd op: 14 mei 2025 om 13:38
-- Serverversie: 10.4.32-MariaDB
-- PHP-versie: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `stemwijzer`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `gebruikers`
--

CREATE TABLE `gebruikers` (
  `gebruikerID` int(11) NOT NULL,
  `gebruikersnaam` varchar(50) NOT NULL,
  `wachtwoord_hash` varchar(255) NOT NULL,
  `email` varchar(100) NOT NULL,
  `rolID` int(11) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `gebruikers_score`
--

CREATE TABLE `gebruikers_score` (
  `scoreID` int(11) NOT NULL,
  `gebruikerID` int(11) DEFAULT NULL,
  `verkiezingID` int(11) DEFAULT NULL,
  `resultaat` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL CHECK (json_valid(`resultaat`))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `nieuwsberichten`
--

CREATE TABLE `nieuwsberichten` (
  `nieuwsberichtID` int(11) NOT NULL,
  `titel` varchar(150) NOT NULL,
  `inhoud` text NOT NULL,
  `publicatiedatum` datetime DEFAULT current_timestamp(),
  `gebruikerID` int(11) DEFAULT NULL,
  `partijID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `nieuwsreactie`
--

CREATE TABLE `nieuwsreactie` (
  `reactieID` int(11) NOT NULL,
  `nieuwsberichtID` int(11) DEFAULT NULL,
  `gebruikerID` int(11) DEFAULT NULL,
  `inhoud` text NOT NULL,
  `datum` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `partijdeelnemers`
--

CREATE TABLE `partijdeelnemers` (
  `deelnemerID` int(11) NOT NULL,
  `voornaam` varchar(50) NOT NULL,
  `achternaam` varchar(50) NOT NULL,
  `partijID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `partijen`
--

CREATE TABLE `partijen` (
  `partijID` int(11) NOT NULL,
  `partijnaam` varchar(100) NOT NULL,
  `partijinfo` text DEFAULT NULL,
  `aantalstemmen` int(11) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `partij_standpunt`
--

CREATE TABLE `partij_standpunt` (
  `partijID` int(11) NOT NULL,
  `stellingID` int(11) NOT NULL,
  `antwoord` tinyint(1) DEFAULT NULL,
  `gewicht` float DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `rol`
--

CREATE TABLE `rol` (
  `rolID` int(11) NOT NULL,
  `rolNaam` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `rollen`
--

CREATE TABLE `rollen` (
  `rolID` int(11) NOT NULL,
  `rolNaam` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `standpunten`
--

CREATE TABLE `standpunten` (
  `standpuntID` int(11) NOT NULL,
  `standpunt` text NOT NULL,
  `partijID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `stellingen`
--

CREATE TABLE `stellingen` (
  `stellingID` int(11) NOT NULL,
  `verkiezingID` int(11) DEFAULT NULL,
  `tekst` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `verkiezingen`
--

CREATE TABLE `verkiezingen` (
  `verkiezingID` int(11) NOT NULL,
  `titel` varchar(100) NOT NULL,
  `beschrijving` text DEFAULT NULL,
  `start_datum` datetime DEFAULT NULL,
  `eind_datum` datetime DEFAULT NULL,
  `aanmaakdatum` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `verkiezing_partij`
--

CREATE TABLE `verkiezing_partij` (
  `partijID` int(11) NOT NULL,
  `verkiezingID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `gebruikers`
--
ALTER TABLE `gebruikers`
  ADD PRIMARY KEY (`gebruikerID`),
  ADD UNIQUE KEY `gebruikersnaam` (`gebruikersnaam`),
  ADD UNIQUE KEY `email` (`email`),
  ADD KEY `rolID` (`rolID`);

--
-- Indexen voor tabel `gebruikers_score`
--
ALTER TABLE `gebruikers_score`
  ADD PRIMARY KEY (`scoreID`),
  ADD KEY `gebruikerID` (`gebruikerID`),
  ADD KEY `verkiezingID` (`verkiezingID`);

--
-- Indexen voor tabel `nieuwsberichten`
--
ALTER TABLE `nieuwsberichten`
  ADD PRIMARY KEY (`nieuwsberichtID`),
  ADD KEY `gebruikerID` (`gebruikerID`),
  ADD KEY `partijID` (`partijID`);

--
-- Indexen voor tabel `nieuwsreactie`
--
ALTER TABLE `nieuwsreactie`
  ADD PRIMARY KEY (`reactieID`),
  ADD KEY `nieuwsberichtID` (`nieuwsberichtID`),
  ADD KEY `gebruikerID` (`gebruikerID`);

--
-- Indexen voor tabel `partijdeelnemers`
--
ALTER TABLE `partijdeelnemers`
  ADD PRIMARY KEY (`deelnemerID`),
  ADD KEY `partijID` (`partijID`);

--
-- Indexen voor tabel `partijen`
--
ALTER TABLE `partijen`
  ADD PRIMARY KEY (`partijID`);

--
-- Indexen voor tabel `partij_standpunt`
--
ALTER TABLE `partij_standpunt`
  ADD PRIMARY KEY (`partijID`,`stellingID`),
  ADD KEY `stellingID` (`stellingID`);

--
-- Indexen voor tabel `rol`
--
ALTER TABLE `rol`
  ADD PRIMARY KEY (`rolID`);

--
-- Indexen voor tabel `rollen`
--
ALTER TABLE `rollen`
  ADD PRIMARY KEY (`rolID`);

--
-- Indexen voor tabel `standpunten`
--
ALTER TABLE `standpunten`
  ADD PRIMARY KEY (`standpuntID`),
  ADD KEY `partijID` (`partijID`);

--
-- Indexen voor tabel `stellingen`
--
ALTER TABLE `stellingen`
  ADD PRIMARY KEY (`stellingID`),
  ADD KEY `verkiezingID` (`verkiezingID`);

--
-- Indexen voor tabel `verkiezingen`
--
ALTER TABLE `verkiezingen`
  ADD PRIMARY KEY (`verkiezingID`);

--
-- Indexen voor tabel `verkiezing_partij`
--
ALTER TABLE `verkiezing_partij`
  ADD PRIMARY KEY (`partijID`,`verkiezingID`),
  ADD KEY `verkiezingID` (`verkiezingID`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `gebruikers`
--
ALTER TABLE `gebruikers`
  MODIFY `gebruikerID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `gebruikers_score`
--
ALTER TABLE `gebruikers_score`
  MODIFY `scoreID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `nieuwsberichten`
--
ALTER TABLE `nieuwsberichten`
  MODIFY `nieuwsberichtID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `nieuwsreactie`
--
ALTER TABLE `nieuwsreactie`
  MODIFY `reactieID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `partijdeelnemers`
--
ALTER TABLE `partijdeelnemers`
  MODIFY `deelnemerID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `partijen`
--
ALTER TABLE `partijen`
  MODIFY `partijID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `rol`
--
ALTER TABLE `rol`
  MODIFY `rolID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `rollen`
--
ALTER TABLE `rollen`
  MODIFY `rolID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `standpunten`
--
ALTER TABLE `standpunten`
  MODIFY `standpuntID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `stellingen`
--
ALTER TABLE `stellingen`
  MODIFY `stellingID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `verkiezingen`
--
ALTER TABLE `verkiezingen`
  MODIFY `verkiezingID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Beperkingen voor geëxporteerde tabellen
--

--
-- Beperkingen voor tabel `gebruikers`
--
ALTER TABLE `gebruikers`
  ADD CONSTRAINT `gebruikers_ibfk_1` FOREIGN KEY (`rolID`) REFERENCES `rol` (`rolID`);

--
-- Beperkingen voor tabel `gebruikers_score`
--
ALTER TABLE `gebruikers_score`
  ADD CONSTRAINT `gebruikers_score_ibfk_1` FOREIGN KEY (`gebruikerID`) REFERENCES `gebruikers` (`gebruikerID`),
  ADD CONSTRAINT `gebruikers_score_ibfk_2` FOREIGN KEY (`verkiezingID`) REFERENCES `verkiezingen` (`verkiezingID`);

--
-- Beperkingen voor tabel `nieuwsberichten`
--
ALTER TABLE `nieuwsberichten`
  ADD CONSTRAINT `nieuwsberichten_ibfk_1` FOREIGN KEY (`gebruikerID`) REFERENCES `gebruikers` (`gebruikerID`),
  ADD CONSTRAINT `nieuwsberichten_ibfk_2` FOREIGN KEY (`partijID`) REFERENCES `partijen` (`partijID`);

--
-- Beperkingen voor tabel `nieuwsreactie`
--
ALTER TABLE `nieuwsreactie`
  ADD CONSTRAINT `nieuwsreactie_ibfk_1` FOREIGN KEY (`nieuwsberichtID`) REFERENCES `nieuwsberichten` (`nieuwsberichtID`),
  ADD CONSTRAINT `nieuwsreactie_ibfk_2` FOREIGN KEY (`gebruikerID`) REFERENCES `gebruikers` (`gebruikerID`);

--
-- Beperkingen voor tabel `partijdeelnemers`
--
ALTER TABLE `partijdeelnemers`
  ADD CONSTRAINT `partijdeelnemers_ibfk_1` FOREIGN KEY (`partijID`) REFERENCES `partijen` (`partijID`);

--
-- Beperkingen voor tabel `partij_standpunt`
--
ALTER TABLE `partij_standpunt`
  ADD CONSTRAINT `partij_standpunt_ibfk_1` FOREIGN KEY (`partijID`) REFERENCES `partijen` (`partijID`),
  ADD CONSTRAINT `partij_standpunt_ibfk_2` FOREIGN KEY (`stellingID`) REFERENCES `stellingen` (`stellingID`);

--
-- Beperkingen voor tabel `standpunten`
--
ALTER TABLE `standpunten`
  ADD CONSTRAINT `standpunten_ibfk_1` FOREIGN KEY (`partijID`) REFERENCES `partijen` (`partijID`);

--
-- Beperkingen voor tabel `stellingen`
--
ALTER TABLE `stellingen`
  ADD CONSTRAINT `stellingen_ibfk_1` FOREIGN KEY (`verkiezingID`) REFERENCES `verkiezingen` (`verkiezingID`);

--
-- Beperkingen voor tabel `verkiezing_partij`
--
ALTER TABLE `verkiezing_partij`
  ADD CONSTRAINT `verkiezing_partij_ibfk_1` FOREIGN KEY (`partijID`) REFERENCES `partijen` (`partijID`),
  ADD CONSTRAINT `verkiezing_partij_ibfk_2` FOREIGN KEY (`verkiezingID`) REFERENCES `verkiezingen` (`verkiezingID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
